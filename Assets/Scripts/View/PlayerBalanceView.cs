using TMPro;
using UnityEngine;
using Zenject;

public class PlayerBalanceView : MonoBehaviour
{
    [Inject] private PlayerBalance _playerBalance;
    [Inject] private AutoCollectService _autoCollectService;

    [SerializeField] private TMP_Text _balanceText;
    [SerializeField] private TMP_Text _tapBalanceText;
    [SerializeField] private TMP_Text _autoCollectBalanceText;
    [SerializeField] private TMP_Text _alltimeBalanceText;
    private void Awake()
    {
        _playerBalance.OnUpdateBalance += SetView;
        SetView();
    }

    private void OnDestroy()
    {
        _playerBalance.OnUpdateBalance -= SetView;
    }

    private void SetView()
    {
        _balanceText.text = _playerBalance.Balance.ToString();
        _tapBalanceText.text = _playerBalance.TapValue.ToString();
        _autoCollectBalanceText.text = _playerBalance.AutoCollectValue.ToString();
        _alltimeBalanceText.text = (_playerBalance.TapValue+_playerBalance.AutoCollectValue).ToString();
    }
}