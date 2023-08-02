using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerInsanity playerInsanity))
        {
            playerInsanity.SetLight();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInsanity playerInsanity))
            playerInsanity.SetDefault();
    }
}
