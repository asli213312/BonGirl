using UnityEngine;

namespace _BonGirl_.Editor.Scripts.Utility
{
    public static class GameObjectExtensions
    {
        public static void Destroy(this GameObject gameObject, float delay = 0f)
        {
            Object.Destroy(gameObject, delay);
        }
        
        public static void Activate(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
        
        public static void Deactivate(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}