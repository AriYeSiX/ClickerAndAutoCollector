using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ValueListener : MonoBehaviour
{
    [Inject] private List<IValueSender> _valueSenders = new List<IValueSender>();
    
    public event Action<int, CollectType> OnRegisterIncrement;
    public event Action<int> OnRegisterDecrement; //Можно в дальнейшем использовать для введения IValueGetter и механики внутриигрового магазина
    
    [Inject]
    private void Construct(List<IValueSender> valueSenders)
    {
        _valueSenders = valueSenders;
    }
    
    public void Awake()
    {
        foreach (var valueSender in _valueSenders)
        {
            valueSender.OnSend += OnSendValue;
        }
    }

    public void OnDestroy()
    {
        foreach (var valueSender in _valueSenders)
        {
            valueSender.OnSend -= OnSendValue;
        }
    }

    private void OnSendValue(ValueSignal signal)
    {
        OnRegisterIncrement?.Invoke(signal.Amount, signal.CollectType);
    }
}