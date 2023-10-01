using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class LevelGenerator
    {
        private DifferenceFinder _differenceFinder;
        
        public LevelGenerator(DifferenceFinder differenceFinder) 
        {
            _differenceFinder = differenceFinder;
        }
        
        public Level GenerateLevel(Sprite originalImage, Sprite differentImage, Sprite background)
        {
            Level newLevel = new Level();

            newLevel.Background = background;
            newLevel.OriginalImage = originalImage;
            newLevel.DifferentImage = differentImage;
            newLevel.DifferencePositions = _differenceFinder.FindDifferences(originalImage, differentImage);

            return newLevel;
        }
        
        
    }
}