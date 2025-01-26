using System;
using System.Globalization;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EnergyService : IInitializable, IDisposable
{
    private const string FORMAT = "yyyy-MM-dd HH:mm:ss"; // Формат для сохранения
    private const string ENERGY_KEY = "Energy";
    private const string EXIT_DATE_KEY = "EXIT_DATE";
    
    [Inject] private EnergyConsts _energyConsts;

    private int _energy
    {
        get => PlayerPrefs.GetInt(ENERGY_KEY, 0);
        set
        {
            PlayerPrefs.SetInt(ENERGY_KEY, value);
            PlayerPrefs.Save();
        }
    }
    public int Energy => _energy;
    
    public event Action OnEnergyChanged;
    
    private CancellationTokenSource _cancellationTokenSource;
    public void Initialize()
    {
        if (!PlayerPrefs.HasKey(ENERGY_KEY))
        {
            _energy = _energyConsts.MaxEnergy;
        }
        else
        {
            var cultureInfo = CultureInfo.InvariantCulture;
            var previousTime = DateTime.ParseExact(PlayerPrefs.GetString(EXIT_DATE_KEY), FORMAT, cultureInfo);
            // В дальнейшем можно заменить на проверку наличия интернета и получать текущие даты из сети, чтобы обойти накрутку времени,
            // без интернета не начислять офлайн энергию
            var now = DateTime.Now;

            var difference = now - previousTime;
            var offlineSeconds = Convert.ToInt32(difference.TotalSeconds);
            var offlineEnergy = offlineSeconds / _energyConsts.Seconds;
            
            AddEnergy(offlineEnergy);
        }
        
        OnEnergyChanged?.Invoke();
        
        StartEnergyTask();
    }
    
    public void Dispose()
    {
        var now = DateTime.Now;
        var nowString = now.ToString(FORMAT);
        PlayerPrefs.SetString(EXIT_DATE_KEY, nowString);
        PlayerPrefs.Save();
    }

    private async void StartEnergyTask()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
        await EnergyTask(_cancellationTokenSource.Token);
    }

    private async UniTask EnergyTask(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_energyConsts.Seconds), cancellationToken: token);
        if (_cancellationTokenSource.IsCancellationRequested)
        {
            return;
        }
        AddEnergy(_energyConsts.EnergyForTick);
        StartEnergyTask();
    }

    private void AddEnergy(int amount)
    {
        _energy += amount;
        if (_energy > _energyConsts.MaxEnergy)
        {
            _energy = _energyConsts.MaxEnergy;
        }
        OnEnergyChanged?.Invoke();
    }

    public void RemoveEnergy(int amount)
    {
        _energy -= amount;
        if (_energy < 0)
        {
            _energy = 0;
        }
        
        OnEnergyChanged?.Invoke();
    }

    public bool HaveEnergy(int amount)
    {
        return _energy >= amount;
    }
}