using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Localization;
using UnityEngine.Localization.Settings;

namespace _BonGirl_.Editor.Scripts
{
    public class LanguageSelector : MonoBehaviour
    {
        public void SetLocale(string language)
        {
            switch (language)
            {
                case "en":
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
                    break;
                case "fr":
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
                    break;
                case "it":
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[4];
                    break;
                case "es":
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[7];
                    break;
                case "de":
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[3];
                    break;
                case "ja":
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[5];
                    break;
                case "ch":
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
                    break;
                case "ru":
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[6];
                    break;
                
                default:
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];                        
                    break;
            }
        }
    }
}