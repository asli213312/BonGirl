using UnityEngine;

namespace _BonGirl_.Editor.Scripts
{
    public class GalleryData : ScriptableObject
    {
        //[field: SerializeField] public GameObject DifferenceSelectorPrefab { get; private set; }
        [field: SerializeField] public bool ResetLevelsOnStart { get; private set; }
        [field: SerializeField] public Sprite SpriteLevelLocked { get; private set; }
        [field: SerializeField] public LevelView[] Levels { get; private set; }
    }
}