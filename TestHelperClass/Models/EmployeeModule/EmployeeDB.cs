using Admins_Transportation.helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TestHelperClass.Resources;

namespace TestHelperClass.Models.employeeModule
{
    internal sealed class EmployeeDB : DatabaseUtil
    {

        #region Constractor

        private static EmployeeDB instance = null;

        public static EmployeeDB GetInstance()
        {
            if (instance == null)
                return instance = new EmployeeDB();
            else return instance;
        }

        #endregion Constractor

        #region CustClasses

        private List<EmployeeData> CustEmployeesData(DataTable dtEmployees)
        {
            List<EmployeeData> employeeDatas = new List<EmployeeData>();
            for (int i = 0; i < dtEmployees.Rows.Count; i++)
            {
                employeeDatas.Add(CustOneEmployee(dtEmployees, i));
            }
            return employeeDatas;
        }

        private List<EmployeeData> CustEnEmployeesData(DataTable dtEmployees)
        {
            List<EmployeeData> employeeDatas = new List<EmployeeData>();
            for (int i = 0; i < dtEmployees.Rows.Count; i++)
            {
                employeeDatas.Add(CustEnEmployee(dtEmployees, i));
            }
            return employeeDatas;
        }

        private List<EmployeeData> CustArEmployeesData(DataTable dtEmployees)
        {
            List<EmployeeData> employeeDatas = new List<EmployeeData>();
            for (int i = 0; i < dtEmployees.Rows.Count; i++)
            {
                employeeDatas.Add(CustArEmployee(dtEmployees, i));
            }
            return employeeDatas;
        }

        private EmployeeData CustOneEmployee(DataTable dtEmployees, int i)
        {
            if (dtEmployees.Rows.Count > 0)
            {
                EmployeeData employeeData = new EmployeeData
                {
                    employeeId = Convert.ToInt32(dtEmployees.Rows[i]["employeeId"]),
                    employeeNameAR = Convert.ToString(dtEmployees.Rows[i]["employeeNameAR"]),
                    employeeCountryCode = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeCountryCode"]),
                    employeeCCName = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeCCName"]),
                    employeePhone = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeePhone"]),
                    employeePhoneCC = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeePhoneCC"]),
                    employeeAddressAR = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeAddressAR"]),
                    emmployeeArchiveStatus = MethodesConvertalClass.ConvertToBool(dtEmployees.Rows[i]["emmployeeArchiveStatus"]),
                    employNameEN = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employNameEN"]),
                    employeeAddressEN = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeAddressEN"])
                };
                return employeeData;
            }
            else
            {
                return null;
            }
        }

        private EmployeeData CustEnEmployee(DataTable dtEmployees, int i)
        {
            if (dtEmployees.Rows.Count > 0)
            {
                EmployeeData employeeData = new EmployeeData
                {
                    employeeId = Convert.ToInt32(dtEmployees.Rows[i]["employeeId"]),
                    employeeCountryCode = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeCountryCode"]),
                    employeeCCName = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeCCName"]),
                    employeePhone = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeePhone"]),
                    employeePhoneCC = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeePhoneCC"]),
                    emmployeeArchiveStatus = MethodesConvertalClass.ConvertToBool(dtEmployees.Rows[i]["emmployeeArchiveStatus"]),
                    employNameEN = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employNameEN"]),
                    employeeAddressEN = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeAddressEN"])
                };
                return employeeData;
            }
            else
            {
                return null;
            }
        }

        private EmployeeData CustArEmployee(DataTable dtEmployees, int i)
        {
            if (dtEmployees.Rows.Count > 0)
            {
                EmployeeData employeeData = new EmployeeData
                {
                    employeeId = Convert.ToInt32(dtEmployees.Rows[i]["employeeId"]),
                    employeeNameAR = Convert.ToString(dtEmployees.Rows[i]["employeeNameAR"]),
                    employeeCountryCode = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeCountryCode"]),
                    employeeCCName = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeCCName"]),
                    employeePhone = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeePhone"]),
                    employeePhoneCC = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeePhoneCC"]),
                    employeeAddressAR = MethodesConvertalClass.ConvertToString(dtEmployees.Rows[i]["employeeAddressAR"]),
                    emmployeeArchiveStatus = MethodesConvertalClass.ConvertToBool(dtEmployees.Rows[i]["emmployeeArchiveStatus"]),
                };
                return employeeData;
            }
            else
            {
                return null;
            }
        }

        #endregion CustClasses

        #region GetClasses

