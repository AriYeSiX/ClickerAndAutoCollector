using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EffectViewFactory : MonoBehaviour
{
    [Inject, SerializeField] private List<AbstractEffectView> _effectViews = new List<AbstractEffectView>();
    [Inject] private TapService _tapService;
    
    [SerializeField] private Transform _tapViewParent;
    
    private void Awake()
    {
        _tapService.OnSend += ShowTapEffect;
    }

    private void OnDestroy()
    {
        _tapService.OnSend -= ShowTapEffect;
    }

    private void ShowTapEffect(ValueSignal signal)
    {
        var effect = Instantiate(_effectViews[Random.Range(0, _effectViews.Count)], _tapViewParent);
        effect.Show(signal.Amount.ToString());
    }
}