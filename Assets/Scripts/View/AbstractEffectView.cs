using UnityEngine;

public abstract class AbstractEffectView : MonoBehaviour
{
    public abstract void Show(string EffectText);
    
    protected abstract void Hide();
}