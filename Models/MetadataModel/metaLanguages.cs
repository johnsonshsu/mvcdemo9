using System;
using System.Collections.Generic;

namespace mvcdemo9.Models
{
    [ModelMetadataType(typeof(z_metaLanguages))]
    public partial class Languages
    {

    }
}

public class z_metaLanguages
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "語言編號")]
    [Required(ErrorMessage = "語言編號為必填欄位")]
    public string? LangNo { get; set; }
    [Display(Name = "語言名稱")]
    [Required(ErrorMessage = "語言名稱為必填欄位")]
    public string? LangName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
