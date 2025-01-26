using System;

public interface IValueSender
{
    public event Action<ValueSignal> OnSend;
    
    void SendValue();
}