using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestHelperClass.Resources
{
    public static class Constants
    {
        public static readonly int SUCCEDED_STATUS = 1;
        public static readonly int FAILED_STATUS = 0;

        public static readonly string LAN_AR = "ar";
        public static readonly string LAN_EN = "en";

        public static readonly string SelectQuery = "SELECT * FROM [dbo].[tbEmployees]";
        public static readonly string UpdateQuery = "UPDATE [dbo].[tbEmployees]";
        public static readonly string SelectEnQuery = "SELECT [employeeId] " +
                    ",[employeeCountryCode]" +
                    ",[employeeCCName]" +
                    ",[employeePhone]" +
                    ",[employeePhoneCC]" +
                    ",[emmployeeArchiveStatus]" +
                    ",[employNameEN]" +
                    ",[employeeAddressEN] FROM [dbo].[tbEmployees]";
        public static readonly string SelectArQuery = "SELECT [employeeId] " +
                ",[employeeNameAR]" +
                ",[employeeCountryCode]" +
                ",[employeeCCName]" +
                ",[employeePhone]" +
                ",[employeePhoneCC]" +
                ",[employeeAddressAR]" +
                ",[emmployeeArchiveStatus]" +
                "FROM [dbo].[tbEmployees]";

    }
}