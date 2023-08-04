﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbsorbAbility : MonoBehaviour
{
    public GameObject ButtonAbsorbUI;
    public GameObject TimerUI;
    public TextMeshProUGUI AbsorptionTimerUI;

    [SerializeField] private KeyCode _absorbKey = KeyCode.B;
    [SerializeField] private float _timeToAbsorb = 5f;

    private bool _isAbsorption = false;
    private bool _canAbsorbed = false;
    private float _absorptionTimer = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canAbsorbed = true;
            ButtonAbsorbUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canAbsorbed = false;
            ButtonAbsorbUI.SetActive(false);
            TimerUI.SetActive(false);
        }
    }

    private void Start()
    {
        ButtonAbsorbUI.SetActive(false);
        TimerUI.SetActive(false);
    }

    private void Update()
    {
        

        if (_isAbsorption)
        {
            ButtonAbsorbUI.SetActive(false);
            TimerUI.SetActive(true);
            _absorptionTimer += Time.deltaTime;
            AbsorptionTimerUI.text = Mathf.Clamp(_timeToAbsorb - _absorptionTimer, 0f, _timeToAbsorb).ToString("F1");
            if (_absorptionTimer >= _timeToAbsorb)
            {

                Debug.Log("Способность поглащена");
                _absorptionTimer = 0f;
                _isAbsorption = false;
                ButtonAbsorbUI.SetActive(false);
                TimerUI.SetActive(false);
            }
        }

        if (Input.GetKeyDown(_absorbKey) && _canAbsorbed)
        {
            _absorptionTimer = 0f;
            _isAbsorption = true;
        }

        if (Input.GetKeyUp(_absorbKey) || !_canAbsorbed)
        {
            _absorptionTimer = 0f;
            _isAbsorption = false;
        }
    }
}