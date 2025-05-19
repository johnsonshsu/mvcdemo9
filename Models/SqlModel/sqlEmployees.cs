using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcdemo9.Models
{
    public class z_sqlEmployees : DapperSql<Employees>
    {
        public z_sqlEmployees()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Employees.EmpNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Employees.Id, Employees.IsValid, Employees.EmpNo, Employees.EmpName, Employees.GenderCode, 
vi_CodeGender.CodeName AS GenderName, Employees.DeptNo, Departments.DeptName, Employees.TitleNo, 
Titles.TitleName, Employees.Birthday, Employees.OnboardDate, Employees.LeaveDate, Employees.ContactEmail, 
Employees.ContactTel, Employees.ContactAddress, Employees.IdentityID, Employees.RegisterTel, 
Employees.RegisterAddress, Employees.UrgentName, Employees.UrgentTel, Employees.RelationNo, 
vi_CodeRelation.CodeName AS RelationName, Employees.CertificateNo, 
vi_CodeCertificate.CodeName AS CertificateName, Employees.Remark
FROM Employees 
LEFT OUTER JOIN vi_CodeCertificate ON Employees.CertificateNo = vi_CodeCertificate.CodeNo 
LEFT OUTER JOIN vi_CodeRelation ON Employees.RelationNo = vi_CodeRelation.CodeNo 
LEFT OUTER JOIN vi_CodeGender ON Employees.GenderCode = vi_CodeGender.CodeNo 
LEFT OUTER JOIN Departments ON Employees.DeptNo = Departments.DeptNo 
LEFT OUTER JOIN Titles ON Employees.TitleNo = Titles.TitleNo 
";
            return str_query;
        }

        public override Employees GetData(string dataNo)
        {
            string sql_query = GetSQLSelect();
            sql_query += " WHERE Employees.EmpNo = @DataNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("DataNo", dataNo);
            var model = dpr.ReadSingle<Employees>(sql_query, parm);
            return model;
        }

        public override List<string> GetSearchColumns()
        {
            //由系統自動取得文字欄位的集合
            SearchColumns = base.GetSearchColumns();
            SearchColumns.Add("vi_CodeCertificate.CodeName");
            SearchColumns.Add("vi_CodeRelation.CodeName");
            SearchColumns.Add("vi_CodeGender.CodeName");
            SearchColumns.Add("Departments.DeptName");
            SearchColumns.Add("Titles.TitleName");
            return SearchColumns;
        }
    }
}