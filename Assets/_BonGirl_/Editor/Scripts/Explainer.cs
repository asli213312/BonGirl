using System;
using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class Explainer : MonoBehaviour
    {
        [SerializeField] private GameObject explainerPanel;
        [SerializeField] private ButtonAnimation explainerAnimation;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Text explainerText;

        private const string _explainerNextText = "USE A HINT"; 
        private int _nextClickCounter;

        public event Action OnExplainerClose;

        private void Awake()
        {
            nextButton.onClick.AddListener(ChangeStartState);
        }

        private void OnDestroy()
        {
            nextButton.onClick.RemoveListener(ChangeStartState);
        }

        private void ChangeStartState()
        {
            explainerText.text = _explainerNextText;
            
            _nextClickCounter++;

            if (_nextClickCounter == 2)
            {
                CloseExplainer();
                OnExplainerClose?.Invoke();
            }
            else if (_nextClickCounter == 1)
            {
                explainerAnimation.BeginBackStateAnimation();
            }
        }

        private void CloseExplainer()
        {
            explainerPanel.SetActive(false);
            
            Destroy(explainerPanel, 3f);
        }

        public void ShowExplainer()
        {
            explainerPanel.SetActive(true);
            
            Invoke("EnableStartAnimation", 1f);
        }

        private void EnableStartAnimation()
        {
            explainerAnimation.BeginStartStateAnimation();
        }
    }
}