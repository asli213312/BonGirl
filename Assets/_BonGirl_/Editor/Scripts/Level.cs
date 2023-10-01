using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    [Serializable]
    public class Level
    {
        [field: SerializeField]
        public Sprite OriginalImage { get; set; }
        [field: SerializeField]
        public Sprite DifferentImage { get; set; }
        public Sprite Background { get; set; }
        [field: SerializeField]
        public List<Vector2> DifferencePositions { get; set; }
    }
}