using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestHelperClass.Models.employeeModule
{
    public class EmployeeDataContext
    {
        public int employeeId { get; set; }
        public string employeeName { get; set; }                //Main
        public string employeeNameAR { get; set; }              //AR
        public string employNameEN { get; set; }                //EN
        public string employeeCountryCode { get; set; }
        public string employeeCCName { get; set; }
        public string employeePhone { get; set; }
        public string employeePhoto { get; set; }
        public string employeePhoneCC { get; set; }
        public string employeeAddress { get; set; }             //Main
        public string employeeAddressAR { get; set; }           //AR
        public string employeeAddressEN { get; set; }           //En
        public bool emmployeeArchiveStatus { get; set; }
        public bool filterStatus { get; set; }
        public string textSearch { get; set; }
        public string language { get; set; }
    }

    public class EmployeeData
    {
        public int employeeId { get; set; }
        public string employeeNameAR { get; set; }              //AR
        public string employNameEN { get; set; }                //EN
        public string employeeCountryCode { get; set; }
        public string employeeCCName { get; set; }
        public string employeePhone { get; set; }
        public string employeePhoto { get; set; }
        public string employeePhoneCC { get; set; }
        public string employeeAddressAR { get; set; }           //AR
        public string employeeAddressEN { get; set; }           //En
        public bool emmployeeArchiveStatus { get; set; }
    }
}