using System;
using System.Collections;
using UnityEngine;

namespace Ukiyo.Unity.Core.Page
{
    public enum PageState
    {
        None, On, Off
    }
    
    public class Page: MonoBehaviour
    {
        Animator animator;

        public PageState State { get; private set; }

        [SerializeField] PageType type;
        public PageType Type { get { return type; } set { type = value; } }

        [SerializeField] bool useAnimation;
        public bool UseAnimation { get { return useAnimation; } }

        private bool active;

        public bool Active 
        { 
            get 
            { 
                return active; 
            } 
            set 
            {
                if(!value)
                {
                    active = false;
                    gameObject.SetActive(false);
                }
                else
                {
                    active = true;
                }
            }
        }

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
            Active = transitionOn;
        }

        IEnumerator AwaitAnimation(bool transitionOn)
        {
            State = transitionOn ? PageState.On : PageState.Off;
            
            string state = Enum.GetName(typeof(PageState), State);

            while(!animator.GetCurrentAnimatorStateInfo(0).IsName(state))
            {
                yield return null;
            }

            while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || animator.IsInTransition(0))
            {
                yield return null;
            }

            State = PageState.None;
            Active = transitionOn;
        }
    }
}