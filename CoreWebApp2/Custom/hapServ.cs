using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWebApp2.Models;
using HtmlAgilityPack;

namespace CoreWebApp2.Custom
{
    public class HapServ
    {
        private readonly string _url;
        public string BaseUrl { get; }
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

        public HapServ(string url, string baseUrl, string tekrarlanan, List<int> isim, string isimAtr, List<int> fiyat, List<int> fiyat2, string fiyatAtr, List<int> link, string linkAtr, string linkExtra, List<int> puan, string puanAtr, List<int> resim, string resimAtr, List<int> satici, string saticiAtr)
        {
            _url = url;
            BaseUrl = baseUrl;
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

        public async Task<List<product>> GetProducts()
        {
            try
            {
                var db = new List<product>();
                HtmlWeb loader = new HtmlWeb();
                var doc = loader.Load(_url);
                var liNode = doc.DocumentNode.SelectNodes(_tekrarlanan);
                foreach (var item in liNode)
                {
                    try
                    {
                        product pro = new product
                        {
                            Isim = await Getname(item, _name, _nameAtr),
                            Fiyat = await Getprice(
                                                      item,
                                                      _price,
                                                      _price2,
                                                      _priceAtr
                                        ),
                            Link = string.IsNullOrEmpty(_linkExtra)
                                 ? await Getlink(item, _link, _linkAtr)
                                 : _linkExtra + await Getlink(item, _link, _linkAtr),
                            Resim = await Getimg(item, _img, _imgAtr),
                            Satıcı = await Getseller(item, _seller, _sellerAtr),
                            Puan = await Getpoint(item, _point, _pointAtr)
                        };
                        if (pro.Fiyat == null || pro.Isim == null || pro.Link == null || pro.Resim == null)
                        {
                            continue;
                        }
                        db.Add(pro);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                return db;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<string> Getname(HtmlNode li, List<int> child, string atr)
        {
            var nameNode = li;
            try
            {
                await Task.Yield();
                foreach (var item in child) nameNode = nameNode.ChildNodes[item];
                return string.IsNullOrEmpty(atr) ? nameNode.InnerText.Trim() : nameNode.Attributes[atr].Value.Trim();
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
                price = string.IsNullOrEmpty(atr) ? priceNode.InnerText.Trim().Replace(" ", string.Empty).Replace("\n", string.Empty) : priceNode.Attributes[atr].Value.Trim().Replace(" ", string.Empty).Replace("\n", string.Empty);
                await Task.Yield();
                return price;
            }
            catch (Exception)
            {
                try
                {
//                    price = null;
                    priceNode = li;
                    foreach (var item in child2) priceNode = priceNode.ChildNodes[item];
                    price = string.IsNullOrEmpty(atr) ? priceNode.InnerText.Trim().Replace(" ", string.Empty).Replace("\n", string.Empty) : priceNode.Attributes[atr].Value.Trim().Replace(" ", string.Empty).Replace($"\n", string.Empty);
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
                return string.IsNullOrEmpty(atr) ? linkNode.InnerText.Trim() : linkNode.Attributes[atr].Value.Trim();
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
                return string.IsNullOrEmpty(atr) ? imgNode.InnerText.Trim() : imgNode.Attributes[atr].Value.Trim();
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
                return string.IsNullOrEmpty(atr) ? sellerNode.InnerText.Trim() : sellerNode.Attributes[atr].Value.Trim();
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
                return string.IsNullOrEmpty(atr) ? pointNode.InnerText.Trim() : pointNode.Attributes[atr].Value.Trim();
            }
            catch (Exception)
            {
                await Task.Yield();
                return null;
            }
        }
    }
}