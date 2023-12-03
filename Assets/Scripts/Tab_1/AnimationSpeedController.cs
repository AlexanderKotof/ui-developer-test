using UnityEngine;
using UnityEngine.UI;

public class AnimationSpeedController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _defaultSpeed;

    private void Awake()
    {
        InitSlider();
    }
    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetAnimationSpeed);
    }
    private void OnDisable()
    {
        _slider.onValueChanged.RemoveAllListeners();
    }

    private void InitSlider()
    {
        _slider.maxValue = _maxSpeed;
        _slider.minValue = _minSpeed;
        _slider.value = _defaultSpeed;
    }
    private void SetAnimationSpeed(float value)
    {
        _animator.speed = value;
    }
}
