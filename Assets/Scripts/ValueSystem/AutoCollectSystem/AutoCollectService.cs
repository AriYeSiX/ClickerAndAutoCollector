using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

public class AutoCollectService : IValueSender, IInitializable, IDisposable
{
    [Inject] private AutoCollectConsts _autoCollectConsts;
    private CancellationTokenSource _cancellationTokenSource;
    public event Action<ValueSignal> OnSend;
    
    private async UniTask CollectTask(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_autoCollectConsts.Seconds),cancellationToken: token);
        if (!token.IsCancellationRequested)
        {
            SendValue();
        }
    }
    
    public void SendValue()
    {
        OnSend?.Invoke(new ValueSignal(_autoCollectConsts.Value, CollectType.Auto));
        Initialize();
    }

    public async void Initialize()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        await CollectTask(_cancellationTokenSource.Token);
    }

    public void Dispose()
    {
        _cancellationTokenSource?.Cancel();
    }
}