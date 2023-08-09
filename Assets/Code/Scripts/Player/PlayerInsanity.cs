using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Code.Scripts.Logic;
using UnityEngine;

public class PlayerInsanity : MonoBehaviour, IStat
{
    public event Action<float> OnInsanityChanged;
    private const float MaxInsanityValue = 100f;
    private const float StartInsanityValue = 10f;

    
    private int _insanityChangeValue = 2;
    [Header("Insanity Value")]
    [SerializeField] private int _standardChangeValue = 2;
    [SerializeField] private int _voidChangeValue = 4;
    [SerializeField] private int _lightChangeValue = -10;
    
    
    private int _insanityChangeCooldown = 60;
    [Header("Insanity Cooldown")]
    [SerializeField]private int _standardCooldown = 60;
    [SerializeField]private int _lightCooldown = 10;

    public EnvironmentType EnvironmentType
    {
        get => _environmentType;
        set
        {
            _environmentType = value;
            switch (_environmentType)
            {
                case EnvironmentType.Void:
                    _insanityChangeValue = _voidChangeValue;
                    _insanityChangeCooldown = _standardCooldown;
                    break;
                case EnvironmentType.Light:
                    _insanityChangeValue = _lightChangeValue;
                    _insanityChangeCooldown = _lightCooldown;
                    break;
                case EnvironmentType.Default:
                    _insanityChangeValue = _standardChangeValue;
                    _insanityChangeCooldown = _standardCooldown;
                    break;
            }
        }
    }

    private EnvironmentType _environmentType;

    public float InsanityValue
    {
        get => _insanityValue;
        set
        {
            _insanityValue = Mathf.Clamp(value, 0f, MaxInsanityValue);
            OnInsanityChanged?.Invoke(_insanityValue);
        }
    }

    [SerializeField, Space (10)] private float _insanityValue;

    private CancellationTokenSource _cancellationTokenSource;
    
    private void Awake()
    {
        EnvironmentType = EnvironmentType.Default;
        Initialize();
    }

    private void Initialize()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        InsanityValue = StartInsanityValue;
        InsanityRise(_cancellationTokenSource.Token);
    }
    
    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }

    private async void InsanityRise(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(_insanityChangeCooldown * 1000, token);

                while (_insanityValue < MaxInsanityValue)
                {
                    InsanityValue += _insanityChangeValue;
            
                    if (MaxInsanityValue - _insanityValue == 0)
                    {
                        print("GameOver");
                        return;
                    }

                    await Task.Delay(_insanityChangeCooldown * 1000, token);
                }
            }
            token.ThrowIfCancellationRequested();
        }
        catch (OperationCanceledException oc)
        {
            print(oc);
        }
    }

    [ContextMenu("SetVoid")] public void SetVoid()
    {
        EnvironmentType = EnvironmentType.Void;
        RestartInsanityRise();
    }

    [ContextMenu("SetLight")] public void SetLight()
    {
        EnvironmentType = EnvironmentType.Light;
        RestartInsanityRise();
    }

    [ContextMenu("SetDefault")] public void SetDefault()
    {
        EnvironmentType = EnvironmentType.Default;
        RestartInsanityRise();
    }

    private void RestartInsanityRise()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        InsanityRise(_cancellationTokenSource.Token);
    }
}