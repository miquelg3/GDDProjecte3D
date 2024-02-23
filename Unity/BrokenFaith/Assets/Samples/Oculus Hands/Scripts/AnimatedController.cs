using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimatedController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private InputActionReference gripInputActionReference, triggerInputActionReference;
    private Animator _handAnimator;
    private float _gripValue;
    private float _triggerValue;
    #endregion
    
    void Start()
    {
        _handAnimator = GetComponent<Animator>();
    }
    
    void Update()
    {
        AnimateGrip();
        AnimateTriger();
    }
    private void AnimateGrip()
    {
        _gripValue = gripInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Grip", _gripValue);
    }
    private void AnimateTriger()
    {
        _triggerValue = triggerInputActionReference.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);
    }
}
