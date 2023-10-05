using System;
using UnityEngine;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class OriginalButton : MonoBehaviour
    {
        private DifferentButton _differentNeighbor;
        private DifferentButton _thisDifferentButton;
        private Button _button;
        

        private void Start()
        {
            _button = GetComponent<Button>();
            _thisDifferentButton = GetComponent<DifferentButton>();

            _button.onClick.AddListener(InvokeDifferentButtonClick);
            _differentNeighbor.Button.onClick.AddListener(InvokeThisButtonClick);

            ClearColorButton();
        }

        private void OnEnable()
        {
            //IsInitialized = false;
            Invoke("CheckNeighborIsDisabled", 1f);
        }

        public void CheckNeighborIsDisabled()
        {
            if (_differentNeighbor.gameObject.activeInHierarchy)
                gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            Invoke("UnsubscribeEvents", 0.3f);   
        }

        private void UnsubscribeEvents()
        {
            _button.onClick.RemoveListener(InvokeDifferentButtonClick);
            _differentNeighbor.Button.onClick.RemoveListener(InvokeThisButtonClick);
        }

        private void InvokeDifferentButtonClick()
        {
            _differentNeighbor.Button.onClick.Invoke();
            _button.interactable = false;
        }

        public void InvokeThisButtonClick()
        {
            _thisDifferentButton.SetStateAfterClicked();
            _button.interactable = false;
        }
        
        public void ClearColorButton() => _button.image.color = Color.clear;

        public void SelectNeighbor(DifferentButton differentButton)
        {
            _differentNeighbor = differentButton;
        } 
    }
}