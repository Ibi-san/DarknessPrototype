using System;
using UnityEngine;

public class EnvironmentZone : MonoBehaviour
{
    [SerializeField] private EnvironmentType _environmentType;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerInsanity playerInsanity))
        {
            switch (_environmentType)
            {
                case EnvironmentType.Light:
                    playerInsanity.SetLight();
                    break;
                case EnvironmentType.Void:
                    playerInsanity.SetVoid();
                    break;
                case EnvironmentType.Default:
                    playerInsanity.SetDefault();
                    break;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInsanity playerInsanity))
            playerInsanity.SetDefault();
    }
}


