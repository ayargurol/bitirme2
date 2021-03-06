﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CoreWebApp2.Models;
using HtmlAgilityPack;

namespace CoreWebApp2.Custom
{
    public class HapServ
    {
        public string _baseUrl { get; }
        public string logo_link { get; set; }
        private readonly string _url;
        private readonly string _sitename;
        private readonly string _tekrarlanan;
        private readonly List<int> _name;
        private readonly string _nameAtr;
        private readonly List<int> _price;
        private readonly List<int> _price2;
        private readonly string _priceAtr;
        private readonly List<int> _link;
        private readonly string _linkExtra;
        private readonly string _linkAtr;
        private readonly List<int> _point;
        private readonly string _pointAtr;
        private readonly List<int> _img;
        private readonly string _imgAtr;
        private readonly List<int> _seller;
        private readonly string _sellerAtr;

        public HapServ(string url, string sitename, string baseUrl, string tekrarlanan, List<int> isim, string isimAtr,
            List<int> fiyat,
            List<int> fiyat2, string fiyatAtr, List<int> link, string linkAtr, string linkExtra, List<int> puan,
            string puanAtr, List<int> resim, string resimAtr, List<int> satici, string saticiAtr)
        {
            _url = url;
            _sitename = sitename;
            _baseUrl = baseUrl;
            _tekrarlanan = tekrarlanan;
            _name = isim;
            _nameAtr = isimAtr;
            _price = fiyat;
            _price2 = fiyat2;
            _priceAtr = fiyatAtr;
            _link = link;
            _linkAtr = linkAtr;
            _linkExtra = linkExtra;
            _point = puan;
            _pointAtr = puanAtr;
            _img = resim;
            _imgAtr = resimAtr;
            _seller = satici;
            _sellerAtr = saticiAtr;
        }

        public async Task<SearchModel> GetProducts()
        {
            var loader = new HtmlWeb();
            var watch = Stopwatch.StartNew();
            var doc = loader.Load(_url);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var db = new SearchModel();
            Console.WriteLine($"Request answered from " + _sitename.ToUpper() + " with " + elapsedMs + " ms");

            var liNode = doc.DocumentNode.SelectNodes(_tekrarlanan);
            foreach (var item in liNode)
            {
                try
                {
                    // Product Initialization
                    var pro = new product
                    {
                        Isim = await Getname(item, _name, _nameAtr),
                        Fiyat = await Getprice(item, _price, _price2, _priceAtr),
                        Link = await Getlink(item, _link, _linkAtr),
                        Resim = await Getimg(item, _img, _imgAtr),
                        Satıcı = await Getseller(item, _seller, _sellerAtr),
                        Puan = await Getpoint(item, _point, _pointAtr),
                        Site = _sitename
                    };
                    pro.Kategori = await GetCategory(pro.Link);

                    #region Conditional Edits

                    if (_linkExtra != null || !string.IsNullOrEmpty(_linkExtra) || !pro.Link.Contains(@"www") || _linkExtra.Length > 1)
                    {
                        pro.Link = _linkExtra + pro.Link;
                    }

                    if (string.IsNullOrEmpty(pro.Fiyat) || string.IsNullOrEmpty(pro.Isim) ||
                        string.IsNullOrEmpty(pro.Link) || string.IsNullOrEmpty(pro.Resim))
                    {
                        continue;
                    }

                    if (pro.Satıcı == null)
                    {
                        pro.Satıcı = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(pro.Puan))
                    {
                        pro.Puan = pro.Puan.Replace(@"%", string.Empty);
                        if (pro.Puan.Contains("width"))
                        {
                            pro.Puan = pro.Puan
                                .Replace("width:", string.Empty)
                                .Replace(";", string.Empty);
                        }
                    }
                    else
                    {
                        pro.Puan = string.Empty;
                    }

                    pro.Fiyat = pro.Fiyat.Replace("TL", string.Empty)
                        .Replace(".", string.Empty);
                    var proFiyat = Convert.ToDouble(pro.Fiyat);
                    pro.Fiyat = pro.Fiyat.Remove(pro.Fiyat.IndexOf(','), 3);

                    if (proFiyat < 25)
                    {
                        db.CountPrices.c0_25++;
                    }
                    else if (proFiyat < 50)
                    {
                        db.CountPrices.c25_50++;
                    }
                    else if (proFiyat < 100)
                    {
                        db.CountPrices.c50_100++;
                    }
                    else if (proFiyat < 250)
                    {
                        db.CountPrices.c100_250++;
                    }
                    else if (proFiyat < 500)
                    {
                        db.CountPrices.c250_500++;
                    }
                    else if (proFiyat < 1000)
                    {
                        db.CountPrices.c500_1000++;
                    }
                    else if (proFiyat < 2500)
                    {
                        db.CountPrices.c1000_2500++;
                    }
                    else if (proFiyat < 5000)
                    {
                        db.CountPrices.c2500_5000++;
                    }
                    else
                    {
                        db.CountPrices.c5000_plus++;
                    }

                    #endregion

                    db.Products.Add(pro);
                    db.TotalCount++;

                }
                catch (Exception)
                {
                    continue;
                }
            }
            db.logo_link = logo_link;
            db.sitename = _sitename;
            return db;
        }

