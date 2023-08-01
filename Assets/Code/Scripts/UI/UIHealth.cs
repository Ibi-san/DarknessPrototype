using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHealth : MonoBehaviour
{
    [SerializeField] private UnitHealth _unitHealth;
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeartSprite;
    [SerializeField] private Sprite _scratchedHeartSprite;
    [SerializeField] private Sprite _crackedHeartSprite;
    [SerializeField] private Sprite _emptyHeartSprite;
    private int _strengthHeart = 3;

    private void OnEnable()
    {
        _unitHealth.OnHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        _unitHealth.OnHealthChanged -= UpdateHealthUI;
    }
    private void Start()
    {
        UpdateHealthUI(_unitHealth.Health);
    }

    private void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i * _strengthHeart < currentHealth)
            {
                _hearts[i].enabled = true;
                _hearts[i].sprite = GetHeartSprite(currentHealth - i * _strengthHeart);
            }
            else
            {
                _hearts[i].sprite = GetHeartSprite(0);
            }
        }
    }

    private Sprite GetHeartSprite(int remainingHealth)
    {

        switch (remainingHealth)
        {
            case 3: return _fullHeartSprite;
            case 2: return _scratchedHeartSprite;
            case 1: return _crackedHeartSprite;
            case 0: return _emptyHeartSprite;
            default: return _fullHeartSprite;


        }
    }
}
