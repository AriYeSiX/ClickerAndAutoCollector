using Zenject;

public class TapButton : AbstractButtonView
{
    [Inject] private TapService _tapService;
    
    protected override void OnClickEvent()
    {
        _tapService.SendValue();
    }
}