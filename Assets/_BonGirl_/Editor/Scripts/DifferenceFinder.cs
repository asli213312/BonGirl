using System.Collections.Generic;
using UnityEngine;

namespace _BonGirl_.Editor.Scripts
{
    public abstract class DifferenceFinder
    {
        public abstract List<Vector2> FindDifferences(Sprite originalImage, Sprite differentImage);
    }
}