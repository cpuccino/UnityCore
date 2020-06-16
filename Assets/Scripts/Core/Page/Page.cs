using System.Collections;
using UnityEngine;

namespace UnityCore.Page
{
    // Whether the page is transitioning on, off or neutral
    // Must match the animation state
    public enum PageAnimationState
    {
        None, On, Off
    }
    
    public class Page: MonoBehaviour
    {
        Animator _animator;
        [SerializeField] PageType _type = default;
        [SerializeField] bool _useAnimation = default;

        public PageAnimationState AnimationState { get; private set; }
        public bool Active { get; private set; }
        public PageType Type { get => _type; set => _type = value; }
        public bool UseAnimation => _useAnimation;

        void OnEnable()
        {
            _animator = GetComponent<Animator>();
            if(!_animator) return;

            _animator.SetBool("on", false);
            _animator.enabled = UseAnimation;
        }
        
        public void Animate(bool transitionOn)
        {
            if(UseAnimation && _animator != null)
            {
                _animator.SetBool("on", transitionOn);
                StopCoroutine(AwaitAnimation(transitionOn));
                StartCoroutine(AwaitAnimation(transitionOn));
                return;
            }
            SetActive(transitionOn);
        }

        IEnumerator AwaitAnimation(bool transitionOn)
        {
            AnimationState = transitionOn ? PageAnimationState.On : PageAnimationState.Off;

            while(!_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationState.ToString()))
            {
                yield return null;
            }

            while(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || _animator.IsInTransition(0))
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