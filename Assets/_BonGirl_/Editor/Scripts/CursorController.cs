using UnityEditor;
using UnityEngine;

namespace _BonGirl_.Editor.Scripts
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        private void Awake()
        {
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            mousePosition.z = 0;

            transform.position = mousePosition;
        }
    }
}