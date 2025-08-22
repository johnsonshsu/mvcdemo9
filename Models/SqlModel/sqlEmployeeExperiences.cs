using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcdemo9.Models
{
    public class z_sqlEmployeeExperiences : DapperSql<EmployeeExperiences>
    {
        public z_sqlEmployeeExperiences()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "EmployeeExperiences.EmpNo, EmployeeExperiences.StartDate";
            DefaultOrderByDirection = "ASC,ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public List<EmployeeExperiences> GetDataList(string empNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<EmployeeExperiences>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE EmployeeExperiences.EmpNo = @EmpNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("EmpNo", empNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<EmployeeExperiences>(sql_query, parm);
            return model;
        }
    }
}