        public List<EmployeeData> GetAllEmployee(string lang)
        {
            List<EmployeeData> EmployeesData;
            DataTable dtEmployees;

            if (ValedationClass.IsValidCompareToStrings(lang.ToString(), Constants.LAN_EN) == true)
            {
                dtEmployees = GetanythingThroughQuery(Constants.SelectEnQuery);
                EmployeesData = CustEnEmployeesData(dtEmployees);
                foreach (var item in EmployeesData)
                return EmployeesData;
            }

            dtEmployees = GetanythingThroughQuery(Constants.SelectArQuery);
            EmployeesData = CustArEmployeesData(dtEmployees);
            return EmployeesData;
        }

        public EmployeeData GetEmployee(string textSearch, string lang)
        {
            EmployeeData employeeData;
            DataTable dtEmployee;
            string queryString;
            if (ValedationClass.IsValidCompareToStrings(lang.ToString(), Constants.LAN_EN) == true)
            {
                queryString = Constants.SelectEnQuery + " where[employeeNameAR] = N'" + textSearch + "' or[employeePhone] = N'" + textSearch + "' or[employNameEN] = N'" + textSearch + "'";
                dtEmployee = GetanythingThroughQuery(queryString);
                employeeData = CustEnEmployee(dtEmployee, 0);
                return employeeData;
            }

            queryString = Constants.SelectArQuery + " where[employeeNameAR] = N'" + textSearch + "' or[employeePhone] = N'" + textSearch + "' or[employNameEN] = N'" + textSearch + "'";
            dtEmployee = GetanythingThroughQuery(queryString);
            employeeData = CustArEmployee(dtEmployee, 0);
            return employeeData;
        }

        public EmployeeData GetEmployeeById(int employeeId)
        {
            EmployeeData employeeData;
            string queryString = Constants.SelectQuery + " where[employeeId] =" + employeeId;
            DataTable dtEmployee = GetanythingThroughQuery(queryString);
            employeeData = CustOneEmployee(dtEmployee, 0);
            return employeeData;
        }

        public EmployeeData SearchByNameEmployee(EmployeeDataContext instanceDataModel)
        {
            EmployeeData employeeData;
            string queryUpdate;
            if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
            {
                queryUpdate = Constants.SelectQuery
                    + " WHERE [employNameEN] = N'" + instanceDataModel.employeeName.Trim() + "'";
            }
            else
            {
                queryUpdate = Constants.SelectQuery
                    + " WHERE [employeeNameAR] = N'" + instanceDataModel.employeeName.Trim() + "'";
            }
            DataTable dtEmployee = GetanythingThroughQuery(queryUpdate);
            employeeData = CustOneEmployee(dtEmployee, 0);
            return employeeData;
        }

        public EmployeeData SearchByPhoneEmployee(EmployeeDataContext instanceDataModel)
        {
            EmployeeData employeeData;

            string queryUpdate = Constants.SelectQuery
                + " WHERE [employeePhoneCC] = N'" + MethodesClass.ConcatonatePhoneWithCc(instanceDataModel.employeePhone,instanceDataModel.employeeCCName) + "' and [employeeId] != N'" + instanceDataModel.employeeId + "'";
            DataTable dtEmployee = GetanythingThroughQuery(queryUpdate);
            employeeData = CustOneEmployee(dtEmployee, 0);
            return employeeData;
        }

        #endregion GetClasses

        #region PostClasses

        public int AddEmployee(EmployeeDataContext instanceDataModel)
        {
            int isAdded;
            string queryInsert;
            string queryValues;
            //employeePhoneCC 
            if (instanceDataModel.employeeAddress != null)
            {
                queryInsert = "INSERT INTO [dbo].[tbEmployees]"
                    + "([employeeNameAR]"
                    + ",[employNameEN]"
                    + ",[employeePhoneCC]"
                    + ",[employeeAddressEN]"
                    + ",[employeeCountryCode]"
                    + ",[employeeCCName]"
                    + ",[employeePhone]"
                    + ",[employeeAddressAR]"
                    + ",[emmployeeArchiveStatus])";

                queryValues = " " + "VALUES(N'" + instanceDataModel.employeeNameAR.Trim()
                    + "',N'" + instanceDataModel.employNameEN.Trim()
                    + "',N'" + instanceDataModel.employeePhoneCC.Trim()
                    + "',N'" + instanceDataModel.employeeAddressEN.Trim()
                    + "',N'" + instanceDataModel.employeeCountryCode.Trim()
                    + "',N'" + instanceDataModel.employeeCCName.Trim()
                    + "',N'" + instanceDataModel.employeePhone.Trim()
                    + "',N'" + instanceDataModel.employeeAddressAR.Trim()
                    + "',N'" + instanceDataModel.emmployeeArchiveStatus
                    + "')";

                isAdded = ExecuteNonQuery(queryInsert + queryValues);
                return isAdded;
            }

            queryInsert = "INSERT INTO [dbo].[tbEmployees]"
                + "([employeeNameAR]"
                + ",[employNameEN]"
                + ",[employeePhoneCC]"
                + ",[employeeCountryCode]"
                + ",[employeeCCName]"
                + ",[employeePhone]"
                + ",[emmployeeArchiveStatus])";

            queryValues = " " + "VALUES(N'" + instanceDataModel.employeeNameAR.Trim()
                + "',N'" + instanceDataModel.employNameEN.Trim()
                + "',N'" + instanceDataModel.employeePhoneCC.Trim()
                + "',N'" + instanceDataModel.employeeCountryCode.Trim()
                + "',N'" + instanceDataModel.employeeCCName.Trim()
                + "',N'" + instanceDataModel.employeePhone.Trim()
                + "',N'" + instanceDataModel.emmployeeArchiveStatus
                + "')";

            isAdded = ExecuteNonQuery(queryInsert + queryValues);
            return isAdded;
        }

