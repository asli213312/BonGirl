using System;
using System.Collections.Generic;
using _BonGirl_.Editor.Scripts;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class LevelView : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private Image background;
        [SerializeField] private LevelData levelData;
        [SerializeField] private List<Image> originalStates;
        [SerializeField] private List<Image> differentStates;
        [SerializeField] private Button nextStateButton;
        [SerializeField] private Button backMenuButton;
        
        [Header("Options")]
        [Tooltip("If true, the difference buttons will be interactable at start")]
        [SerializeField] private bool needEnableDifferenceButtons;
        
        private DifferenceHinter _differenceHinter;
        private DifferentButton _differentButton;
        private DifferenceChecker[] _differenceCheckers;
        public Image BackGround => background;
        public LevelData LevelData => levelData;
        public List<Image> OriginalStates => originalStates;

        public Button NextStateButton => nextStateButton;
        
        public bool IsGallery { get; set; }

        private LevelSelector _levelSelector;
        private Previewer _previewer;

        private int _currentDifferentState;
        private int _currentOriginalState;

        private bool _levelIsCompleted;
        private const int FIRST_STATE = 0;

        public void Initialize(LevelSelector levelSelector, Previewer previewer)
        {
            _levelSelector = levelSelector;
            _previewer = previewer;
        }

        private void Awake()
        {
            IsGallery = false;
        }

        private void Start()
        {
            DisplayLevel();
            
            nextStateButton.interactable = false;
            nextStateButton.onClick.AddListener(ChangeState);
            backMenuButton.onClick.AddListener(SetBackState);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < differentStates.Count; i++)
            {
                DifferentImage newDifferentImage = differentStates[i].GetComponent<DifferentImage>();
                newDifferentImage.OnDifferenceFound -= ActivateNextState;
            }
            
            backMenuButton.onClick.RemoveListener(CloseView);
        }

        public void DisplayLevel()
        {
            _differenceCheckers = GetComponentsInChildren<DifferenceChecker>();
            _differenceHinter = GetComponentInChildren<DifferenceHinter>();
            
            DisplayImages();
        }

        private void DisplayImages()
        {
            for (int i = 0; i < originalStates.Count; i++)
            {
                if (i > 0)
                {
                    originalStates[i].gameObject.SetActive(false);
                } 
            }

            for (int i = 0; i < differentStates.Count; i++)
            {
                DifferentImage newDifferentImage = differentStates[i].GetComponent<DifferentImage>();

                if (newDifferentImage == differentStates[0].GetComponent<DifferentImage>())
                {
                    newDifferentImage.IsFirstState = true;
                }

                newDifferentImage.Initialize(_differenceCheckers, _differenceHinter, needEnableDifferenceButtons, originalStates);
                newDifferentImage.InitSoundInvoker(_levelSelector.SoundInvoker);
                newDifferentImage.OnDifferenceFound += ActivateNextState;

                if (i > 0)
                {
                    differentStates[i].gameObject.SetActive(false);
                }
            }
        }

        private void ActivateNextState()
        {
            if (!_levelIsCompleted)
            {
                nextStateButton.interactable = true;
            }
            else
            {
                nextStateButton.onClick.RemoveListener(ChangeState);
            }
        }

        private void ChangeState()
        {
            SetNextState();
            
            if (_currentDifferentState != 0 && _currentOriginalState != 0)
            {
                differentStates[_currentDifferentState].gameObject.SetActive(true);
                originalStates[_currentOriginalState].gameObject.SetActive(true);
                
                DifferentImage previousDifferentImage = differentStates[_currentDifferentState].GetComponent<DifferentImage>();
                Debug.Log("previousDifferentImage : ",previousDifferentImage);
                
                if (!previousDifferentImage.StateIsChanged)
                    nextStateButton.interactable = false;
                else
                    nextStateButton.interactable = true;
            }
            else
            {
                SetLastImage();
                nextStateButton.interactable = true;
                
                nextStateButton.onClick.AddListener(CloseView);
                _levelSelector.CurrentLevel.NextStateButton.onClick.AddListener(_previewer.SetPreviews);
                _levelSelector.CurrentLevel.levelData.Locked = false;

                _levelIsCompleted = true;
                Debug.Log("level Completed");
                Debug.Log("current level index: " + _levelSelector.CurrentLevel.LevelData.LevelIndex);
            }
        }

        private void SetNextState()
        {
            SetState(ref _currentDifferentState, differentStates, true);
            SetState(ref _currentOriginalState, originalStates, true);
        }

        private void SetBackState()
        {
            SetState(ref _currentDifferentState, differentStates, false);
            SetState(ref _currentOriginalState, originalStates, false);
        }

        private void SetState(ref int currentState, List<Image> states, bool forward)
        {
            if (currentState >= 0 && currentState < states.Count)
                states[currentState].gameObject.SetActive(false);

            if (forward)
                currentState = GetNextState(currentState, states.Count);
            else if (currentState != FIRST_STATE)
            {
                currentState = GetPreviousState(currentState, states.Count);
                
                DifferentImage newDifferentImage = differentStates[currentState].GetComponent<DifferentImage>();

                if (!newDifferentImage.CheckDifferencesDisabled())
                    nextStateButton.interactable = true;
                
                newDifferentImage.ResetDifferences();
            }
            else
            {
                _levelSelector.SelectorPanel.SetActive(true);
                CloseView();
                Destroy(gameObject, 3f);
                return;
            }

            if (currentState >= 0 && currentState < states.Count)
                states[currentState].gameObject.SetActive(true);
        }
        
        private int GetPreviousState(int currentState, int count)
        {
            if (count == 0)
                return -1;

            if (currentState <= 0)
                return count - 1;

            return currentState - 1;
        }
        
        private int GetNextState(int currentState, int count)
        {
            if (count == 0)
                return -1;

            if (currentState >= count - 1)
                return 0;

            return currentState + 1;
        }

        private void SetLastImage()
        {
            _currentOriginalState = originalStates.Count - 1;
            _currentDifferentState = differentStates.Count - 1;
            differentStates[_currentDifferentState].gameObject.SetActive(true);
            originalStates[_currentOriginalState].gameObject.SetActive(true);
        }

        private void CloseView()
        {
            gameObject.SetActive(false);
        }
    }
}