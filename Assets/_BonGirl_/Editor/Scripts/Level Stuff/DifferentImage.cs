using System;
using System.Collections.Generic;
using System.Linq;
using _BonGirl_.Editor.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class DifferentImage : MonoBehaviour
    {
        public event Action OnDifferenceFound;
        public event Action StateChanged;

        private List<DifferentButton> _differenceButtons;

        private DifferenceChecker[] _differenceCheckers;
        private DifferenceHinter _differenceHinter;

        private SoundInvoker _soundInvoker;

        public bool IsFirstState { get; set; }
        public bool StateIsChanged { get; set; }
        
        private bool _needEnableDifferenceButtons;
        private List<Image> _originalStates;
        private List<OriginalButton> _originalButtonsList = new List<OriginalButton>();

        public void Initialize(DifferenceChecker[] checker, DifferenceHinter hinter, bool needEnableDifferenceButtons, List<Image> originalStates)
        {
            _differenceCheckers = checker;
            _differenceHinter = hinter;
            _needEnableDifferenceButtons = needEnableDifferenceButtons;
            _originalStates = originalStates;
        }

        public void InitSoundInvoker(SoundInvoker soundInvoker)
        {
            _soundInvoker = soundInvoker;
        }

        public void Start()
        {
            _differenceButtons = GetComponentsInChildren<DifferentButton>().ToList();

            if (_differenceButtons.Count != _differenceCheckers.Length)
            {
                Debug.LogError("The number of different buttons doesn't match the number of checkers.");
                return;
            }

            for (int i = 0; i < _differenceButtons.Count; i++)
            {
                _differenceButtons[i].Initialize(_differenceCheckers[i], _differenceHinter);
                _differenceButtons[i].Button.onClick.AddListener(DifferenceFound);
                _differenceButtons[i].OnClicked += _soundInvoker.InvokeClip;

                SpawnOriginalButtons(i);

                if (!_needEnableDifferenceButtons)
                    _differenceButtons[i].Button.image.color = Color.clear;

                _differenceHinter.Initialize(_differenceButtons);

                Invoke("InvokeResetCheckers", 0.1f);
            }

            OnDifferenceFound += _differenceHinter.DisableButton;
            StateChanged += _differenceHinter.ResetTimer;
            StateChanged += ResetOriginalButtons;
        }

        private void SpawnOriginalButtons(int i)
        {
            foreach (var originalState in _originalStates)
            {
                DifferentButton originalStateButton = Instantiate(_differenceButtons[i], originalState.transform);
                RectTransform buttonRectTransform = originalStateButton.GetComponent<RectTransform>();
                RectTransform originalRectTransform = _differenceButtons[i].Button.GetComponent<RectTransform>();

                buttonRectTransform.sizeDelta = originalRectTransform.sizeDelta;
                buttonRectTransform.anchoredPosition = originalRectTransform.anchoredPosition;

                originalStateButton.Initialize(_differenceCheckers[i], _differenceHinter);
                originalStateButton.OnClicked += _soundInvoker.InvokeClip;
                OriginalButton originalButton = originalStateButton.gameObject.AddComponent<OriginalButton>();
                originalButton.SelectNeighbor(_differenceButtons[i]);

                _originalButtonsList.Add(originalButton);
            }
        }

        private void OnEnable()
        {
            foreach (var originalButton in _originalButtonsList)
            {
                originalButton.CheckNeighborIsDisabled();
            }
        }

        private void ResetOriginalButtons()
        {
            foreach (var originalButton in _originalButtonsList)
            {
                originalButton.transform.Deactivate();
            }
        }

        private void InvokeResetCheckers()
        {
            if (!IsFirstState)
            {
                for (int i = 0; i < _differenceButtons.Count; i++)
                {
                    _differenceButtons[i].ResetChecker();    
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var differentButton in _differenceButtons)
            {
                if (differentButton == null) return;
                
                differentButton.Button.onClick.RemoveListener(DifferenceFound);
                differentButton.OnClicked -= _soundInvoker.InvokeClip;
            }

            OnDifferenceFound -= _differenceHinter.DisableButton;
            StateChanged -= _differenceHinter.ResetTimer;
            StateChanged -= ResetOriginalButtons;
        }

        private void OnDisable()
        {
            StateChanged?.Invoke();
        }

        private void DifferenceFound()
        {
            Debug.Log("check difference if found");
        
            for (int i = 0; i < _differenceButtons.Count; i++)
            {
                if (_differenceButtons[i].Button.interactable)
                {
                    return;
                }
            }

            StateIsChanged = true;
            Debug.Log("Differences were found!");
            OnDifferenceFound?.Invoke();
        }

        public bool CheckDifferencesDisabled()
        {
            for (int i = 0; i < _differenceButtons.Count; i++)
            {
                if (_differenceButtons[i].Button.interactable == false)
                {
                    return true;
                }
            }

            return false;
        }

        public void ResetDifferences()
        {
            for (int i = 0; i < _differenceButtons.Count; i++)
            {
                _differenceButtons[i].Button.interactable = true;
            }
        }
    }
}