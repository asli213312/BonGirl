using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _BonGirl_.Editor.Scripts
{
    public class LevelPreview : MonoBehaviour, IPointerDownHandler
    {
        private Gallery _gallery;
        private Previewer _previewer;
        private LevelView _levelView;
        private bool _isLocked;

        public void Initialize(LevelView levelView,  bool isLocked, Previewer previewer)
        {
            _levelView = levelView;
            _isLocked = isLocked;
            _previewer = previewer;
        }

        public void SetGallery(Gallery gallery) => _gallery = gallery;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isLocked)
            {
                Debug.LogWarning("Level is locked");
                return;
            }

            _gallery.gameObject.SetActive(false);
            
            _previewer.GetLevelSelector().CurrentLevel = _levelView;
            _previewer.PreviewPanel.gameObject.SetActive(true);
            _levelView.IsGallery = true;
            
            
            _previewer.SetPreviews();
        }
    }
}