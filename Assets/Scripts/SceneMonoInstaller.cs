using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneMonoInstaller : MonoInstaller
{
    [SerializeField] private SoftValueConsts _softValueConsts;
    [SerializeField] private AutoCollectConsts _autoCollectConsts;
    [SerializeField] private EnergyConsts _energyConsts;
    [SerializeField] private AbstractEffectView[] _effectViews;
    public override void InstallBindings()
    {
        Container.Bind<SoftValueConsts>().FromInstance(_softValueConsts).AsSingle();
        Container.Bind<AutoCollectConsts>().FromInstance(_autoCollectConsts).AsSingle();
        Container.Bind<EnergyConsts>().FromInstance(_energyConsts).AsSingle();
        Container.BindInterfacesAndSelfTo<EnergyService>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<TapService>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<AutoCollectService>().FromNew().AsSingle();
        Container.Bind<ValueListener>().FromComponentInHierarchy(true).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerBalance>().FromNew().AsSingle();
        Container.Bind<PlayerBalanceView>().FromComponentInHierarchy(true).AsSingle();
        Container.Bind<AbstractEffectView>().FromMethodMultiple(GetEffectViews);
        Container.Bind<EffectViewFactory>().FromComponentInHierarchy(true).AsSingle();
    }

    IEnumerable<AbstractEffectView> GetEffectViews(InjectContext context)
    {
        return _effectViews;
    }
}