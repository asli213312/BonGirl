using UnityEngine;
using UnityEngine.EventSystems;

namespace _BonGirl_.Editor.Scripts
{
    public class LevelSelectorPreview : MonoBehaviour, IPointerDownHandler
    {
        private Gallery _gallery;
        private Previewer _previewer;
        private LevelSelector _levelSelector;
        private LevelView _levelView;
        private Canvas _mainCanvas;

        public void Initialize(LevelView levelView, Canvas mainCanvas, LevelSelector levelSelector, Previewer previewer)
        {
            _levelView = levelView;
            _mainCanvas = mainCanvas;
            _levelSelector = levelSelector;
            _previewer = previewer;
        }

        public void SetGallery(Gallery gallery) => _gallery = gallery;

        public void OnPointerDown(PointerEventData eventData)
        {
            _levelSelector.SelectorPanel.SetActive(false);
            
            if (!_levelSelector.GameConfig.ExplainerIsInitialized)
            {
                _previewer.SetExplainer();
                _levelSelector.GameConfig.ExplainerIsInitialized = true;

                if (_levelSelector.GameConfig.ExplainerIsInitialized) return;
            }
            
            _levelSelector.CurrentLevel = Instantiate(_levelView, _mainCanvas.transform.position, Quaternion.identity, _mainCanvas.transform);
            _levelSelector.CurrentLevel.Initialize(_levelSelector, _previewer);
            
            _levelSelector.ExhaustedLevels.Add(_levelSelector.CurrentLevel);
        }
    }
}