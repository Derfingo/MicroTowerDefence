using MicroTowerDefence;

public class StateLevelPresenter
{
    private readonly ILevelState _levelState;

    private readonly IStateView _stateView;

    public StateLevelPresenter(ILevelState levelState, IStateView stateView)
    {
        _levelState = levelState;
        _stateView = stateView;

        _levelState.OnWinEvent += _stateView.ShowWinMenu;
        _levelState.OnPauseEvent += _stateView.ShowPauseMenu;
        _levelState.OnDefeatEvent += _stateView.ShowDefeatMenu;

        _stateView.OnContinueEvent += () => _levelState.OnPause(true);
        _stateView.OnRestartEvent += _levelState.OnRestart;
    }

    ~StateLevelPresenter()
    {
        _levelState.OnWinEvent -= _stateView.ShowWinMenu;
        _levelState.OnPauseEvent -= _stateView.ShowPauseMenu;
        _levelState.OnDefeatEvent -= _stateView.ShowDefeatMenu;

        _stateView.OnContinueEvent -= () => _levelState.OnPause(true);
    }
}
