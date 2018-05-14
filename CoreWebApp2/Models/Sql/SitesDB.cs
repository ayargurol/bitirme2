using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApp2.Models.Sql
{
    [Table("Sites")]
    public class SitesDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SiteId { get; set; }


        [Display(Name = "Site İsmi"), DataType(DataType.Text)]
        [Required]
        public string SiteName { get; set; }


        [Display(Name = "Site Adresi"), DataType(DataType.Url)]
        [Required]
        public string BaseUrl { get; set; }

        [Required]
        [Display(Name = "Site Logosu"), DataType(DataType.Url)]
        public string Logo_link { get; set; }


        [Display(Name = "Arama Url'i ilk kısım"), DataType(DataType.Text)]
        [Required]
        public string SearchUrlPart1 { get; set; }


        [Display(Name = "Arama Url'i ikinci kısım"), DataType(DataType.Text)]
        [Required]
        public string SearcUrlPart2 { get; set; }


        [Required]
        [Display(Name = "Arama Sonucu Tekrar Eden Kısım"), DataType(DataType.Text)]
        public string RepatedItem { get; set; }


        [Column("NameChilds", TypeName = "varchar")]
        [Display(Name = "İsim Sıralaması"), DataType(DataType.Text)]
        public string NameChilds { get; set; }


        [Display(Name = "İsim Niteliği"), DataType(DataType.Text)]
        public string NameAttribute { get; set; }


        [Display(Name = "Fiyat Sıralaması"), DataType(DataType.Text)]
        [Column("PriceChilds", TypeName = "varchar")]
        public string PriceChilds { get; set; }


        [Column("PriceChildsTwo", TypeName = "varchar")]
        [Display(Name = "İkinci Fiyat Sıralaması"), DataType(DataType.Text)]
        public string PriceChildsTwo { get; set; }


        [Column("PriceAttribute", TypeName = "varchar")]
        [Display(Name = "Fiyat Niteliği"), DataType(DataType.Text)]
        public string PriceAttribute { get; set; }


        [Required]
        [Column("LinkChilds", TypeName = "varchar")]
        [Display(Name = "Link Sıralaması"), DataType(DataType.Text)]
        public string LinkChilds { get; set; }


        [Required]
        [Column("LinkAttribute", TypeName = "varchar")]
        [Display(Name = "Link Niteliği"), DataType(DataType.Text)]
        public string LinkAttribute { get; set; }


        [Column("LinkExtra", TypeName = "varchar")]
        [Display(Name = "Link Ekstra"), DataType(DataType.Text)]
        public string LinkExtra { get; set; }


        [Required]
        [Column("ImageChilds", TypeName = "varchar")]
        [Display(Name = "Resim Sıralaması"), DataType(DataType.Text)]
        public string ImageChilds { get; set; }


        [Required]
        [Column("ImageAttribute", TypeName = "varchar")]
        [Display(Name = "Resim Niteliği"), DataType(DataType.Text)]
        public string ImageAttribute { get; set; }


        [Column("SellerChilds", TypeName = "varchar")]
        [Display(Name = "Satıcı Sıralaması"), DataType(DataType.Text)]
        public string SellerChilds { get; set; }


        [Column("SellerAttribute", TypeName = "varchar")]
        [Display(Name = "Satıcı Niteliği"), DataType(DataType.Text)]
        public string SellerAttribute { get; set; }


        [Column("SatisfactionChilds", TypeName = "varchar")]
        [Display(Name = "Puan Sıralaması"), DataType(DataType.Text)]
        public string SatisfactionChilds { get; set; }


        [Column("SatisfactionAttribute", TypeName = "varchar")]
        [Display(Name = "Puan Niteliği"), DataType(DataType.Text)]
        public string SatisfactionAttribute { get; set; }


    }
}
