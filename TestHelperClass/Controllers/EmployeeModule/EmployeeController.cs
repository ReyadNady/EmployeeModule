using Admins_Transportation.helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TestHelperClass.Models.employeeModule;
using TestHelperClass.Resources;

namespace TestHelperClass.Controllers.employeeModule
{
    [RoutePrefix("EmployeeController")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeDB employeeDB;
        private readonly EmployeeValid employeeValid;

        public EmployeeController()
        {
            employeeDB = EmployeeDB.GetInstance();
            employeeValid = EmployeeValid.GetInstance();
        }

        [HttpPost, Route("AddEmployee")]
        public HttpResponseMessage AddEmployee([FromBody] EmployeeDataContext instanceDataModel)
        {
            try
            {
                if (instanceDataModel == null || instanceDataModel.language == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPLOYEE_NULL_ERROR, data = Messages.EMPTY });
                }

                string isValid = employeeValid.IsValidEmployee(instanceDataModel);
                if (ValedationClass.IsValidCompareToStrings(isValid, true.ToString()) == true)
                {
                    EmployeeData isDubleName = employeeDB.SearchByNameEmployee(instanceDataModel);
                    if (isDubleName != null)
                    {
                        if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.NAME_EN_IS_EXIST, data = Messages.EMPTY });
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.NAME_IS_EXIST, data = Messages.EMPTY });
                        }
                    }
                    else
                    {
                        EmployeeData isDublePhone = employeeDB.SearchByPhoneEmployee(instanceDataModel);
                        if (isDublePhone != null)
                        {
                            if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.PHONE_EN_IS_EXIST, data = Messages.EMPTY });
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.PHONE_IS_EXIST, data = Messages.EMPTY });
                            }
                        }
                        else
                        {
                            //Translation
                            employeeValid.Translation(instanceDataModel);
                            int isAdd = employeeDB.AddEmployee(instanceDataModel);
                            if (isAdd > 0)
                            {
                                if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_EN_MESSAGES, data = Messages.EMPTY });
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_MESSAGES, data = Messages.EMPTY });
                                }
                            }
                            else
                            {
                                if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.ADD_EMPLOYEE_EN_ERROR, data = Messages.EMPTY });
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.ADD_EMPLOYEE_ERROR, data = Messages.EMPTY });
                                }
                            }
                        }
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = isValid, data = Messages.EMPTY });
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.ADD_EMPLOYEE_ERROR, data = Messages.EMPTY });
            }
        }

        [HttpGet, Route("GetAllEmployee")]
        public HttpResponseMessage GetAllEmployee([FromUri] EmployeeDataContext instanceDataModel)
        {
            List<EmployeeData> dtEmployees;
            try
            {
                if (instanceDataModel == null || instanceDataModel.language == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPLOYEE_NULL_ERROR, data = Messages.EMPTY });
                }

                dtEmployees = employeeDB.GetAllEmployee(instanceDataModel.language);

                if (dtEmployees.Count > 0)
                {
                    if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_EN_MESSAGES, data = dtEmployees });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_MESSAGES, data = dtEmployees });
                    }
                }
                else
                {
                    if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.GETALL_EMPLOYEE_EN_ERROR, data = Messages.EMPTY });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.GETALL_EMPLOYEE_ERROR, data = Messages.EMPTY });
                    }
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.GETALL_EMPLOYEE_ERROR, data = Messages.EMPTY });
            }
        }

        [HttpGet, Route("GetEmployee")]
        public HttpResponseMessage GetEmployee([FromUri] EmployeeDataContext instanceDataModel)
        {
            try
            {
                if (instanceDataModel == null || instanceDataModel.language == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPLOYEE_NULL_ERROR, data = Messages.EMPTY });
                }

                if (instanceDataModel.filterStatus == true)
                {
                    if ((ValedationClass.IsValidNameAr(instanceDataModel.textSearch) || ValedationClass.IsValidNameEn(instanceDataModel.textSearch)) == true)
                    {
                        EmployeeData dtEmployees = employeeDB.GetEmployee(instanceDataModel.textSearch,instanceDataModel.language);
                        if (dtEmployees != null)
                        {
                            if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_EN_MESSAGES, data = dtEmployees });
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_MESSAGES, data = dtEmployees });
                            }
                        }
                        else
                        {
                            if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.NAME_NOT_EN_EXIST, data = Messages.EMPTY });
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.NAME_NOT_EXIST, data = Messages.EMPTY });
                            }
                        }
                    }
                    else
                    {
                        if (ValedationClass.IsValidNumber(instanceDataModel.textSearch) == true)
                        {
                            EmployeeData dtEmployees = employeeDB.GetEmployee(instanceDataModel.textSearch,instanceDataModel.language);
                            if (dtEmployees != null)
                            {
                                if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_EN_MESSAGES, data = dtEmployees });
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_MESSAGES, data = dtEmployees });
                                }
                            }
                            else
                            {
                                if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.PHONE_EN_NOT_EXIST, data = Messages.EMPTY });
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.PHONE_NOT_EXIST, data = Messages.EMPTY });
                                }
                            }
                        }
                        else
                        {
                            if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.CORRECT_EN_NAME_AND_PHONE, data = Messages.EMPTY });
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.CORRECT_NAME_AND_PHONE, data = Messages.EMPTY });
                            }
                        }
                    }
                }
                else
                {
                    return GetAllEmployee(instanceDataModel);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EMPLOYEE_ERROR, data = Messages.EMPTY });
            }
        }

        [HttpPost, Route("UpdateEmployee")]
        public HttpResponseMessage UpdateEmployee([FromBody] EmployeeDataContext instanceDataModel)
        {
            try
            {
                if (instanceDataModel == null || instanceDataModel.language == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPLOYEE_NULL_ERROR, data = Messages.EMPTY });
                }

                string isValid = employeeValid.IsValidEmployee(instanceDataModel);
                if (ValedationClass.IsValidCompareToStrings(isValid, true.ToString()) == true)
                {
                    EmployeeData dtEmployee = employeeDB.GetEmployeeById(instanceDataModel.employeeId);
                    if (dtEmployee != null)
                    {
                        EmployeeData isDubleName = employeeDB.SearchByNameEmployee(instanceDataModel);
                        if (isDubleName != null)
                        {
                            if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.NAME_EN_IS_EXIST, data = Messages.EMPTY });
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.NAME_IS_EXIST, data = Messages.EMPTY });
                            }
                        }
                        else
                        {
                            EmployeeData isDublePhone = employeeDB.SearchByPhoneEmployee(instanceDataModel);
                            if (isDublePhone != null)
                            {
                                if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.PHONE_EN_IS_EXIST, data = Messages.EMPTY });
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.PHONE_IS_EXIST, data = Messages.EMPTY });
                                }
                            }
                            else
                            {
                                employeeValid.Translation(instanceDataModel);
                                int isUpdated = employeeDB.UpdateEmployee(instanceDataModel);
                                if (isUpdated > 0)
                                {
                                    if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_EN_MESSAGES, data = Messages.EMPTY });
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_MESSAGES, data = Messages.EMPTY });
                                    }
                                }
                                else
                                {
                                    if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.UPDATED_EN_FAILED, data = Messages.EMPTY });
                                    }
                                    else
                                    {
                                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.UPDATED_FAILED, data = Messages.EMPTY });
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EN_EMPLOYEE_ERROR, data = Messages.EMPTY });
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EMPLOYEE_ERROR, data = Messages.EMPTY });
                        }
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = isValid, data = Messages.EMPTY });
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EMPLOYEE_ERROR, data = Messages.EMPTY });
            }
        }

        [HttpPost, Route("ArchiveEmployee")]
        public HttpResponseMessage ArchiveEmployee([FromBody] EmployeeDataContext instanceDataModel)
        {
            try
            {
                if (instanceDataModel == null || instanceDataModel.language == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPLOYEE_NULL_ERROR, data = Messages.EMPTY });
                }

                EmployeeData dtEmployee = employeeDB.GetEmployeeById(instanceDataModel.employeeId);
                if (dtEmployee != null)
                {
                    int isArchived = employeeDB.ArchiveEmployee(instanceDataModel.employeeId);
                    if (isArchived > 0)
                    {
                        if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_EN_MESSAGES, data = Messages.EMPTY });
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_MESSAGES, data = Messages.EMPTY });
                        }
                    }
                    else
                    {
                        if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.ARCHIVE_EN_DB_ERROR, data = Messages.EMPTY });
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.ARCHIVE_DB_ERROR, data = Messages.EMPTY });
                        }
                    }
                }
                else
                {
                    if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EN_EMPLOYEE_ERROR, data = Messages.EMPTY });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EMPLOYEE_ERROR, data = Messages.EMPTY });
                    }
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EMPLOYEE_ERROR, data = Messages.EMPTY });
            }
        }

        [HttpPost, Route("RestorEmployee")]
        public HttpResponseMessage RestorEmployee([FromBody] EmployeeDataContext instanceDataModel)
        {
            try
            {
                if (instanceDataModel == null || instanceDataModel.language == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPLOYEE_NULL_ERROR, data = Messages.EMPTY });
                }

                EmployeeData dtEmployee = employeeDB.GetEmployeeById(instanceDataModel.employeeId);
                if (dtEmployee != null)
                {
                    int isRestored = employeeDB.RestorEmployee(instanceDataModel.employeeId);
                    if (isRestored > 0)
                    {
                        if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_EN_MESSAGES, data = Messages.EMPTY });
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_MESSAGES, data = Messages.EMPTY });
                        }
                    }
                    else
                    {
                        if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.RESTORE_EN_ERROR, data = Messages.EMPTY });
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.RESTORE_ERROR, data = Messages.EMPTY });
                        }
                    }
                }
                else
                {
                    if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EN_EMPLOYEE_ERROR, data = Messages.EMPTY });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EMPLOYEE_ERROR, data = Messages.EMPTY });
                    }
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EMPLOYEE_ERROR, data = Messages.EMPTY });
            }
        }


        [HttpPost, Route("DeleteEmployee")]
        public HttpResponseMessage DeleteEmployee([FromBody] EmployeeDataContext instanceDataModel)
        {
            try
            {
                if (instanceDataModel == null || instanceDataModel.language == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPLOYEE_NULL_ERROR, data = Messages.EMPTY });
                }

                EmployeeData dtEmployee = employeeDB.GetEmployeeById(instanceDataModel.employeeId);
                if (dtEmployee != null)
                {
                    int isDelete = employeeDB.DeleteEmployee(instanceDataModel.employeeId);
                    if (isDelete > 0)
                    {
                        if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_EN_MESSAGES, data = Messages.EMPTY });
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.SUCCEDED_STATUS, msg = Messages.SUCCEDED_MESSAGES, data = Messages.EMPTY });
                        }
                    }
                    else
                    {
                        if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.DELETE_EN_ERROR, data = Messages.EMPTY });
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.DELETE_ERROR, data = Messages.EMPTY });
                        }
                    }
                }
                else
                {
                    if (ValedationClass.IsValidCompareToStrings(instanceDataModel.language.ToString(), Constants.LAN_EN) == true)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EN_EMPLOYEE_ERROR, data = Messages.EMPTY });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EMPLOYEE_ERROR, data = Messages.EMPTY });
                    }
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { status = Constants.FAILED_STATUS, msg = Messages.EMPTY_EMPLOYEE_ERROR, data = Messages.EMPTY });
            }
        }


        //Upload Image
        [HttpPost, Route("UploadImage")]
        public async Task<string> UploadImage()
        {

            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var file in provider.FileData)
                {
                    var name = file.Headers.ContentDisposition.FileName;
                    name = name.Trim('"');
                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, name);
                    File.Move(localFileName, filePath);
                }
            }
            catch
            {
                return "Failed";
            }

            return "Done Uploaded !";

        }
    }
}