using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Animator))]
public class AnimateHandController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private InputActionReference gripInputActionReference, triggerInputActionReference;
    private Animator _handAnimator;
    private float _gripValue;
    private float _triggerValue;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _handAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
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