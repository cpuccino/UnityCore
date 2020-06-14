using System;
using System.Collections;
using UnityEngine;

namespace Ukiyo.Unity.Core.Page
{
    // Whether the page is transitioning on, off or neutral
    // Must match the animation state
    public enum PageAnimationState
    {
        None, On, Off
    }
    
    public class Page: MonoBehaviour
    {
        Animator animator;

        public PageAnimationState AnimationState { get; private set; }

        public bool Active { get; private set; }

        [SerializeField] PageType type;
        public PageType Type { get { return type; } set { type = value; } }

        [SerializeField] bool useAnimation;
        public bool UseAnimation { get { return useAnimation; } }

        void OnEnable()
        {
            animator = GetComponent<Animator>();
            if(!animator) return;

            animator.SetBool("on", false);
            animator.enabled = UseAnimation;
        }
        
        public void Animate(bool transitionOn)
        {
            if(UseAnimation && animator != null)
            {
                animator.SetBool("on", transitionOn);
                StopCoroutine(AwaitAnimation(transitionOn));
                StartCoroutine(AwaitAnimation(transitionOn));
                return;
            }
            SetActive(transitionOn);
        }

        IEnumerator AwaitAnimation(bool transitionOn)
        {
            AnimationState = transitionOn ? PageAnimationState.On : PageAnimationState.Off;

            while(!animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationState.ToString()))
            {
                yield return null;
            }

            while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || animator.IsInTransition(0))
            {
                yield return null;
            }

            AnimationState = PageAnimationState.None;
            SetActive(transitionOn);
        }

        private void SetActive(bool transitionOn)
        {
            Active = transitionOn;
            if(!transitionOn)
            {
                gameObject.SetActive(false);
            }
        }

    }
}