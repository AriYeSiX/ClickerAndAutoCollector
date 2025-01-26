using System;

[Serializable]
public class ValueSignal
{
    public int Amount { get; private set; }
    public CollectType CollectType{ get; private set; }

    public ValueSignal(int amount, CollectType collectType)
    {
        Amount = amount;
        CollectType = collectType;
    }
}