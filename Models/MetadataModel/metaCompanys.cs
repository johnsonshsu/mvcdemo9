using System;
using System.Collections.Generic;

namespace mvcdemo9.Models
{
    [ModelMetadataType(typeof(z_metaCompanys))]
    public partial class Companys
    {

    }
}

public class z_metaCompanys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "預設")]
    public bool IsDefault { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "類別代號")]
    public string? CodeNo { get; set; }
    [Display(Name = "公司編號")]
    [Required(ErrorMessage = "公司編號不可空白!!")]
    public string? CompNo { get; set; }
    [Display(Name = "公司名稱")]
    [Required(ErrorMessage = "公司名稱不可空白!!")]
    public string? CompName { get; set; }
    [Display(Name = "公司簡稱")]
    [Required(ErrorMessage = "公司簡稱不可空白!!")]
    public string? ShortName { get; set; }
    [Display(Name = "英文名稱")]
    public string? EngName { get; set; }
    [Display(Name = "英文簡稱")]
    public string? EngShortName { get; set; }
    [Display(Name = "註冊日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime RegisterDate { get; set; }
    [Display(Name = "負責人")]
    public string? BossName { get; set; }
    [Display(Name = "連絡人")]
    public string? ContactName { get; set; }
    [Display(Name = "公司電話")]
    public string? CompTel { get; set; }
    [Display(Name = "連絡電話")]
    public string? ContactTel { get; set; }
    [Display(Name = "傳真電話")]
    public string? CompFax { get; set; }
    [Display(Name = "統一編號")]
    public string? CompID { get; set; }
    [Display(Name = "電子信箱")]
    public string? ContactEmail { get; set; }
    [Display(Name = "公司地址")]
    public string? CompAddress { get; set; }
    [Display(Name = "公司網址")]
    public string? CompUrl { get; set; }

    public string? TwitterUrl { get; set; }

    public string? FacebookUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? SkypeUrl { get; set; }

    public string? LinkedinUrl { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string? AboutusText { get; set; }

    public string? SupportText { get; set; }

    public string? ReturnText { get; set; }

    public string? ShippingText { get; set; }

    public string? PaymentText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
