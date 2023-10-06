using UnityEngine;

namespace _BonGirl_.Editor.Scripts.Utility
{
    public static class TransformExtensions
    {
        public static void Destroy(this Transform transform, float delay = 0f)
        {
            Object.Destroy(transform.gameObject, delay);
        }
        
        public static void Activate(this Transform transform)
        {
            transform.gameObject.SetActive(true);
        }
        
        public static void Deactivate(this Transform transform)
        {
            transform.gameObject.SetActive(false);
        }
    }
}