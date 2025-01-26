using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LinearEffectView : AbstractEffectView
{
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private Vector2 _linearVectorMax;
    [SerializeField] private Vector2 _linearVectorMin;
    [SerializeField] private Vector2 _linearVectorException;
    [SerializeField] private float _duration;
    private TweenerCore<Vector3, Vector3, VectorOptions> _linearTween;
    
    public override void Show(string EffectText)
    {
        _valueText.text = "+" + EffectText;

        var randomX = Random.Range(_linearVectorMin.x, _linearVectorMax.x);
        var randomY = Random.Range(_linearVectorMin.y, _linearVectorMax.y);
        var globalRandomPosition = transform.TransformPoint(new Vector3(randomX, randomY, 0f));
        
        _linearTween = transform.DOMove(globalRandomPosition, _duration);
        _linearTween.onComplete += Hide; 
        _linearTween.Play();
    }

    protected override void Hide()
    {
        _linearTween.onComplete -= Hide;
        DestroyImmediate(gameObject);
    }
}