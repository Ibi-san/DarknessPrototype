using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset _asset;

    private void Awake()
    {
        _asset.Enable();
    }

    private void OnDestroy()
    {
        _asset.Disable();
    }

}
