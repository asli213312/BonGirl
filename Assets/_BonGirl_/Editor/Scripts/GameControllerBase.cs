using UnityEngine;
using Zenject;

namespace _BonGirl_.Editor.Scripts
{
    public abstract class GameControllerBase : MonoBehaviour
    {
        protected int CurrentLevel;
        protected Level CurrentLevelData;
        protected GameConfig GameConfig;
        protected ResourceLoader ResourceLoader;
        protected LevelGenerator LevelGenerator;

        public abstract void StartGame();
        public abstract void NextLevel();
    }
}