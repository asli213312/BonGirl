using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class GameHandler : GameControllerBase
    {
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private ResourceLoader resourceLoader;
        private LevelGenerator _levelGenerator;
        private InputSystem _inputSystem;
        private DifferentImage _differentImage;
        
        private const int FIRST_LEVEL = 0;

        private void InitializeGamePlay()
        {
            _inputSystem = new InputSystem();
        }

        public override void StartGame()
        {
            
        }

        public override void NextLevel()
        {
            
        }

        private void SetLevel(int level)
        {
            Sprite originalImage = LoadImage("OriginalImage");
            Sprite differentImage = LoadImage("DifferentImage");
            Sprite background = LoadImage("BG_White");

            CurrentLevelData = _levelGenerator.GenerateLevel(originalImage, differentImage, background);
            CurrentLevel = level;
        }

        private Sprite LoadImage(string imagePath)
        {
            return null;
        }
    }
}