        public async Task<string> Getname(HtmlNode li, List<int> child, string atr)
        {
            var nameNode = li;
            try
            {
                await Task.Yield();
                foreach (var item in child) nameNode = nameNode.ChildNodes[item];
                return string.IsNullOrEmpty(atr)
                    ? nameNode.InnerText.Trim()
                    : nameNode.Attributes[atr].Value.Trim();
            }
            catch (Exception)
            {
                await Task.Yield();
                return null;
            }
        }

        public async Task<string> Getprice(HtmlNode li, List<int> child, List<int> child2, string atr)
        {
            var priceNode = li;
            string price;
            try
            {
                foreach (var item in child) priceNode = priceNode.ChildNodes[item];
                price = string.IsNullOrEmpty(atr)
                    ? priceNode.InnerText.Trim().Replace(" ", string.Empty)
                        .Replace("\n", string.Empty)
                    : priceNode.Attributes[atr].Value.Trim().Replace(" ", string.Empty)
                        .Replace("\n", string.Empty);
                await Task.Yield();
                return price;
            }
            catch (Exception)
            {
                try
                {
                    priceNode = li;
                    foreach (var item in child2) priceNode = priceNode.ChildNodes[item];
                    price = string.IsNullOrEmpty(atr)
                        ? priceNode.InnerText.Trim().Replace(" ", string.Empty)
                            .Replace("\n", string.Empty)
                        : priceNode.Attributes[atr].Value.Trim().Replace(" ", string.Empty)
                            .Replace($"\n", string.Empty);
                    await Task.Yield();
                    return price;
                }
                catch (Exception)
                {
                    await Task.Yield();
                    return null;
                }
            }
        }

        public async Task<string> Getlink(HtmlNode li, List<int> child, string atr)
        {
            var linkNode = li;
            try
            {
                foreach (var item in child) linkNode = linkNode.ChildNodes[item];
                await Task.Yield();
                return string.IsNullOrEmpty(atr)
                    ? linkNode.InnerText.Trim()
                    : linkNode.Attributes[atr].Value.Trim();
            }
            catch (Exception)
            {
                await Task.Yield();
                return null;
            }
        }

        public async Task<string> Getimg(HtmlNode li, List<int> child, string atr)
        {
            var imgNode = li;
            try
            {
                foreach (var item in child) imgNode = imgNode.ChildNodes[item];
                await Task.Yield();
                return string.IsNullOrEmpty(atr)
                    ? imgNode.InnerText.Trim()
                    : imgNode.Attributes[atr].Value.Trim();
            }
            catch (Exception)
            {
                await Task.Yield();
                return null;
            }
        }

        public async Task<string> Getseller(HtmlNode li, List<int> child, string atr)
        {
            var sellerNode = li;
            try
            {
                foreach (var item in child) sellerNode = sellerNode.ChildNodes[item];
                await Task.Yield();
                return string.IsNullOrEmpty(atr)
                    ? sellerNode.InnerText.Trim()
                    : sellerNode.Attributes[atr].Value.Trim();
            }
            catch (Exception)
            {
                await Task.Yield();
                return null;
            }
        }

        public async Task<string> Getpoint(HtmlNode li, List<int> child, string atr)
        {
            var pointNode = li;
            try
            {
                foreach (var item in child) pointNode = pointNode.ChildNodes[item];
                await Task.Yield();
                return string.IsNullOrEmpty(atr)
                    ? pointNode.InnerText.Trim()
                    : pointNode.Attributes[atr].Value.Trim();
            }
            catch (Exception)
            {
                await Task.Yield();
                return null;
            }
        }

        public async Task<string> GetCategory(string link)
        {
            try
            {
                var stash = link;
                stash = stash.Remove(startIndex: 0, count: link.IndexOf(value: ".com", startIndex: 0, comparisonType: StringComparison.Ordinal) + 5);
                var startIndexOfSlash = stash.IndexOf("/");
                stash = stash.Remove(startIndexOfSlash);
                await Task.Yield();
                return stash.Replace('-', ' ');
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}