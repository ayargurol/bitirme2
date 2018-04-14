using CoreWebApp2.Models;
using System;
using System.Collections.Generic;

namespace CoreWebApp2.Custom
{
    public class filters
    {
        public static List<product> PriceSort(List<product> db)
        {
            for (int i = 0; i < db.Count - 1; i++)
            {
                for (int j = 0; j < db.Count - i - 1; j++)
                {
                    if (parsedCompare(db[j].Fiyat, db[j + 1].Fiyat))
                    {
                        db = swap(db, j, j + 1);
                    }
                }
            }
            return db;
        }

        // Swaps items at received indexes
        private static List<product> swap(List<product> db, int a, int b)
        {
            var _temp = db[a];
            db[a] = db[b];
            db[b] = _temp;
            return db;
        }

        //Removes "TL" from the end and compares.If first is bigger returns TRUE
        private static bool parsedCompare(string first, string second)
        {
            try
            {
                //Bug
                var _first = Convert.ToDouble(first.Remove(first.IndexOf('T'), 2));
                var _second = Convert.ToDouble(second.Remove(second.IndexOf('T'), 2));
                if (_first > _second)
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