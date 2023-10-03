using System;
using System.Collections.Generic;
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
            galleryButton.onClick.AddListener(OpenGallery);
            nextLevelButton.onClick.AddListener(levelSelector.SpawnNewLevel);
            nextLevelButton.onClick.AddListener(CloseSelector);
        }

        private void OnDestroy()
        {
            explainer.OnExplainerClose += OpenLevelSelector;
            galleryButton.onClick.RemoveListener(OpenGallery);
            nextLevelButton.onClick.RemoveListener(levelSelector.SpawnNewLevel);
            nextLevelButton.onClick.RemoveListener(CloseSelector);
        }

        public void SetPreviews()
        {
            for (int i = 0; i < previewImages.Length; i++)
            {
                previewImages[i].sprite = levelSelector.CurrentLevel.OriginalStates[i].sprite;
            }
            
            background.sprite = levelSelector.CurrentLevel.BackGround.sprite;
            
            if (!previewPanel.activeSelf)
                previewPanel.SetActive(true);
            
            if (levelSelector.CurrentLevel.IsGallery)
                SetNextButtonActive(false);
            else
                SetNextButtonActive(true);
            
            if (IsMaxLevel()) nextLevelButton.interactable = false;

            if (!levelSelector.CurrentLevel.LevelData.AchCompleted && levelSelector.CurrentLevel.IsGallery == false)
            {
                SteamAchievements steamAchievements = FindObjectOfType<SteamAchievements>();
                if (steamAchievements != null)
                {
                    string achName = "Girl_" + levelSelector.CurrentLevel.LevelData.LevelIndex;
                    steamAchievements.GainAchievement(achName);
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
            galleryPanel.SetActive(true);
        }

        public void OpenLevelSelector()
        {
            levelSelector.SelectorPanel.SetActive(true);
        }

        private void CloseSelector()
        {
            galleryPanel.SetActive(false);
            previewPanel.SetActive(false);
        }

        public void SetNextButtonActive(bool active) => nextLevelButton.gameObject.SetActive(active);
        public LevelSelector GetLevelSelector() => levelSelector;
    }
}