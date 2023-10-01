using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace _BonGirl_.Editor.Scripts
{
    public class LevelFactory : MonoBehaviour
    {
        [SerializeField] private Previewer previewer;
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private List<LevelView> levels;

        private List<LevelView> ExhaustedLevels {get => levels; set => levels = value; }
        
        public LevelView CurrentLevel { get; private set; }

        private void Start()
        {
            //SpawnLevel();
        }
        
        public void SpawnLevel()
        {
            int randomLevelIndex = UnityEngine.Random.Range(0, levels.Count);
            LevelView randomLevel = levels[randomLevelIndex];
            ExhaustedLevels.Remove(randomLevel);
            
            GameObject newLevel = Instantiate(randomLevel.gameObject, mainCanvas.transform.position, Quaternion.identity, mainCanvas.transform);
            LevelView newLevelView = newLevel.GetComponent<LevelView>();
            CurrentLevel = newLevelView;

            if (newLevelView != null)
            {
                newLevelView.DisplayLevel();
                newLevelView.NextStateButton.onClick.AddListener(previewer.SetPreviews);
            }
        }
    }
}