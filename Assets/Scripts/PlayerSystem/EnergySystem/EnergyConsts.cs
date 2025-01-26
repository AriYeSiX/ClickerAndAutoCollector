using UnityEngine;

[CreateAssetMenu(fileName = "EnergyConsts", menuName = "Clicker/Create new EnergyConsts")]
public class EnergyConsts : ScriptableObject
{
    [SerializeField] private int _seconds;
    public int Seconds => _seconds;
    [SerializeField] private int _energyForTick;
    public int EnergyForTick => _energyForTick;
    [SerializeField] private int _maxEnergy;
    public int MaxEnergy => _maxEnergy;
}