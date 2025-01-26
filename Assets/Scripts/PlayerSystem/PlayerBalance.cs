using System;
using UnityEngine;
using Zenject;

[Serializable]
public class PlayerBalance : IInitializable, IDisposable
{
    public const string PLAYER_BALANCE_KEY = "PlayerBalance";
    [Inject] private ValueListener _valueListener;
    
    [SerializeField] private int _balance;
    public int Balance => _balance;
    [SerializeField] private int _autoCollectValue;
    public int AutoCollectValue => _autoCollectValue;
    [SerializeField] private int _tapValue;
    public int TapValue => _tapValue;
    
    public event Action OnUpdateBalance;

    public void Initialize()
    {
        _valueListener.OnRegisterIncrement += AddBalance;
        
        if (!PlayerPrefs.HasKey(PLAYER_BALANCE_KEY)) 
            return;
        
        var savedBalance = JsonUtility.FromJson<PlayerBalance>(PlayerPrefs.GetString(PLAYER_BALANCE_KEY));
        _balance = savedBalance.Balance;
        _autoCollectValue = savedBalance.AutoCollectValue;
        _tapValue = savedBalance.TapValue;
        OnUpdateBalance?.Invoke();
    }

    public void Dispose()
    {
        _valueListener.OnRegisterIncrement -= AddBalance;
        PlayerPrefs.SetString(PLAYER_BALANCE_KEY, JsonUtility.ToJson(this));
        PlayerPrefs.Save();
    }
    
    private void AddBalance(int amount, CollectType collectType)
    {
        switch (collectType)
        {
            default:
            case CollectType.Auto:
                _autoCollectValue += amount;
                break;
            case CollectType.Tap:
                _tapValue += amount;

                
                break;
        }
        
        _balance += amount;
        OnUpdateBalance?.Invoke();
    }

    private void RemoveBalance(int amount, CollectType collectType)
    {
        switch (collectType)
        {
            default:
            case CollectType.Auto:
                _autoCollectValue -= amount;
                break;
            case CollectType.Tap:
                _tapValue -= amount;
                break;
        }
        
        _balance -= amount;
        OnUpdateBalance?.Invoke();
    }


}

public enum CollectType
{
    Auto = 0,
    Tap = 1
}