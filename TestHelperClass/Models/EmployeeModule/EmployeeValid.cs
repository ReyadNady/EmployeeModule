using Admins_Transportation.helper;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TestHelperClass.Resources;
using Ubiety.Dns.Core;

namespace TestHelperClass.Models.employeeModule
{
    internal sealed class EmployeeValid : ValedationClass
    {
        private static EmployeeValid instance = null;

        public static EmployeeValid GetInstance()
        {
            if (instance == null)
                return instance = new EmployeeValid();
            else return instance;
        }

        public string IsValidEmployee(EmployeeDataContext instanceDataModel)
        {
            if (instanceDataModel != null)
            {
                if (instanceDataModel.language == null || instanceDataModel.language == " ")
                {
                    return Messages.LANG_ERROR;
                }
                else if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_AR) == true)
                {
                    if (ValedationClass.IsValidNameAr(instanceDataModel.employeeName) == false)
                        return Messages.NAME_AR_ERROR;
                    if (instanceDataModel.employeeAddress != null)
                    {
                        if (ValedationClass.IsValidTextAr(instanceDataModel.employeeAddress) == false)
                            return Messages.ADRESS_AR_ERROR;
                    }

                    if (ValedationClass.IsValidPhone(instanceDataModel.employeePhone, instanceDataModel.employeeCountryCode, instanceDataModel.employeeCCName) == false)
                        return Messages.PHONE_AR_ERROR;

                    return true.ToString();
                }
                else if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                {
                    if (ValedationClass.IsValidNameEn(instanceDataModel.employeeName) == false)
                        return Messages.NAME_EN_ERROR;
                    if (instanceDataModel.employeeAddress != null)
                    {
                        if (ValedationClass.IsValidTextEn(instanceDataModel.employeeAddress) == false)
                            return Messages.ADRESS_EN_ERROR;
                    }

                    if (ValedationClass.IsValidPhone(instanceDataModel.employeePhone, instanceDataModel.employeeCountryCode, instanceDataModel.employeeCCName) == false)
                        return Messages.PHONE_EN_ERROR;

                    return true.ToString();
                }
                else
                {
                    return Messages.LANG_ERROR;
                }

            }
            else
                return Messages.LIST_ERROR;
        }

        // Translation Method
        public void Translation(EmployeeDataContext instanceDataModel)
        {
            if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_AR) == true)
            {
                instanceDataModel.employNameEN = TranslatorGoogleClass.Translate(instanceDataModel.employeeName, Constants.LAN_AR, Constants.LAN_EN);
                instanceDataModel.employeeNameAR = instanceDataModel.employeeName;
                instanceDataModel.employeePhoneCC = MethodesClass.ConcatonatePhoneWithCc(instanceDataModel.employeePhone, instanceDataModel.employeeCCName);

                if (instanceDataModel.employeeAddress != null)
                {
                    instanceDataModel.employeeAddressEN = TranslatorGoogleClass.Translate(instanceDataModel.employeeAddress, Constants.LAN_AR, Constants.LAN_EN);
                    instanceDataModel.employeeAddressAR = instanceDataModel.employeeAddress;
                    instanceDataModel.employeePhoneCC = MethodesClass.ConcatonatePhoneWithCc(instanceDataModel.employeePhone, instanceDataModel.employeeCCName);
                }

            }
            else
            {
                instanceDataModel.employeeNameAR = TranslatorGoogleClass.Translate(instanceDataModel.employeeName, Constants.LAN_EN, Constants.LAN_AR);
                instanceDataModel.employNameEN = instanceDataModel.employeeName;
                instanceDataModel.employeePhoneCC = MethodesClass.ConcatonatePhoneWithCc(instanceDataModel.employeePhone, instanceDataModel.employeeCCName);

                if (instanceDataModel.employeeAddress != null)
                {
                    instanceDataModel.employeeAddressAR = TranslatorGoogleClass.Translate(instanceDataModel.employeeAddress, Constants.LAN_EN, Constants.LAN_AR);
                    instanceDataModel.employeeAddressEN = instanceDataModel.employeeAddress;
                    instanceDataModel.employeePhoneCC = MethodesClass.ConcatonatePhoneWithCc(instanceDataModel.employeePhone, instanceDataModel.employeeCCName);
                }
            }
        }
    }
}