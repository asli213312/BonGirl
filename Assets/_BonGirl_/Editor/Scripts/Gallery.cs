using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class Gallery : MonoBehaviour
    {
        [Header("Data")] 
        [SerializeField] private Previewer previewer;
        [SerializeField] private LevelPreview previewPrefab;
        [SerializeField] private Transform content;
        [Header("Options")]
        [SerializeField] private GalleryData data;

        public LevelPreview PreviewPrefab => previewPrefab;

        private bool _isInitialized;

        private void Start()
        {
            _isInitialized = true;
        }

        private void OnEnable()
        {
            InitializeGallery();
        }

        private void InitializeGallery()
        {
            CheckResetLevels();
            RefreshGallery();
            SpawnGallery();
        }

        private void SpawnGallery()
        {
            foreach (var levelView in data.Levels)
            {
                if (levelView != null && data.Levels.Contains(levelView))
                { 
                    LevelPreview newPreview = Instantiate(previewPrefab, content.position, Quaternion.identity, content.transform);
                    Image previewImage = newPreview.GetComponent<Image>();

                    if (levelView.LevelData.Locked)
                        previewImage.sprite = data.SpriteLevelLocked;
                    else
                        previewImage.sprite = levelView.LevelData.PreviewSprite;
        
                    newPreview.Initialize(levelView, levelView.LevelData.Locked, previewer);
                    newPreview.SetGallery(this);
                }
            }
        }

        private void CheckResetLevels()
        {
            if (data.ResetLevelsOnStart && !_isInitialized)
            {
                foreach (var levelView in data.Levels)
                {
                    if (levelView.LevelData.NeedLock)
                        levelView.LevelData.Locked = true;
                }
            }
        }

        private void RefreshGallery()
        {
            foreach (Transform child  in content)
            {
                Destroy(child.gameObject);
            }
        }
    }
}