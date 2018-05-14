using CoreWebApp2.Models.Sql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreWebApp2.Custom
{
    public class SitesConverter
    {
        public List<HapServ> GetSites(SitesContext context,string search)
        {
            List<HapServ> siteList = new List<HapServ>();
            foreach (var item in context.Sites.ToList())
            {
                try
                {
                    HapServ hap = new HapServ(item.SearchUrlPart1 + search + item.SearcUrlPart2, item.SiteName, item.BaseUrl, item.RepatedItem, StringToIntList(item.NameChilds), item.NameAttribute, StringToIntList(item.PriceChilds), StringToIntList(item.PriceChildsTwo), item.PriceAttribute, StringToIntList(item.LinkChilds), item.LinkAttribute, item.LinkExtra, StringToIntList(item.SatisfactionChilds), item.SatisfactionAttribute, StringToIntList(item.ImageChilds), item.ImageAttribute, StringToIntList(item.SellerChilds), item.SellerAttribute);
                    hap.logo_link = item.Logo_link;
                    siteList.Add(hap);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return siteList;
        }
        private List<int> StringToIntList(string str)
        {
            var list = str.Split(',');
            var intList = new List<int>();
            foreach (var item in list)
            {
                intList.Add(Convert.ToInt32(item));
            }
            return intList;
        }
    }
}