        public int UpdateEmployee(EmployeeDataContext instanceDataModel)
        {
            int isUpdated;
            String queryUpdate;

            if (instanceDataModel.employeeAddress != null)
            {
                queryUpdate = Constants.UpdateQuery
                    + "SET [employeeNameAR] = N'" + instanceDataModel.employeeNameAR.Trim()
                    + "',[employNameEN] = N'" + instanceDataModel.employNameEN.Trim()
                    + "',[employeeAddressEN] = N'" + instanceDataModel.employeeAddressEN.Trim()
                    + "',[employeeCountryCode] = N'" + instanceDataModel.employeeCountryCode.Trim()
                    + "',[employeeCCName] = N'" + instanceDataModel.employeeCCName.Trim()
                    + "',[employeePhone] = N'" + instanceDataModel.employeePhone.Trim()
                    + "',[employeePhoneCC] = N'" + instanceDataModel.employeePhoneCC.Trim()
                    + "',[employeeAddressAR] = N'" + instanceDataModel.employeeAddressAR.Trim()
                    + "',[emmployeeArchiveStatus] = N'" + instanceDataModel.emmployeeArchiveStatus
                    + "' WHERE [employeeId] = N'" + instanceDataModel.employeeId + "'";
                isUpdated = ExecuteNonQuery(queryUpdate);
                return isUpdated;
            }

            queryUpdate = Constants.UpdateQuery
                + "SET [employeeNameAR] = N'" + instanceDataModel.employeeNameAR.Trim()
                + "',[employNameEN] = N'" + instanceDataModel.employNameEN.Trim()
                + "',[employeeCountryCode] = N'" + instanceDataModel.employeeCountryCode.Trim()
                + "',[employeeCCName] = N'" + instanceDataModel.employeeCCName.Trim()
                + "',[employeePhone] = N'" + instanceDataModel.employeePhone.Trim()
                + "',[employeePhoneCC] = N'" + instanceDataModel.employeePhoneCC.Trim()
                + "',[emmployeeArchiveStatus] = N'" + instanceDataModel.emmployeeArchiveStatus
                + "' WHERE [employeeId] = N'" + instanceDataModel.employeeId + "'";
            isUpdated = ExecuteNonQuery(queryUpdate);
            return isUpdated;
        }

        public int ArchiveEmployee(int employeeId)
        {
            int isArchived;
            string queryUpdate = Constants.UpdateQuery
                + "SET [emmployeeArchiveStatus] = N'" + 1
                + "' WHERE [employeeId] = N'" + employeeId + "' and [emmployeeArchiveStatus] = N'" + 0 + "'";

            isArchived = ExecuteNonQuery(queryUpdate);
            return isArchived;
        }

        public int RestorEmployee(int employeeId)
        {
            string queryUpdate = Constants.UpdateQuery
                + "SET [emmployeeArchiveStatus] = N'" + 0
                + "' WHERE [employeeId] = N'" + employeeId + "' and [emmployeeArchiveStatus] = N'" + 1 + "'";

            int isRestored = ExecuteNonQuery(queryUpdate);
            return isRestored;
        }

        public int DeleteEmployee(int employeeId)
        {
            string queryUpdate = "DELETE FROM [dbo].[tbEmployees]"
                + " WHERE [employeeId] = N'" + employeeId + "'";

            int isRestored = ExecuteNonQuery(queryUpdate);
            return isRestored;
        }

        #endregion PostClasses

    }
}