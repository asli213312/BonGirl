using System;
using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class DifferentButton : MonoBehaviour
    {
        private DifferenceSelector _differenceSelector;
        public Button Button { get; private set; }

        private DifferenceChecker _differenceChecker;
        private DifferenceHinter _differenceHinter;

        public event Action OnClicked;

        public void Initialize(DifferenceChecker checker, DifferenceHinter hinter)
        {
            _differenceChecker = checker;
            _differenceHinter = hinter;
        }

        private void Start()
        {
            _differenceSelector = GetComponentInChildren<DifferenceSelector>();
            _differenceSelector.gameObject.SetActive(false);

            OnClicked += _differenceHinter.DisableRect;
            OnClicked += _differenceHinter.ResetTimer;
        }

        private void OnEnable()
        {
            gameObject.SetActive(true);

            Button = GetComponent<Button>();
            Button.onClick.AddListener(OnClick);
        }

        public void ResetChecker()
        {
            _differenceChecker.CheckerAnimation.BeginBackStateAnimation();
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClick);
            OnClicked -= _differenceHinter.DisableRect;
            OnClicked -= _differenceHinter.ResetTimer;
        }

        public void OnClick()
        {
            Button.interactable = false;
            
            OnClicked?.Invoke();
            
            _differenceSelector.gameObject.SetActive(true);
            _differenceChecker.CheckerAnimation.BeginStartStateAnimation();
            Debug.Log("Different Found");
        }
    }
}