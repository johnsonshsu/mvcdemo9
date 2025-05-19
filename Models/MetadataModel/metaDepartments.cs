using System;
using System.Collections.Generic;

namespace mvcdemo9.Models
{
    [ModelMetadataType(typeof(z_metaDepartments))]
    public partial class Departments
    {

    }
}

public class z_metaDepartments
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "代號")]
    [Required(ErrorMessage = "登入不可空白!!")]
    public string? DeptNo { get; set; }
    [Display(Name = "名稱")]
    [Required(ErrorMessage = "名稱不可空白!!")]
    public string? DeptName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}