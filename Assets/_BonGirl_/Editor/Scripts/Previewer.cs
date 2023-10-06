using System;
using System.Collections.Generic;
using _BonGirl_.Editor.Scripts.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class Previewer : MonoBehaviour
    {
        [SerializeField] private Gallery gallery;
        [SerializeField] private LevelSelector levelSelector;
        [SerializeField] private Explainer explainer;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button galleryButton;
        [SerializeField] private GameObject previewPanel;
        [SerializeField] private GameObject galleryPanel;
        [SerializeField] private Image background;
        [SerializeField] private Image[] previewImages;
        
        public Gallery Gallery => gallery;
        public GameObject PreviewPanel => previewPanel;

        private void Start()
        {
            explainer.OnExplainerClose += OpenLevelSelector;
            galleryButton.AddListener(OpenGallery);
            nextLevelButton.AddListener(levelSelector.SpawnNewLevel);
            nextLevelButton.AddListener(CloseSelector);
        }

        private void OnDestroy()
        {
            explainer.OnExplainerClose += OpenLevelSelector;
            galleryButton.RemoveListener(OpenGallery);
            nextLevelButton.RemoveListener(levelSelector.SpawnNewLevel);
            nextLevelButton.RemoveListener(CloseSelector);
        }

        public void SetPreviews()
        {
            for (int i = 0; i < previewImages.Length; i++)
            {
                previewImages[i].sprite = levelSelector.CurrentLevel.OriginalStates[i].sprite;
            }
            
            background.sprite = levelSelector.CurrentLevel.BackGround.sprite;
            
            if (!previewPanel.activeSelf)
                previewPanel.Activate();
            
            if (levelSelector.CurrentLevel.IsGallery)
                SetNextButtonActive(false);
            else
                SetNextButtonActive(true);
            
            if (IsMaxLevel()) nextLevelButton.gameObject.Deactivate();

            if (!levelSelector.CurrentLevel.LevelData.AchCompleted && levelSelector.CurrentLevel.IsGallery == false)
            {
                SteamAchievements steamAchievements = FindObjectOfType<SteamAchievements>();
                if (steamAchievements != null)
                {
                    string achName = "Girl_" + levelSelector.CurrentLevel.LevelData.LevelIndex;
                    string testAchName = "ACH_WIN_ONE_GAME";
                    
                    if (levelSelector.GameConfig.CheckDefaultGameOnAchievements == false)
                        steamAchievements.GainAchievement(achName);
                    else
                        steamAchievements.GainAchievement(testAchName);
                }

                levelSelector.CurrentLevel.LevelData.AchCompleted = true;
            }

            Debug.Log("Previews were installed");
        }

        private bool IsMaxLevel()
        {
            return levelSelector.CurrentLevel.LevelData.LevelIndex == levelSelector.GalleryData.Levels.Length;
        }

        public void SetExplainer()
        {
            explainer.ShowExplainer();
        }

        private void OpenGallery()
        {
            galleryPanel.Activate();
        }

        public void OpenLevelSelector()
        {
            levelSelector.SelectorPanel.Activate();
        }

        private void CloseSelector()
        {
            galleryPanel.Deactivate();
            previewPanel.Deactivate();
        }

        public void SetNextButtonActive(bool active) => nextLevelButton.gameObject.SetActive(active);
        public LevelSelector GetLevelSelector() => levelSelector;
    }
}