using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIInsanity : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _insanityValueText;
    [SerializeField] private RectTransform _insanityIndicator;
    [SerializeField] private PlayerInsanity _playerInsanity;

    private float _scaleChangeTime = 1f;
    private Vector3 _defaultScale = new Vector3(2, 2, 1);
    private float _scaleMultiplier = 0.02f;
    
    private void OnEnable() => _playerInsanity.OnInsanityChanged += UpdateInsanityView;

    private void UpdateInsanityView(float currentInsanity)
    {
        UpdateText(currentInsanity);
        UpdateIndicator(currentInsanity);
    }

    private void OnDisable() => _playerInsanity.OnInsanityChanged -= UpdateInsanityView;

    private void UpdateText(float value) => _insanityValueText.text = value.ToString();

    private void UpdateIndicator(float value)
    {
        var scaleFactor = value * _scaleMultiplier;
        var newScale = new Vector3(_defaultScale.x - scaleFactor, _defaultScale.y - scaleFactor, _defaultScale.z);
        _insanityIndicator.DOScale(newScale, _scaleChangeTime);
    }
}
