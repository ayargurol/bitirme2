using System;
using System.Collections.Generic;
using CoreWebApp2.Models;

namespace CoreWebApp2.Custom
{
    public class Filters
    {
        public static List<product> PriceSort(List<product> db)
        {
            for (int i = 0; i < db.Count - 1; i++)
            {
                for (int j = 0; j < db.Count - i - 1; j++)
                {
                    if (ParsedCompare(db[j].Fiyat, db[j + 1].Fiyat))
                    {
                        db = Swap(db, j, j + 1);
                    }
                }
            }
            return db;
        }

        // Swaps items at received indexes
        private static List<product> Swap(List<product> db, int a, int b)
        {
            var temp = db[a];
            db[a] = db[b];
            db[b] = temp;
            return db;
        }

        //Removes "TL" from the end and compares.If first is bigger returns TRUE
        private static bool ParsedCompare(string firstI, string secondI)
        {
            try
            {
                //Bug
                var first = Convert.ToDouble(firstI.Remove(firstI.IndexOf('T'), 2));
                var second = Convert.ToDouble(secondI.Remove(secondI.IndexOf('T'), 2));
                if (first > second)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}