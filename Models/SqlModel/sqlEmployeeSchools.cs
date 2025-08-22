using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcdemo9.Models
{
    public class z_sqlEmployeeSchools : DapperSql<EmployeeSchools>
    {
        public z_sqlEmployeeSchools()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "EmployeeSchools.EmpNo, EmployeeSchools.StartDate";
            DefaultOrderByDirection = "ASC,ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public List<EmployeeSchools> GetDataList(string empNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<EmployeeSchools>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE EmployeeSchools.EmpNo = @EmpNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(EntityObject, searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                parm.Add("EmpNo", empNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<EmployeeSchools>(sql_query, parm);
            return model;
        }
    }
}