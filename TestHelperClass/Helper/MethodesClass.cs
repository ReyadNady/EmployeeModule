using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace Admins_Transportation.helper
{
    public static class MethodesClass
    {
        /*Cerated by Ahemd Abd El Kraim*/

        #region EncryptMD5

        public static string Encrypt_MD5(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ConstansValuesClass.securityKey));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        #endregion EncryptMD5

        #region DecryptMD5

        public static string Decrypt_MD5(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ConstansValuesClass.securityKey));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }

        #endregion DecryptMD5

        #region Encodedbase64

        public static string Encodedbase64(string Value)
        {
            try
            {
                string base64Encoded = Value;
                string result;
                byte[] data = System.Convert.FromBase64String(base64Encoded);
                result = System.Text.ASCIIEncoding.ASCII.GetString(data);
                return result;
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
            return Value;
        }

        #endregion Encodedbase64

        #region Decodedbase64

        public static string Decodedbase64(string Value)
        {
            try
            {
                string base64Decoded = Value;
                string result;
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(base64Decoded);
                result = System.Convert.ToBase64String(data);
                return result;
            }
            catch (Exception e1)
            {
                e1.Message.ToString();
            }
            return Value;
        }

        #endregion Decodedbase64

        #region ItemsDuplicateInList

        public static List<int> ItemsDuplicateInList(List<int> input)
        {
            if (input != null && input.Count > 0)
                return input.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            else
                return new List<int>();
        }

        #endregion ItemsDuplicateInList

        #region Funcation Get End Date in month

        //public static string GetEndDateInMonth(int year, int month)
        //{
        //    string str = year + "/" + month + "/" + "01";
        //    DateTime startDay = Convert.ToDateTime(str);
        //    var endDay = startDay.AddMonths(1).AddDays(-1);
        //    return ChangeFormateDate(endDay.ToString(), ConstansValuesClass.formatDate);
        //}
        public static string GetEndDateInMonth(int startDateInMonth)
        {
            DateTime Now = Convert.ToDateTime(DateTimeNow());
            string str = Now.Year + "/" + Now.Month + "/" + "01";
            DateTime startDay = Convert.ToDateTime(str);
            var endDay = startDay.AddMonths(1).AddDays(-1);
            return ChangeFormateDate(endDay.ToString(), ConstansValuesClass.formatDate);
        }
        public static string GetStartDateInMonth(int startDateInMonth)
        {
            if (startDateInMonth <= 0 || startDateInMonth > 32)
                startDateInMonth = 1;
            string str = "";
            DateTime Now = Convert.ToDateTime(DateTimeNow());
            str = Now.Year + "/" + Now.Month + "/" + startDateInMonth;
            str = ChangeFormateDate(str, ConstansValuesClass.formatDate);
            return str;
        }

        #endregion Funcation Get End Date in month

        #region Function Date

        public static string DateNow()
        {
            DateTime now = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(ConstansValuesClass.StandardTimeZone));
            return now.ToString(ConstansValuesClass.formatDate);
        }

        public static string TimeNow()

        {
            DateTime now = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(ConstansValuesClass.StandardTimeZone));
            return now.ToString(ConstansValuesClass.formatTime);
        }

        public static string DateTimeNow()
        {
            DateTime now = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(ConstansValuesClass.StandardTimeZone));
            return now.ToString(ConstansValuesClass.formatDateTime);
        }

        public static string ChangeFormateDate(string date, string format)
        {
            try
            {
                DateTime oDate = Convert.ToDateTime(date);
                return oDate.ToString(format);
                //if (format.ToLower() == "yyyy-MM-dd".ToLower())
                //{
                //    return oDate.ToString("yyyy-MM-dd");
                //}
                //if (format.ToLower() == "dd/MM/yyyy".ToLower())
                //{
                //    return oDate.ToString("dd/MM/yyyy");
                //}
                //if (format.ToLower() == "yyyy/MM/dd".ToLower())
                //{
                //    return oDate.ToString("yyyy/MM/dd");
                //}
                //if (format.ToLower() == "yyyy/MM/dd HH:mm:ss".ToLower())
                //{
                //    return oDate.ToString("yyyy/MM/dd HH:mm:ss");
                //}
                //if (format.ToLower() == "HH:mm:ss".ToLower())
                //{
                //    return oDate.ToString("HH:mm:ss");
                //}
                //if (format.ToLower() == "HH:mm".ToLower())
                //{
                //    return oDate.ToString("HH:mm");
                //}
                //if (format.ToLower() == "hh:mm:ss tt".ToLower())
                //{
                //    return oDate.ToString("hh:mm:ss tt");
                //}
                //if (format.ToLower() == "hh:mm tt".ToLower())
                //{
                //    return oDate.ToString("hh:mm tt");
                //}
                //return oDate.ToString();
            }
            catch
            {
                return "";
            }
        }

        #endregion Function Date

        #region CalculateAge

        public static string CalculateAge(string date)
        {
            DateTime dateOfBirth = Convert.ToDateTime(DateTimeNow());
            int ageInYears = 0;
            int ageInMonths = 0;
            int ageInDays = 0;
            if (ValedationClass.IsValiDateTime(date, ConstansValuesClass.formatDate) == true)
            {
                dateOfBirth = Convert.ToDateTime(date);
                DateTime currentDate = Convert.ToDateTime(DateTimeNow());
                TimeSpan difference = currentDate.Subtract(dateOfBirth);

                // This is to convert the timespan to datetime object
                DateTime age = DateTime.MinValue + difference;

                // Min value is 01/01/0001
                // Actual age is say 24 yrs, 9 months and 3 days represented as timespan
                // Min Valye + actual age = 25 yrs , 10 months and 4 days.
                // subtract our addition or 1 on all components to get the actual date.

                ageInYears = age.Year - 1;
                ageInMonths = age.Month - 1;
                ageInDays = age.Day - 1;
            }
            return ConvertNumberToArFormat(ageInYears + " سنوات ," + ageInMonths + " شهر , " + ageInDays + " أيام");
        }

        #endregion CalculateAge

        #region RefactorPathImage

        public static string RefactorPathImage(string root, string localPathImage)
        {
            string imagePath = "";
            imagePath = imagePath.Replace(root, localPathImage);
            imagePath = imagePath.Replace("\\", "/");
            return imagePath;
        }

        #endregion RefactorPathImage

        #region Function Generate a random number between two numbers

        // Generate a random number between two numbers
        public static int RandomNumber()
        {
            int result;
            Random random = new Random();
            int value = random.Next(000000, 999999);
            if (value.ToString().Length == 6)
            {
                result = value;
                return result;
            }
            else
            {
                result = value;
                for (int i = 0; i <= 6; i++)
                {
                    for (int x = 0; x <= i; x++)
                        if (result.ToString().Length < 6)
                        {
                            result += Convert.ToInt32(x.ToString()) + random.Next(0, 9);
                        }
                }
                return result;
            }
        }

        // ComPlete Code Number
        public static string ComPleteCodeNumber(string value, int Length)
        {
            string result;
            if (value.Length >= Length)
            {
                result = value.Substring(0, Length);
                return result;
            }
            else
            {
                result = value;
                for (int i = 0; i <= Length; i++)
                {
                    for (int x = 0; x <= i; x++)
                        if (result.ToString().Length < Length)
                        {
                            result = ("0") + result.ToString();
                        }
                }
                return result;
            }
        }

        #endregion Function Generate a random number between two numbers

        #region Compare Tow Array String

        public static Array CompareTowArrayString(List<string> arry2, List<string> arry1)
        {
            var result = arry1.Union(arry2).Except(arry1.Intersect(arry2)).ToArray();
            return result;
        }

        #endregion Compare Tow Array String

        #region Convert format Time Arabic

        public static string ConvertFormatTimeArabic(int hourOfDay, int minute)
        {
            string formatTime;
            if (hourOfDay == 0)
            {
                hourOfDay += 12;
                formatTime = "ص";
            }
            else if (hourOfDay == 12)
            {
                formatTime = "م";
            }
            else if (hourOfDay > 12)
            {
                hourOfDay -= 12;
                formatTime = "م";
            }
            else
            {
                formatTime = "ص";
            }
            string h = hourOfDay.ToString();
            if (h.Length == 1)
            {
                h = "0" + h;
            }
            string m = minute.ToString();
            if (m.Length == 1)
            {
                m = "0" + m;
            }
            string result = ConvertNumberToArFormat(h + ":" + m) + " " + formatTime;
            return result;
        }

        #endregion Convert format Time Arabic

        #region Convert convert Format Time Arabic Without Split

        public static string ConvertFormatTimeArabicWithoutSplit(string time)
        {
            var houres = time.Split(':');
            int hourOfDay = Convert.ToInt32(houres[0]);
            int minute = Convert.ToInt32(houres[1]);
            string formatTime;
            if (hourOfDay == 0)
            {
                hourOfDay += 12;
                formatTime = "ص";
            }
            else if (hourOfDay == 12)
            {
                formatTime = "م";
            }
            else if (hourOfDay > 12)
            {
                hourOfDay -= 12;
                formatTime = "م";
            }
            else
            {
                formatTime = "ص";
            }
            string h = hourOfDay.ToString();
            if (h.Length == 1)
            {
                h = "0" + h;
            }
            string m = minute.ToString();
            if (m.Length == 1)
            {
                m = "0" + m;
            }
            string result = ConvertNumberToArFormat(h + ":" + m) + " " + formatTime;
            return result;
        }

        internal static string GetEnumValue(ConstansValuesClass.TypesOfPaths typesOfPaths, int movementPathTypeOfPathId)
        {
            throw new NotImplementedException();
        }

        #endregion Convert convert Format Time Arabic Without Split

        #region Convert formatTimeEnglish

        public static string ConvertFormatTimeEnglish(int hourOfDay, int minute)
        {
            string formatTime;
            if (hourOfDay == 0)
            {
                hourOfDay += 12;
                formatTime = "AM";
            }
            else if (hourOfDay == 12)
            {
                formatTime = "PM";
            }
            else if (hourOfDay > 12)
            {
                hourOfDay -= 12;
                formatTime = "PM";
            }
            else
            {
                formatTime = "AM";
            }
            string h = hourOfDay.ToString();
            if (h.Length == 1)
            {
                h = "0" + h;
            }
            string m = minute.ToString();
            if (m.Length == 1)
            {
                m = "0" + m;
            }
            string result = h + ":" + m + " " + formatTime;
            return result;
        }

        #endregion Convert formatTimeEnglish

        #region Convert convert Format Time Arabic Without Split

        public static string ConvertFormatTimeEnglishWithoutSplit(string time)
        {
            var houres = time.Split(':');
            int hourOfDay = Convert.ToInt32(houres[0]);
            int minute = Convert.ToInt32(houres[1]);
            string formatTime;
            if (hourOfDay == 0)
            {
                hourOfDay += 12;
                formatTime = "AM";
            }
            else if (hourOfDay == 12)
            {
                formatTime = "PM";
            }
            else if (hourOfDay > 12)
            {
                hourOfDay -= 12;
                formatTime = "PM";
            }
            else
            {
                formatTime = "AM";
            }
            string h = hourOfDay.ToString();
            if (h.Length == 1)
            {
                h = "0" + h;
            }
            string m = minute.ToString();
            if (m.Length == 1)
            {
                m = "0" + m;
            }
            string result = h + ":" + m + " " + formatTime;
            return result;
        }

        #endregion Convert convert Format Time Arabic Without Split

        #region Get Name Day get Name month

        public static string GetNameDayArabic(string dt)
        {
            string dayName = "";
            try
            {
                if (ValedationClass.IsValidEmpty(MethodesConvertalClass.ConvertToDate(dt)) == false)
                {
                    DateTime dateTime = Convert.ToDateTime(MethodesConvertalClass.ConvertToDate(dt));
                    var culture = new CultureInfo("ar-Eg");
                    dayName = culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek).Trim();
                    return dayName;
                }
            }
            catch
            {
            }
            return dayName;
        }

        public static int GetDayId()
        {
            DateTime dt = Convert.ToDateTime(DateNow());
            var culture = new CultureInfo("ar-Eg");
            string dayName = culture.DateTimeFormat.GetDayName(dt.DayOfWeek);

            if ("Saturday".ToLower() == dayName.ToLower() || "السبت".ToLower() == dayName.ToLower())
                return 1;

            if ("Sunday".ToLower() == dayName.ToLower() || "الأحد".ToLower() == dayName.ToLower())
                return 2;

            if ("Monday".ToLower() == dayName.ToLower() || "الإثنين".ToLower() == dayName.ToLower())
                return 3;

            if ("Tuesday".ToLower() == dayName.ToLower() || "الثلاثاء".ToLower() == dayName.ToLower())
                return 4;

            if ("Wednesday".ToLower() == dayName.ToLower() || "الأربعاء".ToLower() == dayName.ToLower())
                return 5;

            if ("Thursday".ToLower() == dayName.ToLower() || "الخميس".ToLower() == dayName.ToLower())
                return 6;

            if ("Friday".ToLower() == dayName.ToLower() || "الجمعة".ToLower() == dayName.ToLower())
                return 7;
            return 0;
        }

        public static int GetDayIdByDate(DateTime dt)
        {
            try
            {
                var culture = new System.Globalization.CultureInfo("ar-Eg");
                string dayName = culture.DateTimeFormat.GetDayName(dt.DayOfWeek);
                if ("Saturday".ToLower() == dayName.ToLower() || "السبت".ToLower() == dayName.ToLower())
                    return 1;

                if ("Sunday".ToLower() == dayName.ToLower() || "الأحد".ToLower() == dayName.ToLower())
                    return 2;

                if ("Monday".ToLower() == dayName.ToLower() || "الإثنين".ToLower() == dayName.ToLower())
                    return 3;

                if ("Tuesday".ToLower() == dayName.ToLower() || "الثلاثاء".ToLower() == dayName.ToLower())
                    return 4;

                if ("Wednesday".ToLower() == dayName.ToLower() || "الأربعاء".ToLower() == dayName.ToLower())
                    return 5;

                if ("Thursday".ToLower() == dayName.ToLower() || "الخميس".ToLower() == dayName.ToLower())
                    return 6;

                if ("Friday".ToLower() == dayName.ToLower() || "الجمعة".ToLower() == dayName.ToLower())
                    return 7;
            }
            catch
            {
            }
            return 0;
        }

        public static string GetNameDayEnglish(DateTime dt)
        {
            var culture = new System.Globalization.CultureInfo("en-US");
            return culture.DateTimeFormat.GetDayName(dt.DayOfWeek).Trim();
        }

        public static string GetMonthNameArabic(DateTime dt)
        {
            return dt.ToString("MMMM", CultureInfo.CreateSpecificCulture("ar-EG")).Trim();
        }

        public static string GetMonthNameEnglish(DateTime dt)
        {
            return dt.ToString("MMMM", CultureInfo.CreateSpecificCulture("en-US")).Trim();
        }

        #endregion Get Name Day get Name month

        #region Calculate Relative Time Ar

        public static string CalculateRelativeTimeAr(DateTime yourDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;
            DateTime now = Convert.ToDateTime(DateTimeNow());
            var ts = new TimeSpan(now.Ticks - yourDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "منذ دقيقة واحدة" : "منذ ثوانى " + ConvertNumberToArFormat(ts.Seconds.ToString());

            if (delta < 2 * MINUTE)
                return " منذ دقيقة ";

            if (delta < 45 * MINUTE)
                return "منذ دقائق" + ConvertNumberToArFormat(ts.Minutes.ToString());

            if (delta < 90 * MINUTE)
                return " منذ ساعة";

            if (delta < 24 * HOUR)
                return "منذ ساعات " + ConvertNumberToArFormat(ts.Hours.ToString());

            if (delta < 48 * HOUR)
                return " منذ الامس";

            if (delta < 30 * DAY)
                return "منذ أيام" + ConvertNumberToArFormat(ts.Days.ToString());

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "منذ شهر" : "منذ اشهر " + ConvertNumberToArFormat(months.ToString());
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "منذ عام واحد " : "سنين مضت " + ConvertNumberToArFormat(years.ToString());
            }
        }

        #endregion Calculate Relative Time Ar

        #region Calculate Relative Time En

        public static string CalculateRelativeTimeEn(DateTime yourDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(Convert.ToDateTime(DateTimeNow()).Ticks - yourDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        #endregion Calculate Relative Time En

        #region Get Time Differnce

        public static TimeSpan GetTimeDiffernce(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts = dt2.Subtract(dt1);
            //string result = ChangeFormatTimeSpan(ts,ConstansValuesClass.timeSpanFormat);
            return ts;
        }

        #endregion Get Time Differnce

        #region Get Time Differnce

        public static string GetTimeDiffernce(string s1, string s2)
        {
            DateTime dt2 = ValedationClass.IsValidEmpty(s2) == true ? Convert.ToDateTime(DateTimeNow()) : Convert.ToDateTime(s2);
            DateTime dt1 = ValedationClass.IsValidEmpty(s1) == true ? Convert.ToDateTime(DateTimeNow()) : Convert.ToDateTime(s1);
            TimeSpan ts = dt2.Subtract(dt1);
            string result = ChangeFormatTimeSpan(ts, ConstansValuesClass.timeSpanFormat);
            return result;
        }

        public static TimeSpan GetTimeDiffernceTimespan(string s1, string s2)
        {
            DateTime dt2 = ValedationClass.IsValidEmpty(s2) == true ? Convert.ToDateTime(DateTimeNow()) : Convert.ToDateTime(s2);
            DateTime dt1 = ValedationClass.IsValidEmpty(s1) == true ? Convert.ToDateTime(DateTimeNow()) : Convert.ToDateTime(s1);
            TimeSpan ts = dt2.Subtract(dt1);
            return ts;
        }

        #endregion Get Time Differnce

        #region compress directory

        //public static void zipDirectory(string dir)
        //{
        //    string parent = Path.GetDirectoryName(dir);
        //    string name = Path.GetFileName(dir);
        //    string fileName = Path.Combine(parent,name+".zip");
        //    ZipFile.CreateFromDirectory(dir, fileName, CompressionLevel.Fastest, true);
        //}

        #endregion compress directory

        #region ConvertExcelToDataTable

        /*public static DataTable ConvertExcelToDataTable(string path)
        {
            DataRow myNewRow;
            DataTable myTable;
            //Create COM Objects. Create a COM object for everything that is referenced
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            int rows = xlRange.Rows.Count;
            int cols = xlRange.Columns.Count;

            //Set DataTable Name and Columns Name
            myTable = new DataTable("MyDataTable");
            myTable.Columns.Add("hrcode", typeof(string);
            myTable.Columns.Add("name", typeof(string);
            myTable.Columns.Add("Titel", typeof(string);
            myTable.Columns.Add("Department", typeof(string);
            myTable.Columns.Add("CC", typeof(string);
            myTable.Columns.Add("Phone", typeof(string);

            //first row using for heading, start second row for data
            for (int i = 2; i <= rows; i++)
            {
                myNewRow = myTable.NewRow();
                myNewRow["hrcode"] = xlRange.Cells[i, 1].Value2.ToString(); //string
                myNewRow["name"] = xlRange.Cells[i, 2].Value2.ToString(); //string
                myNewRow["Titel"] = xlRange.Cells[i, 3].Value2.ToString(); //string
                myNewRow["Department"] = xlRange.Cells[i, 4].Value2.ToString(); //string
                myNewRow["CC"] = xlRange.Cells[i, 5].Value2.ToString(); //string
                myNewRow["Phone"] = xlRange.Cells[i, 6].Value2.ToString(); //string
                myTable.Rows.Add(myNewRow);
            }
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            return myTable;
        }*/

        #endregion ConvertExcelToDataTable

        #region Function Get Image Defoult

        public static string GetImageDefoult(string pathImageTrue)
        {
            //string imageProfile = dtEmploey.Rows[0]["imageProfile"].ToString();
            string imageDefault = "_Default Files/profile.png";
            string imageProfile = pathImageTrue;
            string root = System.Web.HttpContext.Current.Server.MapPath("~/");
            if (imageProfile == string.Empty)
            {
                imageProfile = imageDefault;
            }
            if (!File.Exists(root + "/" + pathImageTrue))
            {
                //SetDefoult
                imageProfile = imageDefault;
            }
            return imageProfile;
        }

        #endregion Function Get Image Defoult

        #region RoundNumber

        public static decimal RoundNumber(decimal desimalValue)
        {
            try
            {
                string value = string.Format("{0:#.00}", desimalValue);
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        #endregion RoundNumber

        #region Calculate date To come

        public static string CalculateRemainedTime(string dateTime, string language)
        {
            string time = "Un Known";

            DateTime date_time_reminder = ValedationClass.IsValidEmpty(dateTime) == true ? Convert.ToDateTime(DateTimeNow()) : Convert.ToDateTime(dateTime);
            DateTime date_time_now = Convert.ToDateTime(DateTimeNow());
            if (date_time_now < date_time_reminder)
            {
                TimeSpan time_span = date_time_reminder - date_time_now;
                time = string.Format("{0:00}:{1:00}:{2:00}", time_span.Hours, time_span.Minutes, time_span.Seconds);
                if (language == ConstansValuesClass.languageCodeEn)
                    return "Left: " + time_span.Days.ToString() + " days, " + time;
                else
                    return "بعد: " + time_span.Days.ToString() + " ايام, " + time;
            }
            else if (date_time_now >= date_time_reminder)
            {
                TimeSpan time_span = date_time_now - date_time_reminder;
                time = string.Format("{0:00}:{1:00}:{2:00}", time_span.Hours, time_span.Minutes, time_span.Seconds);
                if (language == ConstansValuesClass.languageCodeEn)
                    return "Ago: " + time_span.Days.ToString() + " ايام, " + time;
                else
                    return "منذ: " + time_span.Days.ToString() + " ايام, " + time;
            }
            return time;
        }

        #endregion Calculate date To come

        #region Convert TimeSpan

        public static TimeSpan ConvertTimeSpan(string ts)
        {
            try
            {
                return TimeSpan.Parse(ts);
            }
            catch
            {
                return TimeSpan.Zero;
            }
        }

        #endregion Convert TimeSpan

        #region ChangeFormatTimeSpan

        public static string ChangeFormatTimeSpan(TimeSpan timeDifference, string timeSpanFormat)
        {
            return timeDifference.ToString();
            //string result = string.Format(timeSpanFormat, timeDifference.TotalHours, timeDifference.Minutes, timeDifference.Seconds);
            //return result;
        }

        #endregion ChangeFormatTimeSpan

        #region Move File

        public static void MoveFile(string FilePath, string Newpath)
        {
            //Do your job with "file"
            if (!File.Exists(Newpath))
            {
                File.Move(FilePath, Newpath);
            }
        }

        #endregion Move File

        #region Funcation Distance measurement

        public static bool DistanceMeasurement(string empoloyeLatitude, string empoLongitude, string palceLatitude, string placeLongitude, string placeArea)
        {
            try
            {
                double destance = MethodesClass.GetDistanceBetweenPoints(
                    Convert.ToDouble(empoloyeLatitude),
                    Convert.ToDouble(empoLongitude),
                    Convert.ToDouble(palceLatitude),
                    Convert.ToDouble(placeLongitude));

                if (Math.Abs(destance) <= Math.Abs(Convert.ToDouble(placeArea)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception) { }
            return false;
        }

        #endregion Funcation Distance measurement

        #region Function Concatonate Phone With Cc

        public static string ConcatonatePhoneWithCc(string phone, string CountryCodeName)
        {
            //Change Data Phone
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber swissNumberProto = phoneUtil.Parse(phone, CountryCodeName);
            return phoneUtil.Format(swissNumberProto, PhoneNumberFormat.E164);
        }

        #endregion Function Concatonate Phone With Cc

        #region DistanceTo

        //public static double SsDistanceTo(double lat1, double lon1, double lat2, double lon2)
        //{
        //    lon1 = ToRadians(lon1);
        //    lon2 = ToRadians(lon2);
        //    lat1 = ToRadians(lat1);
        //    lat2 = ToRadians(lat2);

        //    // Haversine formula
        //    var dlon = lon2 - lon1;
        //    var dlat = lat2 - lat1;
        //    var a = Math.Pow(Math.Sin(dlat / 2), 2) +
        //                Math.Cos(lat1) * Math.Cos(lat2) *
        //                Math.Pow(Math.Sin(dlon / 2), 2);

        //    var c = 2 * Math.Asin(Math.Sqrt(a));

        //    // Radius of earth in
        //    // kilometers. Use 3956
        //    // for miles
        //    var r = 6371;

        //    // calculate the result
        //    return (c * r);
        //}

        //private static double ToRadians(double angleIn10thofaDegree)
        //{
        //    // Angle in 10th
        //    // of a degree
        //    return (angleIn10thofaDegree * Math.PI) / 180;
        //}

        #endregion DistanceTo

        #region DistanceTo

        public static double GetDistanceBetweenPoints(double fromLong, double fromLat,
                                      double toLong, double toLat)
        {
            double d2r = Math.PI / 180;
            double dLong = (toLong - fromLong) * d2r;
            double dLat = (toLat - fromLat) * d2r;
            double a = Math.Pow(Math.Sin(dLat / 2.0), 2) + Math.Cos(fromLat * d2r)
                    * Math.Cos(toLat * d2r) * Math.Pow(Math.Sin(dLong / 2.0), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = 6367000 * c;
            return Math.Round(d);
            // Radius of earth in
            // Meter. Use 6367000
            // kilometers. Use 3956
            // Miles. Use 6371
        }

        #endregion DistanceTo

        #region Read Excel

        public static DataTable ReadExcel(string fileName, string fileExt)
        {
            DataTable dtexcel = new DataTable();
            string conn;
            if (fileExt.CompareTo(".xls") == 0)
                conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"; //for above excel 2007
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1
                oleAdpt.Fill(dtexcel); //fill excel data into dataTable
            }
            return dtexcel;
        }

        #endregion Read Excel

        #region Is Time Of Day Between

        static public bool IsTimeOfDayBetween(DateTime time, TimeSpan startTime, TimeSpan endTime)
        {
            if (endTime == startTime)
            {
                return true;
            }
            else if (endTime < startTime)
            {
                return time.TimeOfDay <= endTime ||
                    time.TimeOfDay >= startTime;
            }
            else
            {
                return time.TimeOfDay >= startTime &&
                    time.TimeOfDay <= endTime;
            }
        }

        /*public static bool TimeBetween(TimeSpan Time, TimeSpan start, TimeSpan end)
        {
            // convert datetime to a TimeSpan
            TimeSpan now = Time;
            // see if start comes before end
            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end < now && now < start);
        }*/

        #endregion Is Time Of Day Between

        #region Date And Time Between

        public static bool DateAndTimeBetween(DateTime input, DateTime datePast, DateTime dateFuture)
        {
            if (datePast.Date <= input.Date && input.Date <= dateFuture.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Date And Time Between

        #region Function Delete File

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        #endregion Function Delete File

        #region Convert Numbers

        public static string ConvertNumberToArFormat(string value)
        {
            string newValue = (((((((((((((((value + "")
                    .Replace("1", "١")).Replace("2", "٢"))
                    .Replace("3", "٣")).Replace("4", "٤"))
                    .Replace("5", "٥")).Replace("6", "٦"))
                    .Replace("7", "٧")).Replace("8", "٨"))
                    .Replace("9", "٩")).Replace("0", "٠"))
                    .Replace("am", "ص")).Replace("pm", "م"))
                    .Replace("AM", "ص")).Replace("PM", "م"));
            return newValue;
        }

        public static string ConvertNumberToEnFormat(string value)
        {
            string newValue = "";
            if (value != null)
            {
                newValue = (((((((((((((((value + "")
                       .Replace("١", "1")).Replace("٢", "2"))
                       .Replace("٣", "3")).Replace("٤", "4"))
                       .Replace("٥", "5")).Replace("٦", "6"))
                       .Replace("٧", "7")).Replace("٨", "8"))
                       .Replace("٩", "9")).Replace("٠", "0"))
                       .Replace("ص", "am")).Replace("م", "pm"))
                       .Replace("ص", "am")).Replace("م", "pm"));
            }
            return newValue;
        }

        public static string ArabicNumberToDecimalNumber(string number)
        {
            char[] chars = new char[number.Length];
            for (int i = 0; i < number.Length; i++)
            {
                char ch = number.ElementAt(i);
                if (ch >= 0x0660 && ch <= 0x0669)
                    ch -= Convert.ToChar(0x0660 - '0');
                else if (ch >= 0x06f0 && ch <= 0x06F9)
                    ch -= Convert.ToChar(0x06f0 - '0');
                chars[i] = ch;
            }
            return new String(chars);
        }

        #endregion Convert Numbers

        #region Read From XML

        public static string ReadXMl(string key_id)
        {
            //Path
            string path = System.Web.HttpContext.Current.Server.MapPath(@"~/helper/Messages.XML");
            //Xml File
            XDocument xdoc = XDocument.Load(path);
            //ID Value
            string Value = xdoc.Descendants(key_id).First().Value;
            //Retune Value
            return Value;
        }

        #endregion Read From XML

        #region CeartFolder

        public static void CeartFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion CeartFolder

        #region DeleteFolder

        public static void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        #endregion DeleteFolder

        #region Error Log

        public static void ErrorLogAsync(string Message)
        {
            StreamWriter sw = null;
            try
            {
                string sLogFormat = Convert.ToDateTime(MethodesClass.DateTimeNow()).ToString(ConstansValuesClass.formatDate) + " " + Convert.ToDateTime(MethodesClass.DateTimeNow()).ToLongTimeString() + " ==> ";
                string sPathName = System.Web.HttpContext.Current.Server.MapPath("~/_Log");
                //string sPathName = @"C:\Users\Dell\Desktop\Refit v 1.0\_Log";
                CeartFolder(sPathName);
                string sYear = Convert.ToDateTime(MethodesClass.DateTimeNow()).Year.ToString();
                string sMonth = Convert.ToDateTime(MethodesClass.DateTimeNow()).Month.ToString();
                string sDay = Convert.ToDateTime(MethodesClass.DateTimeNow()).Day.ToString();
                string sErrorTime = sDay + " - " + sMonth + "-" + sYear;
                sw = new StreamWriter(sPathName + "\\" + sErrorTime + ".txt", true);
                sw.WriteLine(sLogFormat + Message);
                sw.Flush();

                #region Send mail by Erorr

                string projectName = GetProjectName();
                //Ahmed Mail
                //await SendingAlertMessages.sendMail(ConstansValuesClass.sendFrom, ConstansValuesClass.password, ConstansValuesClass.stmpServer, ConstansValuesClass.portNumber, "a77med.16@gmail.com", "Error Project : " + projectName, (sLogFormat + Message), "");
                ////Ahmed Omer Mail
                //await SendingAlertMessages.sendMail(ConstansValuesClass.sendFrom, ConstansValuesClass.password, ConstansValuesClass.stmpServer, ConstansValuesClass.portNumber, "ahmedomr0914@gmail.com", "Error Project : " + projectName, (sLogFormat + Message), "");

                #endregion Send mail by Erorr
            }
            catch (Exception)
            {
                //ErrorLog(ex.ToString());
            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                    sw.Close();
                }
            }
        }

        #endregion Error Log

        #region Get Projec tName

        public static string GetProjectName()
        {
            return Assembly.GetCallingAssembly().GetName().Name;
        }

        #endregion Get Projec tName

        #region Money Format

        public static string MoneyFormat(string money)
        {
            try
            {
                decimal value = Convert.ToDecimal(money);
                //CultureInfo us = CultureInfo.GetCultureInfo("en-eg");
                //return string.Format(us, "{0:#.00}", value);
                return value.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("ar-eg"));  //  $1,234.95
            }
            catch
            {
                return "0.0";
            }
        }

        #endregion Money Format

        #region GetMacAddress

        public static string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        #endregion GetMacAddress

        #region GetMethodName

        public static string GetMethodName()
        {
            return "Funcation name : " + new StackTrace(1).GetFrame(0).GetMethod().Name;
        }

        #endregion GetMethodName

    }

    public static class MethodesConvertalClass
    {
        #region ConvertToInt

        public static int ConvertToInt(object value)
        {
            if (value == null)
                value = "";
            return ValedationClass.IsValidNumber(value.ToString()) == false ? 0 : Convert.ToInt32(value);
        }

        #endregion ConvertToInt

        #region ConvertToString

        public static string ConvertToString(object value)
        {
            if (value == null)
                value = "";
            return ValedationClass.IsValidEmpty(value.ToString()) == true ? "" : value.ToString();
        }

        #endregion ConvertToString

        #region ConvertToDecimal

        public static decimal ConvertToDecimal(object value)
        {
            if (value == null)
                value = "";
            return ValedationClass.IsValidNumber(value.ToString()) == false ? 0 : Convert.ToDecimal(value);
        }

        #endregion ConvertToDecimal

        #region ConvertToDouble

        public static double ConvertToDouble(object value)
        {
            if (value == null)
                value = "";
            return ValedationClass.IsValidNumber(value.ToString()) == false ? 0 : Convert.ToDouble(value);
        }

        #endregion ConvertToDecimal



        #region ConvertToBool

        public static bool ConvertToBool(object value)
        {
            if (value == null)
                value = "";
            return ValedationClass.IsValidBollenValue(value.ToString()) == false ? false : Convert.ToBoolean(value);
        }

        #endregion ConvertToBool

        #region ConvertToDate

        public static string ConvertToDate(object value)
        {
            try
            {
                return ValedationClass.IsValidEmpty(value.ToString()) == true ? "" : Convert.ToDateTime(value).ToString(ConstansValuesClass.formatDate);
            }
            catch
            {
                return "";
            }
        }

        #endregion ConvertToDate

        #region ConvertToDateTime

        public static string ConvertToDateTime(object value)
        {
            try
            {
                if (value == null)
                    value = "";
                return ValedationClass.IsValidEmpty(value.ToString()) == true ? "" : Convert.ToDateTime(value).ToString(ConstansValuesClass.formatDateTime);
            }
            catch
            {
                return "";
            }
        }

        #endregion ConvertToDateTime

        #region ConvertToDateTime

        public static string ConvertToTime(object value)
        {
            try
            {
                //return ValedationClass.IsValidEmpty(value.ToString()) == true ? "" : DateTime.ParseExact(value.ToString(), ConstansValuesClass.formatTime,
                //                        CultureInfo.InvariantCulture).ToString();

                return ValedationClass.IsValidEmpty(value.ToString()) == true ? "" : DateTime.Parse(value.ToString()).ToString(ConstansValuesClass.formatTime);
            }
            catch
            {
                return "";
            }
        }

        #endregion ConvertToDateTime
    }
}
