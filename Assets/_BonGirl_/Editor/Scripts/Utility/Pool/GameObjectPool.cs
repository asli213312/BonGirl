using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _BonGirl_.Editor.Scripts.Utility.Pool
{
    public class GameObjectPool : BasePool<GameObject>
    {
        public GameObjectPool(GameObject prefab, int preLoadCount) 
            : base(
                () => Preload(prefab), 
                GetAction, 
                ReturnAction, 
                preLoadCount)
        {
        }

        public static GameObject Preload(GameObject prefab) => Object.Instantiate(prefab);
        public static void GetAction(GameObject @object) => @object.Activate();
        public static void ReturnAction(GameObject @object) => @object.Deactivate();
    }
}