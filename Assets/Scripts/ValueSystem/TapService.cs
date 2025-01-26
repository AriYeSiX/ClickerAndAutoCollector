using System;
using Zenject;

public class TapService : IValueSender
{
    [Inject] private SoftValueConsts _softValueConsts;
    [Inject] private AutoCollectConsts _autoCollectConsts;
    [Inject] private EnergyService _energyService;

    public event Action<ValueSignal> OnSend;

    public void SendValue()
    {
        if (!_energyService.HaveEnergy(_softValueConsts.EnergyPrice))
        {
            return;
        }
        
        _energyService.RemoveEnergy(_softValueConsts.EnergyPrice);
        
        var amount =
            (_softValueConsts.Value * _softValueConsts.TapModifier +
             (_softValueConsts.CollectorModifier.AutoCollectPercent * _autoCollectConsts.Value)/100) /
            _softValueConsts.CollectorModifier.Del;
        OnSend?.Invoke(new ValueSignal(amount,CollectType.Tap));
    }
}