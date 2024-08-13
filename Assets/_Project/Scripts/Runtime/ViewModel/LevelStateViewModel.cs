using MicroTowerDefence;
using System;

public class LevelStateViewModel : IDisposable
{
    private readonly ILevelState _levelState;
    private readonly IStateView _stateView;

    public LevelStateViewModel(ILevelState levelState, IStateView stateView)
    {
        _levelState = levelState;
        _stateView = stateView;

        _levelState.OnWinEvent += _stateView.ShowWinMenu;
        _levelState.OnPauseEvent += _stateView.ShowPauseMenu;
        _levelState.OnDefeatEvent += _stateView.ShowDefeatMenu;

        _stateView.OnContinueEvent += () => _levelState.OnPause(true);
        _stateView.OnRestartEvent += _levelState.OnRestart;
        _stateView.OnMainMenuEvent += _levelState.OnMainMenu;
        _stateView.OnNextLevelEvent += _levelState.OnNextLevel;
    }

    public void Dispose()
    {
        _levelState.OnWinEvent -= _stateView.ShowWinMenu;
        _levelState.OnPauseEvent -= _stateView.ShowPauseMenu;
        _levelState.OnDefeatEvent -= _stateView.ShowDefeatMenu;

        _stateView.OnContinueEvent -= () => _levelState.OnPause(true);
        _stateView.OnRestartEvent -= _levelState.OnRestart;
        _stateView.OnMainMenuEvent -= _levelState.OnMainMenu;
        _stateView.OnNextLevelEvent -= _levelState.OnNextLevel;
        UnityEngine.Debug.Log($"Dispose: {GetType().Name}");
    }
}
