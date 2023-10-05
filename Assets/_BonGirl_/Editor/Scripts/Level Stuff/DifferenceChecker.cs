using UnityEditor;
using UnityEngine;

namespace _BonGirl_.Editor.Scripts
{
    public class DifferenceChecker : MonoBehaviour
    {
        public ButtonAnimation CheckerAnimation { get; private set; }

        private void Start()
        {
            CheckerAnimation = GetComponent<ButtonAnimation>();
            CheckerAnimation.DisableAnimator();
        }
    }
}