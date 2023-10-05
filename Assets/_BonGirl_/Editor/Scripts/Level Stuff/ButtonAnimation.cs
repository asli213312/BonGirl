﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _BonGirl_.Editor.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private bool needHover;
        [SerializeField] private bool needBack;
        [SerializeField] private AnimationClip animationStart;
        [SerializeField] private AnimationClip animationBack;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (needHover)
            {
                _animator.enabled = true;
                _animator.Play(animationStart.name, 0);   
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (needHover == false) return;
            
            if (needBack)
                _animator.Play(animationBack.name, 0);
        }

        public void SetModeAnimation(AnimationClip clip, WrapMode mode)
        {
            clip.wrapMode = mode;
        }

        public AnimationClip BeginStartStateAnimation()
        {
            SetStateAnimation(animationStart);

            return animationStart;
        }

        public void BeginBackStateAnimation()
        {
            SetStateAnimation(animationBack);
        }

        private void SetStateAnimation(AnimationClip clip)
        {
            _animator.enabled = true;
            _animator.Play(clip.name, 0);
        }
        
        public void DisableAnimator()
        {
            _animator.enabled = false;
        }
    }
}