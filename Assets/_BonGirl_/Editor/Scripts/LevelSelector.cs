using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class LevelSelector : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private SoundInvoker soundInvoker;
        [SerializeField] private Previewer previewer;
        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private LevelSelectorPreview levelSelectorPreviewPrefab;
        [SerializeField] private GameObject selectorPanel;
        [SerializeField] private Transform content;
        [Header("Options")]
        [SerializeField] private GalleryData data;
        [SerializeField] private GameConfig gameConfig;
        
        public SoundInvoker SoundInvoker => soundInvoker;
        public GameConfig GameConfig => gameConfig;
        public GalleryData GalleryData => data;
        public GameObject SelectorPanel => selectorPanel;
        private List<LevelView> _createdLevels = new();
        public List<LevelView> ExhaustedLevels { get; set; } = new();

        public LevelView CurrentLevel { get; set; }

        private void Awake()
        {
            InitializeSelector();
            
            soundInvoker.Initialize(this);
        }

        private void InitializeSelector()
        {
            SpawnSelector();
        }

        private void SpawnSelector()
        {
            foreach (var levelView in data.Levels)
            {
                LevelSelectorPreview newLevel = Instantiate(levelSelectorPreviewPrefab, content.position, Quaternion.identity, content.transform);
                Image previewImage = newLevel.GetComponent<Image>();
                
                previewImage.sprite = levelView.LevelData.PreviewSprite;
        
                newLevel.Initialize(levelView, mainCanvas, this, previewer);
                newLevel.SetGallery(previewer.Gallery);

                _createdLevels.Add(levelView);
            }
        }

        public void SpawnNewLevel()
        {
            Debug.Log("Spawning new level...");

            if (CurrentLevel == null) return;
            
            int nextLevelIndex = CurrentLevel.LevelData.LevelIndex;
            Destroy(CurrentLevel.gameObject);
            
            Debug.Log("next level index: " + nextLevelIndex + 1);
            LevelView newLevel = data.Levels[nextLevelIndex];
            Debug.Log("new levelIndex: " + newLevel.LevelData.LevelIndex);
            ExhaustedLevels.Add(newLevel);

            LevelView newLevelView = Instantiate(newLevel, mainCanvas.transform.position, Quaternion.identity, mainCanvas.transform);
            newLevelView.Initialize(this, previewer);
            CurrentLevel = newLevelView;

            if (newLevelView != null)
            {
                newLevelView.DisplayLevel();
                newLevelView.NextStateButton.onClick.AddListener(previewer.SetPreviews);
            }
            else
            {
                Debug.LogWarning("No more levels available.");
            }
        }
    }
}