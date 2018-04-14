using CoreWebApp2.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace CoreWebApp2.Custom
{
    public class hapServ
    {
        private string url;
        private string baseUrl;
        private string tekrarlanan;
        private List<int> name;
        private string name_atr;
        private List<int> price;
        private List<int> price_2;
        private string price_atr;
        private List<int> link;
        private string link_extra;
        private string link_atr;
        private List<int> point;
        private string point_atr;
        private List<int> img;
        private string img_atr;
        private List<int> seller;
        private string seller_atr;

        public hapServ(string url, string baseUrl, string tekrarlanan, List<int> isim, string isim_atr, List<int> fiyat, List<int> fiyat_2, string fiyat_atr, List<int> link, string link_atr, string link_extra, List<int> puan, string puan_atr, List<int> resim, string resim_atr, List<int> satici, string satici_atr)
        {
            this.url = url;
            this.baseUrl = baseUrl;
            this.tekrarlanan = tekrarlanan;
            this.name = isim;
            this.name_atr = isim_atr;
            this.price = fiyat;
            this.price_2 = fiyat_2;
            this.price_atr = fiyat_atr;
            this.link = link;
            this.link_atr = link_atr;
            this.link_extra = link_extra;
            this.point = puan;
            this.point_atr = puan_atr;
            this.img = resim;
            this.img_atr = resim_atr;
            this.seller = satici;
            this.seller_atr = satici_atr;
        }

        public async Task<List<product>> GetProducts()
        {
            try
            {
                var db = new List<product>();
                HtmlWeb loader = new HtmlWeb();
                var doc = loader.Load(this.url);
                var liNode = doc.DocumentNode.SelectNodes(this.tekrarlanan);
                foreach (var item in liNode)
                {
                    try
                    {
                        product pro = new product
                        {
                            Isim = await this.Getname(item, this.name, this.name_atr),
                            Fiyat = await this.Getprice(
                                                      item,
                                                      this.price,
                                                      this.price_2,
                                                      this.price_atr
                                        ),
                            Link = string.IsNullOrEmpty(this.link_extra)
                                 ? await this.Getlink(item, this.link, this.link_atr)
                                 : this.link_extra + await this.Getlink(item, this.link, this.link_atr),
                            Resim = await this.Getimg(item, this.img, this.img_atr),
                            Satıcı = await this.Getseller(item, this.seller, this.seller_atr),
                            Puan = await this.Getpoint(item, this.point, this.point_atr)
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
                        continue;
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
            string price = null;
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
                    price = null;
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