using TMPro;
using UnityEngine;
using Zenject;

public class EnergyView : MonoBehaviour
{
    [Inject] private EnergyService _energyService;

    [SerializeField] private TMP_Text _energyText;
    
    private void Start()
    {
        _energyService.OnEnergyChanged += SetView;
        SetView();
    }

    private void OnDestroy()
    {
        _energyService.OnEnergyChanged -= SetView;
    }

    private void SetView()
    {
        _energyText.text = _energyService.Energy.ToString();
    }
}