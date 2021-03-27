using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Admins_Transportation.helper
{
    public static class TranslatorGoogleClass
    {
        #region Properties

        /// <summary>
        /// Gets the supported languages.
        /// </summary>
        public static IEnumerable<string> languages
        {
            get
            {
                TranslatorGoogleClass.EnsureInitialized();
                return TranslatorGoogleClass._languageModeMap.Keys.OrderBy(p => p);
            }
        }

        /// <summary>
        /// Gets the time taken to perform the translation.
        /// </summary>
        public static TimeSpan translationTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the url used to speak the translation.
        /// </summary>
        /// <value>The url used to speak the translation.</value>
        public static string translationSpeechUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        public static Exception error
        {
            get;
            private set;
        }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Translates the specified source text.
        /// </summary>
        /// <param name="sourceText">The source text.</param>
        /// <param name="sourceLanguage">The source language.</param>
        /// <param name="targetLanguage">The target language.</param>
        /// <returns>The translation.</returns>
        public static string Translate
            (string sourceText,
             string sourceLanguage,
             string targetLanguage)
        {
            // Initialize
            error = null;
            translationSpeechUrl = null;
            translationTime = TimeSpan.Zero;
            DateTime tmStart = DateTime.Now;
            string translation = string.Empty;

            try
            {
                // Download translation
                string url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                                            TranslatorGoogleClass.LanguageEnumToIdentifier(sourceLanguage),
                                            TranslatorGoogleClass.LanguageEnumToIdentifier(targetLanguage),
                                            HttpUtility.UrlEncode(sourceText));
                string outputFile = Path.GetTempFileName();
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                    wc.DownloadFile(url, outputFile);
                }

                // Get translated text
                if (File.Exists(outputFile))
                {
                    // Get phrase collection
                    string text = File.ReadAllText(outputFile);
                    int index = text.IndexOf(string.Format(",,\"{0}\"", TranslatorGoogleClass.LanguageEnumToIdentifier(sourceLanguage)));
                    if (index == -1)
                    {
                        // Translation of single word
                        int startQuote = text.IndexOf('\"');
                        if (startQuote != -1)
                        {
                            int endQuote = text.IndexOf('\"', startQuote + 1);
                            if (endQuote != -1)
                            {
                                translation = text.Substring(startQuote + 1, endQuote - startQuote - 1);
                            }
                        }
                    }
                    else
                    {
                        // Translation of phrase
                        text = text.Substring(0, index);
                        text = text.Replace("],[", ",");
                        text = text.Replace("]", string.Empty);
                        text = text.Replace("[", string.Empty);
                        text = text.Replace("\",\"", "\"");

                        // Get translated phrases
                        string[] phrases = text.Split(new[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; (i < phrases.Count()); i += 2)
                        {
                            string translatedPhrase = phrases[i];
                            if (translatedPhrase.StartsWith(",,"))
                            {
                                i--;
                                continue;
                            }
                            translation += translatedPhrase + "  ";
                        }
                    }

                    // Fix up translation
                    translation = translation.Trim();
                    translation = translation.Replace(" ?", "?");
                    translation = translation.Replace("-", "");
                    translation = translation.Replace(" !", "!");
                    translation = translation.Replace(" ,", ",");
                    translation = translation.Replace(" .", ".");
                    translation = translation.Replace(" ;", ";");

                    // And translation speech URL
                    translationSpeechUrl = string.Format("https://translate.googleapis.com/translate_tts?ie=UTF-8&q={0}&tl={1}&total=1&idx=0&textlen={2}&client=gtx",
                                                               HttpUtility.UrlEncode(translation), TranslatorGoogleClass.LanguageEnumToIdentifier(targetLanguage), translation.Length);
                }
            }
            catch (Exception ex)
            {
                error = ex;
            }

            // Return result
            translationTime = DateTime.Now - tmStart;
            return translation;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Converts a language to its identifier.
        /// </summary>
        /// <param name="language">The language."</param>
        /// <returns>The identifier or <see cref="string.Empty"/> if none.</returns>
        private static string LanguageEnumToIdentifier
            (string language)
        {
            EnsureInitialized();
            _languageModeMap.TryGetValue(language, out string mode);
            return mode;
        }

        /// <summary>
        /// Ensures the translator has been initialized.
        /// </summary>
        private static void EnsureInitialized()
        {
            if (TranslatorGoogleClass._languageModeMap == null)
            {
                TranslatorGoogleClass._languageModeMap = new Dictionary<string, string>
                {
                    { "af", "af" },
                    { "sq", "sq" },
                    { "ar", "ar" },
                    { "hy", "hy" },
                    { "az", "az" },
                    { "eu", "eu" },
                    { "be", "be" },
                    { "bn", "bn" },
                    { "bg", "bg" },
                    { "ca", "ca" },
                    { "zh-CN", "zh-CN" },
                    { "hr", "hr" },
                    { "cs", "cs" },
                    { "da", "da" },
                    { "nl", "nl" },
                    { "en", "en" },
                    { "eo", "eo" },
                    { "et", "et" },
                    { "tl", "tl" },
                    { "fi", "fi" },
                    { "fr", "fr" },
                    { "gl", "gl" },
                    { "de", "de" },
                    { "ka", "ka" },
                    { "el", "el" },
                    { "ht", "ht" },
                    { "iw", "iw" },
                    { "hi", "hi" },
                    { "hu", "hu" },
                    { "is", "is" },
                    { "id", "id" },
                    { "ga", "ga" },
                    { "it", "it" },
                    { "ja", "ja" },
                    { "ko", "ko" },
                    { "lo", "lo" },
                    { "la", "la" },
                    { "lv", "lv" },
                    { "lt", "lt" },
                    { "mk", "mk" },
                    { "ms", "ms" },
                    { "mt", "mt" },
                    { "no", "no" },
                    { "fa", "fa" },
                    { "pl", "pl" },
                    { "pt", "pt" },
                    { "ro", "ro" },
                    { "ru", "ru" },
                    { "sr", "sr" },
                    { "sk", "sk" },
                    { "sl", "sl" },
                    { "es", "es" },
                    { "sw", "sw" },
                    { "sv", "sv" },
                    { "ta", "ta" },
                    { "te", "te" },
                    { "th", "th" },
                    { "tr", "tr" },
                    { "uk", "uk" },
                    { "ur", "ur" },
                    { "vi", "vi" },
                    { "cy", "cy" },
                    { "yi", "yi" }
                };
            }
        }

        #endregion Private methods

        #region Fields

        /// <summary>
        /// The language to translation mode map.
        /// </summary>
        private static Dictionary<string, string> _languageModeMap;

        #endregion Fields
    }
}

