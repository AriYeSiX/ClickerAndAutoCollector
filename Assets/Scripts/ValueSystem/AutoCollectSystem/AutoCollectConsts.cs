using UnityEngine;

[CreateAssetMenu(fileName = "AutoCollectConsts", menuName = "Clicker/Create new AutoCollectConsts")]
public class AutoCollectConsts : ScriptableObject
{
    [SerializeField] private int _value = 100;
    public int Value => _value;
    [SerializeField] private int _seconds = 5;
    public int Seconds => _seconds;
}