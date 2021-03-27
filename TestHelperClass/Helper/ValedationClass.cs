using PhoneNumbers;
using System;
using System.Text.RegularExpressions;

namespace Admins_Transportation.helper
{
    public class ValedationClass
    {
        #region Valid IP

        private static readonly string IP_PATTERN = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

        public static bool IsValidIp(string ip)
        {
            return Regex.IsMatch(ip, IP_PATTERN) && ip.Trim().Length > 0;   //ip.Trim().Length > 0; (why?)
        }

        #endregion Valid IP

        #region Valid Email

        private static readonly string Email_PATTERN = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"; 
        //why @ before "" and $ in the end 

        public static bool IsValidEmail(string email)
        {
            if (email != null && email != string.Empty)
                return Regex.IsMatch(email, Email_PATTERN);
            else
                return false;
        }

        #endregion Valid Email

        #region Validation MAC

        private static readonly string MAC_PATTERN = "^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";

        public static bool IsValidMac(string mac)
        {
            if (mac != null && mac != string.Empty)
                return Regex.IsMatch(mac, MAC_PATTERN) && mac.Trim().Length > 0;    //why
            else
                return false;
        }

        #endregion Validation MAC

        #region Validation phone

        public static bool IsValidPhone(string phone, string countryCode, string countryCodeName)
        {
            try
            {
                if (IsValidEmpty(phone) == false && IsValidIntgerNumber(phone) == true)
                {
                    PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
                    PhoneNumber queryPhoneNumber = phoneUtil.Parse(phone, countryCodeName);
                    if (IsValidPhoneGoogle(phone, countryCode) == false)
                        return false;
                    //Valid
                    return phoneUtil.IsValidNumber(queryPhoneNumber);
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhoneGoogle(string swissNumberProto, string countryCode)
        {
            try
            {
                swissNumberProto = countryCode + swissNumberProto;
                if (IsValidEmpty(swissNumberProto) == false)
                {
                    PhoneNumberUtil phoneUtil = PhoneNumberUtil.GetInstance();
                    PhoneNumber queryPhoneNumber = phoneUtil.Parse(swissNumberProto, "");
                    return phoneUtil.IsValidNumber(queryPhoneNumber);
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion Validation phone

        #region Validation image

        private static readonly string IMAGE_PATTERN = @"[\/.](gif|jpg|jpeg|tiff|png|jpeg|jfif)$";

        public static bool IsValidImage(string image)
        {
            if (image != null && image != string.Empty)
                return Regex.IsMatch(image.Trim().ToLower(), IMAGE_PATTERN) && image.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation image

        #region Validation video

        private static readonly string VIDEI_PATTERN = @"[\/.](mov|avi|wmv|mp4|m4p|m4v|ogg|mpg|mp2|mpeg|mpe|mpv|3gp|flv)$";

        public static bool IsValidVideo(string video)
        {
            if (video != null && video != string.Empty)
                return Regex.IsMatch(video.Trim().ToLower(), VIDEI_PATTERN.ToLower()) && video.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation video

        //#region Validation IsDate

        ////(yyyy/MM/dd)
        //private static readonly string isDate_PATTERN = @"((19|20|30|40|50)\d\d)\/(0[1-9]|1[0-9])\/((0|1)[0-9]|2[0-9]|3[0-1])$";

        ////private static readonly string isDate_PATTERN = @"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))|^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$";

        //public static bool IsValidDate(string date)
        //{
        //  //try
        //  //{
        //  //  if (IsValidEmpty(date) == false)
        //  //  {
        //  //    Convert.ToDateTime(date);
        //  //    return true;
        //  //  }
        //  //}
        //  //catch
        //  //{
        //  //  //MethodesClass.ErrorLogAsync("date is " + date + MethodesClass.GetMethodName());
        //  //  return false;
        //  //}
        //  if (date != null && date != string.Empty)
        //    return Regex.IsMatch(date, isDate_PATTERN) & date.Trim().Length > 0;
        //  else
        //  {
        //    return false;
        //  }
        //}

        //#endregion Validation IsDate

        #region Validation DateTime

        public static bool IsValiDateTime(string date, string dateFormat)
        {
            try
            {
                DateTime dt = default;
                //bool isValid = DateTime.TryParseExact(date, dateFormat, null, 0, out dt);
                bool isValid = DateTime.TryParseExact(date, dateFormat, null, 0, out dt);
                if (!isValid) return false;
                DateTime min = new DateTime(1967, 1, 1);
                DateTime max = new DateTime(3000, 1, 1);
                if (dt < min || dt > max) { return false; }
                return true;
            }
            catch (Exception e1)
            {
                MethodesClass.ErrorLogAsync("date is " + date + "\n" + " Exception Is " + e1.Message + MethodesClass.GetMethodName());
                return false;
            }
        }

        #endregion Validation DateTime

        #region Validation Is time

        private static readonly string TIME_24_PATTERN = "^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
        private static readonly string TIME_12_PATTERN = "(1[012]|[1-9]):[0-5][0-9](\\s)?(?i)(am|pm)";

        public static bool IsValidTime24(string time)
        {
            if (time != null && time != string.Empty)
                return Regex.IsMatch(time, TIME_24_PATTERN) && time.Trim().Length > 0;
            else
                return false;
        }

        public static bool IsValidTime12(string time)
        {
            if (time != null && time != string.Empty)
                return Regex.IsMatch(time, TIME_12_PATTERN) && time.Trim().Length > 0;
            else
                return false;
        }

        public static bool IsValidTimeFormat(string input)
        {
            if (input != null && input != string.Empty)
            {
                return TimeSpan.TryParse(
                  input, out TimeSpan dummyOutput);
            }
            else
                return false;
        }

        #endregion Validation Is time

        #region Validation Name AR

        private static readonly string ARABIC_USERNAME = "^[\\u0621-\\u064A\\s]{3,50}$";
        //private static readonly string ARABIC_SPECIAL_USERNAME = "^[\\u0621-\\u064A\\u0660-\\u06690-9-،._/\n\r\t(){}'\" ]{0,500}$";

        public static bool IsValidNameAr(string text)
        {
            if (text != null && text != string.Empty)
                return Regex.IsMatch(text, ARABIC_USERNAME) && text.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation Name AR

        #region Validation text AR

        //private static readonly string arPATTERN = "[ء-ي]+";
        public static readonly string SPECIAL_ARABIC_NUMBER = "^[\\u0621-\\u064A\\u0660-\\u06690-9-،._/\n\r\t(){}'\" ]{0,500}$";

        public static bool IsValidTextAr(string text)
        {
            //text = text.Replace("\n", " ");
            if (text != null && text != string.Empty)
                return Regex.IsMatch(text, SPECIAL_ARABIC_NUMBER) && text.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation text AR

        #region Validation text EN

        //private static readonly string enTextPATTERN = "^[a-zA-Z0-9$@$!%*?&#^-_.+(){}/'" + '"' + "+\n+,+:+'+%+<+>+.+;+^+\\+~+=+-]+$";
        private static readonly string EN_TEXT_PATTERN = @"^(.|\s)*[a-zA-Z0-9]+(.|\s)*$";

        public static bool IsValidTextEn(string text)
        {
            if (text != null && text != string.Empty)
                return Regex.IsMatch(text, EN_TEXT_PATTERN) && text.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation text EN

        #region Validation Symbol 2 Degit English (Min 2 and Max 12)

        private static readonly string SYMBOL_12_DEGIT = "^[ A-Za-z]{2,12}$";

        public static bool IsValidSymbol2Degit(string text)
        {
            if (text != null && text != string.Empty)
                return Regex.IsMatch(text, SYMBOL_12_DEGIT) && text.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation Symbol 2 Degit English

        #region Validation Name EN

        private static readonly string EN_NAME_PATTERN = "^[ A-Za-z]{2,100}$";

        public static bool IsValidNameEn(string text)
        {
            if (text != null && text != string.Empty)
                return Regex.IsMatch(text, EN_NAME_PATTERN) && text.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation Name EN

        #region Validation Address

        //public static string addressPATTERN = @"^[a-zA-Z0-9,.!?_]*$";
        private static readonly string ADDRESS_PATTERN = @"^[a-zA-Z0-9,._(){}/'" + '"' + " ]*$";

        public static bool IsValidAddress(string address)
        {
            if (address != null && address != string.Empty)
                return Regex.IsMatch(address, ADDRESS_PATTERN) && address.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation Address

        #region Validation Letar And Numbr

        //public static string addressPATTERN = @"^[a-zA-Z0-9,.!?_]*$";
        private static readonly string ENGLISH_WITH_NUMBER_PATTERN = @"^[a-zA-Z0-9 ]*$";

        public static bool IsValidEnglishWithNumber(string text)
        {
            if (text != null && text != string.Empty)
                return Regex.IsMatch(text, ENGLISH_WITH_NUMBER_PATTERN) && text.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation Letar And Numbr

        #region Validation lat,lang

        private static readonly string LAT_LONG_PATTERN = @"^(\-?\d+(\.\d+)?),\s*(\-?\d+(\.\d+)?)$";

        public static bool IsValidLat_Long(string lat_long)
        {
            if (lat_long != null && lat_long != string.Empty)
                return Regex.IsMatch(lat_long, LAT_LONG_PATTERN) && lat_long.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation lat,lang

        #region Validation IsNumber

        private static readonly string NUMBER_PATTERN = @"^-?[0-9]\d*(\.\d+)?$";

        public static bool IsValidNumber(string number)
        {
            if (number != null && number != string.Empty)
                return Regex.IsMatch(number, NUMBER_PATTERN) && number.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation IsNumber

        #region Validation PortNuber

        // This regex must accept special characters and capital Letters and small Letters and numbers and minlength 8
        private static readonly string PORT_PATTERN = "^()([1-9]|[1-5]?[0-9]{2,4}|6[1-4][0-9]{3}|65[1-4][0-9]{2}|655[1-2][0-9]|6553[1-5])$";

        public static bool IsValidPort(string port)
        {
            if (port != null && port != string.Empty)
                return Regex.IsMatch(port, PORT_PATTERN) && port.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation PortNuber

        #region Validation Passwords

        //Validation This regex must accept special characters and capital Letters and small Letters and numbers and minlength 8
        // This regex must accept special characters and capital Letters and small Letters and numbers and minlength 8
        private static readonly string PASSWORD_PATTERN = "^(?=.*?\\p{Lu})(?=.*?\\p{Ll})(?=.*?\\d)(?=.*?[`~!@#$%^&*()\\-_=+\\\\|\\[{\\]};:'\",<.>/?]).*$";

        public static bool IsValidPasswords(string passwor)
        {
            if (passwor != null && passwor != string.Empty)
                return Regex.IsMatch(passwor, PASSWORD_PATTERN) && passwor.Trim().Length > 0;
            else
                return false;
        }
        #endregion Validation Passwords

        #region Validation Is Boolen Value

        private static readonly string BOLLEN_PATTERN = "^([Tt][Rr][Uu][Ee]|[Ff][Aa][Ll][Ss][Ee])$";

        public static bool IsValidBollenValue(string text)
        {
            if (text != null && text != string.Empty)
                return Regex.IsMatch(text, BOLLEN_PATTERN) && text.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation Is Boolen Value

        #region Validation Is Intger Number_PATTERN

        private static readonly string INTGER_NUMBER_PATTERN = "^[0-9]+$";

        public static bool IsValidIntgerNumber(string number)
        {
            if (number != null && number != string.Empty)
                return Regex.IsMatch(number, INTGER_NUMBER_PATTERN) && number.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation Is Intger Number_PATTERN

        #region Validation Compare toStrings

        public static bool IsValidCompareToStrings(string str1, string str2)
        {
            str1 = (str1 == null || str1 == string.Empty) ? "" : str1.Trim();
            str2 = (str2 == null || str2 == string.Empty) ? "" : str2.Trim();
            return string.Equals(str1, str2, StringComparison.CurrentCultureIgnoreCase);
        }

        #endregion Validation Compare toStrings

        #region Validation IsNotEmpty toStrings

        public static bool IsValidEmpty(string str)
        {
            if (str == null || str.Trim().Length <= 0)
                return true;
            else
                return false;
        }

        #endregion Validation IsNotEmpty toStrings

        #region Validation Menumen Cout Character

        public static bool IsValidMenumenCoutCharacter(string str, int count)       //????
        {
            if (str.Trim().Length <= count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion Validation Menumen Cout Character

        #region Validation isValidGender

        private static readonly string GENDOR_PATTERN = "^(?:m|M|male|Male|f|F|female|Female)$";

        public static bool IsValidGender(string text)
        {
            if (text != null && text != string.Empty)
                return Regex.IsMatch(text, GENDOR_PATTERN) && text.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation isValidGender

        #region Validation isValidURL

        private static readonly string URL_PATTERN = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";

        public static bool IsValidURL(string text)
        {
            if (text != null && text != string.Empty)
                return Regex.IsMatch(text, URL_PATTERN) && text.Trim().Length > 0;
            else
                return false;
        }

        #endregion Validation isValidURL

        #region Validation Language Type

        public static bool IsValidLanguage(string language)         //????
        {
            language = (language == null || language == string.Empty) ? "" : language.Trim();
            if (language.ToLower() == ConstansValuesClass.laguageCodeAr.ToLower()
              || language.ToLower() == ConstansValuesClass.languageCodeEn.ToLower())
                return true;
            else
                return false;
        }

        #endregion Validation Language Type

        #region Validation IsValidPlatForm

        public static bool IsValidPlatForm(string userFirebasePlatForm)         //????
        {
            userFirebasePlatForm = IsValidEmpty(userFirebasePlatForm) == true ? "" : userFirebasePlatForm.Trim();
            if (userFirebasePlatForm.Trim().ToLower() != ConstansValuesClass.platFormAndroid.ToLower()
            && userFirebasePlatForm.Trim().ToLower() != ConstansValuesClass.platFormWeb.ToLower()
            && userFirebasePlatForm.Trim().ToLower() != ConstansValuesClass.platFormIos.ToLower())
                return false;
            else
                return true;
        }

        #endregion Validation IsValidPlatForm
    }
}
