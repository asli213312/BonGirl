using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    [CreateAssetMenu(fileName = "New LevelData")]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public Sprite PreviewSprite { get; private set; }
        [field: SerializeField] public LevelView LevelView { get; private set; }
        [field: SerializeField] public int LevelIndex { get; private set; }
        [field: SerializeField] public bool Locked { get; set; }

        [Tooltip("Need for the level to be locked on start")] public bool NeedLock;
    }
}