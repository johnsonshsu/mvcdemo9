using System;
using System.Collections.Generic;

namespace mvcdemo9.Models
{
    [ModelMetadataType(typeof(z_metaEmployees))]
    public partial class Employees
    {
        [NotMapped]
        [Display(Name = "性別")]
        public string? GenderName { get; set; }
        [NotMapped]
        [Display(Name = "部門名稱")]
        public string? DeptName { get; set; }
        [NotMapped]
        [Display(Name = "職稱")]
        public string? TitleName { get; set; }
        [NotMapped]
        [Display(Name = "證照項目")]
        public string? CertificateName { get; set; }
        [NotMapped]
        [Display(Name = "關係")]
        public string? RelationName { get; set; }
    }
}

public class z_metaEmployees
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsValid { get; set; }
    [Display(Name = "員工編號")]
    [Required(ErrorMessage = "員工編號不可空白!!")]
    public string? EmpNo { get; set; }
    [Display(Name = "員工姓名")]
    [Required(ErrorMessage = "員工姓名不可空白!!")]
    public string? EmpName { get; set; }
    [Display(Name = "性別代號")]
    public string? GenderCode { get; set; }
    [Display(Name = "部門代號")]
    public string? DeptNo { get; set; }
    [Display(Name = "職稱代號")]
    public string? TitleNo { get; set; }
    [Display(Name = "出生日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? Birthday { get; set; }
    [Display(Name = "到職日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? OnboardDate { get; set; }
    [Display(Name = "離職日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? LeaveDate { get; set; }
    [Display(Name = "電子信箱")]
    [EmailAddress(ErrorMessage = "電子信箱格式錯誤!!")]
    public string? ContactEmail { get; set; }
    [Display(Name = "通訊電話")]
    public string? ContactTel { get; set; }
    [Display(Name = "通訊地址")]
    public string? ContactAddress { get; set; }
    [Display(Name = "身份證字號")]
    public string? IdentityID { get; set; }
    [Display(Name = "戶籍電話")]
    public string? RegisterTel { get; set; }
    [Display(Name = "戶籍地址")]
    public string? RegisterAddress { get; set; }
    [Display(Name = "緊急連絡人")]
    public string? UrgentName { get; set; }
    [Display(Name = "緊急連絡電話")]
    public string? UrgentTel { get; set; }
    [Display(Name = "關係")]
    public string? RelationNo { get; set; }
    [Display(Name = "證照項目")]
    public string? CertificateNo { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}