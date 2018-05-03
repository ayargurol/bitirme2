// ReSharper disable InconsistentNaming

namespace CoreWebApp2.Models
{
    public class Prices
    {
        public int c0_25 { get; set; }
        public int c25_50 { get; set; }
        public int c50_100 { get; set; }
        public int c100_250 { get; set; }
        public int c250_500 { get; set; }
        public int c500_1000 { get; set; }
        public int c1000_2500 { get; set; }
        public int c2500_5000 { get; set; }
        public int c5000_plus { get; set; }

        public Prices()
        {
            c0_25 = c25_50 =
                c50_100 = c100_250 = c250_500 = c500_1000 = c1000_2500 = c2500_5000 = c5000_plus = 0;
        }
    }
}