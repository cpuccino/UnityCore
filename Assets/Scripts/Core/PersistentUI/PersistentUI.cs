using System.Collections;
using UnityEngine;

namespace UnityCore.PersistentUI
{
    // Whether the persistent ui is transitioning on, off or neutral
    // Must match the animation state
    public enum PersistentUIAnimationState
    {
        None, On, Off
    }

    public class PersistentUI : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private PersistentUIType _type = default;
        [SerializeField] private bool _useAnimation = default;

        public PersistentUIAnimationState AnimationState { get; private set; }
        public bool Active { get; private set; }
        public PersistentUIType Type { get => _type; set => _type = value; }
        public bool UseAnimation => _useAnimation;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            if (!_animator) return;

            _animator.SetBool("on", false);
            _animator.enabled = UseAnimation;
        }

        public void Animate(bool transitionOn)
        {
            if (UseAnimation && _animator != null)
            {
                _animator.SetBool("on", transitionOn);
                StopCoroutine(AwaitAnimation(transitionOn));
                StartCoroutine(AwaitAnimation(transitionOn));
                return;
            }
            SetActive(transitionOn);
        }

        private IEnumerator AwaitAnimation(bool transitionOn)
        {
            AnimationState = transitionOn ? PersistentUIAnimationState.On : PersistentUIAnimationState.Off;

            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationState.ToString()))
            {
                yield return null;
            }

            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || _animator.IsInTransition(0))
            {
                yield return null;
            }

            AnimationState = PersistentUIAnimationState.None;
            SetActive(transitionOn);
        }

        private void SetActive(bool transitionOn)
        {
            Active = transitionOn;
            if (!transitionOn)
            {
                gameObject.SetActive(false);
            }
        }
    }
}