//TranslatorGoogleClass._languageModeMap = new Dictionary<string, string>();
//                TranslatorGoogleClass._languageModeMap.Add("Afrikaans", "af");
//                TranslatorGoogleClass._languageModeMap.Add("Albanian", "sq");
//                TranslatorGoogleClass._languageModeMap.Add("Arabic", "ar");
//                TranslatorGoogleClass._languageModeMap.Add("Armenian", "hy");
//                TranslatorGoogleClass._languageModeMap.Add("Azerbaijani", "az");
//                TranslatorGoogleClass._languageModeMap.Add("Basque", "eu");
//                TranslatorGoogleClass._languageModeMap.Add("Belarusian", "be");
//                TranslatorGoogleClass._languageModeMap.Add("Bengali", "bn");
//                TranslatorGoogleClass._languageModeMap.Add("Bulgarian", "bg");
//                TranslatorGoogleClass._languageModeMap.Add("Catalan", "ca");
//                TranslatorGoogleClass._languageModeMap.Add("Chinese", "zh-CN");
//                TranslatorGoogleClass._languageModeMap.Add("Croatian", "hr");
//                TranslatorGoogleClass._languageModeMap.Add("Czech", "cs");
//                TranslatorGoogleClass._languageModeMap.Add("Danish", "da");
//                TranslatorGoogleClass._languageModeMap.Add("Dutch", "nl");
//                TranslatorGoogleClass._languageModeMap.Add("English", "en");
//                TranslatorGoogleClass._languageModeMap.Add("Esperanto", "eo");
//                TranslatorGoogleClass._languageModeMap.Add("Estonian", "et");
//                TranslatorGoogleClass._languageModeMap.Add("Filipino", "tl");
//                TranslatorGoogleClass._languageModeMap.Add("Finnish", "fi");
//                TranslatorGoogleClass._languageModeMap.Add("French", "fr");
//                TranslatorGoogleClass._languageModeMap.Add("Galician", "gl");
//                TranslatorGoogleClass._languageModeMap.Add("German", "de");
//                TranslatorGoogleClass._languageModeMap.Add("Georgian", "ka");
//                TranslatorGoogleClass._languageModeMap.Add("Greek", "el");
//                TranslatorGoogleClass._languageModeMap.Add("Haitian Creole", "ht");
//                TranslatorGoogleClass._languageModeMap.Add("Hebrew", "iw");
//                TranslatorGoogleClass._languageModeMap.Add("Hindi", "hi");
//                TranslatorGoogleClass._languageModeMap.Add("Hungarian", "hu");
//                TranslatorGoogleClass._languageModeMap.Add("Icelandic", "is");
//                TranslatorGoogleClass._languageModeMap.Add("Indonesian", "id");
//                TranslatorGoogleClass._languageModeMap.Add("Irish", "ga");
//                TranslatorGoogleClass._languageModeMap.Add("Italian", "it");
//                TranslatorGoogleClass._languageModeMap.Add("Japanese", "ja");
//                TranslatorGoogleClass._languageModeMap.Add("Korean", "ko");
//                TranslatorGoogleClass._languageModeMap.Add("Lao", "lo");
//                TranslatorGoogleClass._languageModeMap.Add("Latin", "la");
//                TranslatorGoogleClass._languageModeMap.Add("Latvian", "lv");
//                TranslatorGoogleClass._languageModeMap.Add("Lithuanian", "lt");
//                TranslatorGoogleClass._languageModeMap.Add("Macedonian", "mk");
//                TranslatorGoogleClass._languageModeMap.Add("Malay", "ms");
//                TranslatorGoogleClass._languageModeMap.Add("Maltese", "mt");
//                TranslatorGoogleClass._languageModeMap.Add("Norwegian", "no");
//                TranslatorGoogleClass._languageModeMap.Add("Persian", "fa");
//                TranslatorGoogleClass._languageModeMap.Add("Polish", "pl");
//                TranslatorGoogleClass._languageModeMap.Add("Portuguese", "pt");
//                TranslatorGoogleClass._languageModeMap.Add("Romanian", "ro");
//                TranslatorGoogleClass._languageModeMap.Add("Russian", "ru");
//                TranslatorGoogleClass._languageModeMap.Add("Serbian", "sr");
//                TranslatorGoogleClass._languageModeMap.Add("Slovak", "sk");
//                TranslatorGoogleClass._languageModeMap.Add("Slovenian", "sl");
//                TranslatorGoogleClass._languageModeMap.Add("Spanish", "es");
//                TranslatorGoogleClass._languageModeMap.Add("Swahili", "sw");
//                TranslatorGoogleClass._languageModeMap.Add("Swedish", "sv");
//                TranslatorGoogleClass._languageModeMap.Add("Tamil", "ta");
//                TranslatorGoogleClass._languageModeMap.Add("Telugu", "te");
//                TranslatorGoogleClass._languageModeMap.Add("Thai", "th");
//                TranslatorGoogleClass._languageModeMap.Add("Turkish", "tr");
//                TranslatorGoogleClass._languageModeMap.Add("Ukrainian", "uk");
//                TranslatorGoogleClass._languageModeMap.Add("Urdu", "ur");
//                TranslatorGoogleClass._languageModeMap.Add("Vietnamese", "vi");
//                TranslatorGoogleClass._languageModeMap.Add("Welsh", "cy");
//                TranslatorGoogleClass._languageModeMap.Add("Yiddish", "yi");