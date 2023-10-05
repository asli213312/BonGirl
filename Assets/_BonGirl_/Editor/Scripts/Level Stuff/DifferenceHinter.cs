using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    public class DifferenceHinter : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Sprite startSprite;
        [SerializeField] private Image differenceRect;
        [SerializeField] private float timeToEnable;

        public Image DifferenceRect => differenceRect;
        
        private Button _button;
        private DifferentButton _selectedDifferentButton;
        private Sprite _startSprite;
        private Image _disableImage;
        private ButtonAnimation _animator;

        private List<DifferentButton> _differentButtons;

        private float _timer;
        private bool _needResetTimer;

        public void Initialize(List<DifferentButton> differentButton)
        {
            _differentButtons = differentButton;
        }

        private void Start()
        {
            _button ??= GetComponent<Button>();
            _startSprite ??= GetComponent<Image>().sprite;
            _animator ??= GetComponent<ButtonAnimation>();

            differenceRect = Instantiate(differenceRect, transform.parent);
            differenceRect.gameObject.SetActive(false);
            _button.interactable = false;
            _needResetTimer = true;
            
            _animator.DisableAnimator();
        }

        private void Update()
        {
            if (_needResetTimer)
                _timer += Time.deltaTime;

            if (_timer >= timeToEnable && _needResetTimer)
            {
                _button.interactable = true;

                _timer = 0;
                _needResetTimer = false;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Image image = GetComponent<Image>();
            if (image != null)
            {
                image.sprite = startSprite;
            }

            if (_button.interactable)
                SetPositionAtButton();
            
            _button.interactable = false;
            _animator.BeginBackStateAnimation();

            StartCoroutine(DisableRectWithDelay());

            _needResetTimer = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_button.interactable) return;
            
            _animator.BeginStartStateAnimation();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_button.interactable) return;
            
            _animator.BeginBackStateAnimation();
        }

        private void SetPositionAtButton()
        {
            Vector3 rectPositionAtButton = GetPositionRandomButton(_differentButtons);
            differenceRect.gameObject.SetActive(true);
            differenceRect.transform.SetParent(_selectedDifferentButton.transform);
            
            differenceRect.transform.position = rectPositionAtButton;
        }

        private Vector3 GetPositionRandomButton(List<DifferentButton> differentButtons)
        {
            List<DifferentButton> activeButtons = new List<DifferentButton>();
            
            foreach (var button in differentButtons)
            {
                if (button.gameObject.activeInHierarchy && button.Button.interactable)
                    activeButtons.Add(button);
            }

            if (activeButtons.Count == 0)
            {
                Debug.LogWarning("No active and interactable buttons available.");
                return Vector3.up * 10;
            }

            int randomIndex = UnityEngine.Random.Range(0, activeButtons.Count);
            DifferentButton randomButton = activeButtons[randomIndex];
            _selectedDifferentButton = randomButton;

            return randomButton.transform.position;
        }

        public void ResetTimer()
        {
            _needResetTimer = true;
        }

        public void DisableButton()
        {
            _button.interactable = false;
            _needResetTimer = false;
        }

        public void DisableRect()
        {
            differenceRect.gameObject.SetActive(false);            
        }

        private IEnumerator DisableRectWithDelay()
        {
            yield return new WaitForSeconds(timeToEnable * 2);

            differenceRect.gameObject.SetActive(false);
        }
    }
}