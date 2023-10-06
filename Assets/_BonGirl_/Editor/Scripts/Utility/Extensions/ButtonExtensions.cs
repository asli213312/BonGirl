using UnityEngine.Events;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts.Utility
{
    public static class ButtonExtensions
    {
        public static void AddListener(this Button button, UnityAction callBack)
        {
            button.onClick.AddListener(callBack);
        }
        
        public static void RemoveListener(this Button button, UnityAction callBack)
        {
            button.onClick.RemoveListener(callBack);
        }

        public static void RemoveAllListeners(this Button button)
        {
            button.onClick.RemoveAllListeners();
        }
    }
}