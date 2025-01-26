using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoftValueConsts", menuName = "Clicker/Create new SoftValueConsts")]
public class SoftValueConsts : ScriptableObject
{
    [SerializeField] private int _value = 100;
    public int Value => _value;
    
    [SerializeField] private int _tapModifier = 1;
    public int TapModifier => _tapModifier;
    
    [SerializeField] private int _energyPrice = 1;
    public int EnergyPrice => _energyPrice;

    
    [SerializeField] private CollectorModifier _collectorModifier;
    public CollectorModifier CollectorModifier => _collectorModifier;
}

[Serializable]
public class CollectorModifier
{
    //Сумма доходов от заданного в настройках scriptable object значения в T времени
    //было не понятно из описания задания, что это в итоге за переменная,
    //поэтому заменил это на требование из доп. задачи
    [SerializeField, Range(0,100f)] private int _autoCollectPercent;
    public int AutoCollectPercent => _autoCollectPercent;

    [SerializeField] private int _del = 1;
    public int Del => _del;

}