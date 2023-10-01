using UnityEngine;

namespace _BonGirl_.Editor.Scripts
{
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public bool ExplainerIsInitialized { get; set; }
    }
}