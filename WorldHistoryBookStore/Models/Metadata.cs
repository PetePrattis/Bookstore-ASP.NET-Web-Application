using System;
using System.ComponentModel.DataAnnotations;

namespace WorldHistoryBookStore.Models
{//ready
    public class AuthorMetadata 
    {

        [Required]
        [RegularExpression(@"[0-9][0-9][0-9]-[0-9][0-9]-[0-9][0-9][0-9][0-9]",
        ErrorMessage = "ID example: 132-49-8765. All digits accepted.")]
        [Display(Name = "au_id")]
        public object au_id;


        [Required]
        [StringLength(40)]
        [RegularExpression("^[a-zA-Z _]+$", ErrorMessage = "Only letters accepted.")]
        [Display(Name = "au_lname")]
        public string au_lname;


        [Required]
        [StringLength(20)]
        [RegularExpression("^[a-zA-Z _]+$", ErrorMessage = "Only letters accepted.")]
        [Display(Name = "au_fname")]
        public string au_fname;

        [StringLength(12)]
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only digits accepted.")]
        [Display(Name = "phone")]
        public string phone;

        [StringLength(40)]
        [Display(Name = "address")]
        public string address;

        [StringLength(20)]
        [Display(Name = "city")]
        public string city;

        [StringLength(2)]
        [RegularExpression("^[a-zA-Z _]+$", ErrorMessage = "Only letters accepted.")]
        [Display(Name = "state")]
        public string state;

        [StringLength(5)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only digits accepted.")]
        [Display(Name = "zip")]
        public string zip;


        [Required]
        [Display(Name = "contract")]
        public string contract;
    }

    public class TitleMetadata
    {
        [Required]
        [StringLength(6)]
        [Display(Name = "title_id")]
        public string title_id;

        [Required]
        [StringLength(80)]
        [Display(Name = "title1")]
        public string title1;

        [Required]
        [StringLength(12)]
        [RegularExpression("^[a-zA-Z _]+$", ErrorMessage = "Only letters accepted.")]
        [Display(Name = "type")]
        public string type;

        [MaxLength(12)]
        [Display(Name = "price")]
        public Decimal price;

        [MaxLength(12)]
        [Display(Name = "advance")]
        public Decimal advance;

        [StringLength(200)]
        [Display(Name = "notes")]
        public string notes;


    }
    
    public class PublisherMetadata
    {
        [Required]
        [StringLength(4)]
        [RegularExpression("99[0-9][0-9]", ErrorMessage = "ID example: 99xx, where x is number.")]
        [Display(Name ="pub_id")]
        public string pub_id;

        [StringLength(40)]
        [Display(Name = "pub_name")]
        public string pub_name;

        [StringLength(20)]
        [Display(Name = "city")]
        public string city;

        [StringLength(2)]
        [Display(Name = "state")]
        public string state;

        [StringLength(30)]
        [Display(Name = "country")]
        public string country;
    }

    public class StoreMetadata
    {
        [Required]
        [StringLength(4)]
        [Display(Name ="stor_id")]
        public string stor_id;

        [StringLength(40)]
        [Display(Name ="stor_name")]
        public string stor_name;

        [StringLength(40)]
        [Display(Name = "stor_address")]
        public string stor_address;

        [StringLength(20)]
        [Display(Name = "city")]
        public string city;

        [StringLength(2)]
        [Display(Name = "state")]
        public string state;

        [StringLength(5)]
        [Display(Name = "zip")]
        public string zip;
    }

    public class SaleMetadata
    {
        [Required]
        [StringLength(4)]
        [Display(Name = "stor_id")]
        public string stor_id;

        [Required]
        [StringLength(20)]
        [Display(Name = "ord_num")]
        public string ord_num;

        [Required]
        [Display(Name = "ord_date")]
        public DateTime ord_date;

        [Required]
        [Range(0,32767, ErrorMessage ="Quantity values starts from 0.")]
        [Display(Name = "qty")]
        public string qty;
        
        [Required]
        [StringLength(12)]
        [Display(Name = "payterms")]
        public string payterms;

        [Required]
        [StringLength(6)]
        [Display(Name = "title_id")]
        public string title_id;
    }

    public class DiscountMetadata
    {
        [Required]
        [StringLength(40)]
        [Display(Name ="discounttype")]
        public string discounttype;

        [StringLength(4)]
        [Display(Name ="stor_id")]
        public string stor_id;

        [Display(Name ="lowqty")]
        [Range(-32768, 32767)]
        public string lowqty;

        [Display(Name = "highqty")]
        [Range(-32768,32767)]
        public string highqty;

        [Required]
        [Display(Name ="discount1")]
        [RegularExpression("^[0-9]{1,2}(.[0-9]{2})?$", ErrorMessage="Acceptable number is a decimal one with a precision of two.")]
        public float discount1;
    }

    public class JobMetadata
    {
        [Required]
        [Display(Name = "job_id")]
        public Int16 job_id;

        //[Required]
        [Display(Name = "job_desc")]
        [StringLength(50)]
        public string job_desc;

        [Required]
        [Display(Name = "min_lvl")]
        [Range(10,250)]
        public Byte min_lvl;


        [Required]
        [Display(Name = "max_lvl")]
        [Range(10, 250)]
        public Byte max_lvl;

    }

    public class EmployeeMetadata
    {
        [Required]
        [StringLength(9)]
        [Display(Name = "emp_id")]
        [RegularExpression("[A-Z][A-Z][A-Z][1-9][0-9][0-9][0-9][0-9][FM]+$",
            ErrorMessage ="ID example: ZZZxyyyyF or ZZZxyyyyM where Z is capital A-Z, x is a number from 1-9 and y from 0-9.")]
        public string emp_id;

        [Required]
        [StringLength(20)]
        [Display(Name = "fname")]
        public string fname;

        [StringLength(1)]
        [Display(Name = "minit")]
        public string minit;

        [Required]
        [StringLength(30)]
        [Display(Name = "lname")]
        public string lname;

        [Required]
        [Display(Name = "job_id")]
        public int job_id;

        //[Required]
        [StringLength(4)]
        [Display(Name = "pub_id")]
        public string pub_id;

    }

    public class TitleauthorMetadata
    {
        [Display(Name = "au_ord")]
        [Range(0,255, ErrorMessage ="Values must be betweeen 0 and 255")] 
        public Byte au_ord;
    }
  
}