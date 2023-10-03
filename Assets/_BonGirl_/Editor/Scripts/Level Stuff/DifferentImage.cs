using System;
using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class DifferentImage : MonoBehaviour
    {
        public event Action OnDifferenceFound;
        public event Action StateChanged;

        private DifferentButton[] _differenceButtons;

        public DifferentButton[] DifferenceButtons => _differenceButtons;
        
        private DifferenceChecker[] _differenceCheckers;
        private DifferenceHinter _differenceHinter;

        private SoundInvoker _soundInvoker;
        
        public bool IsFirstState { get; set; }

        private int _currentIndex;
        private bool _needEnableDifferenceButtons;

        public void Initialize(DifferenceChecker[] checker, DifferenceHinter hinter, bool needEnableDifferenceButtons)
        {
            _differenceCheckers = checker;
            _differenceHinter = hinter;
            _needEnableDifferenceButtons = needEnableDifferenceButtons;
        }

        public void InitSoundInvoker(SoundInvoker soundInvoker)
        {
            _soundInvoker = soundInvoker;
        }

        private void Start()
        {
            _differenceButtons = GetComponentsInChildren<DifferentButton>();
            
            if (_differenceButtons.Length != _differenceCheckers.Length)
            {
                Debug.LogError("The number of different buttons doesn't match the number of checkers.");
                return;
            }

            for (int i = 0; i < _differenceButtons.Length; i++)
            {
                _differenceButtons[i].Initialize(_differenceCheckers[i], _differenceHinter);
                _differenceButtons[i].Button.onClick.AddListener(DifferenceFound);
                _differenceButtons[i].OnClicked += _soundInvoker.InvokeClip;
                
                if (!_needEnableDifferenceButtons)
                    _differenceButtons[i].Button.image.color = Color.clear;    
                
                _differenceHinter.Initialize(_differenceButtons);

                Invoke("InvokeResetCheckers", 0.1f);
            }

            OnDifferenceFound += _differenceHinter.DisableButton;
            StateChanged += _differenceHinter.ResetTimer;
        }

        private void InvokeResetCheckers()
        {
            if (!IsFirstState)
            {
                for (int i = 0; i < _differenceButtons.Length; i++)
                {
                    _differenceButtons[i].ResetChecker();    
                }
            }
        }
        
        private void OnDestroy()
        {
            foreach (var differentButton in _differenceButtons)
            {
                differentButton.Button.onClick.RemoveListener(DifferenceFound);
                differentButton.OnClicked -= _soundInvoker.InvokeClip;
            }
            
            OnDifferenceFound -= _differenceHinter.DisableButton;
            StateChanged -= _differenceHinter.ResetTimer;
        }

        private void OnDisable()
        {
            StateChanged?.Invoke();
        }

        private void DifferenceFound()
        {
            for (int i = 0; i < _differenceButtons.Length; i++)
            {
                if (_differenceButtons[i].Button.interactable)
                {
                    return;
                }
            }
            
            OnDifferenceFound?.Invoke();
        }

        public bool CheckDifferencesDisabled()
        {
            for (int i = 0; i < _differenceButtons.Length; i++)
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
            for (int i = 0; i < _differenceButtons.Length; i++)
            {
                _differenceButtons[i].Button.interactable = true;
            }
        }
    }
}