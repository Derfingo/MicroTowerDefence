namespace MicroTowerDefence
{
    public class TowerInteractionViewModel : IDispose
    {
        private readonly ITowerInteraction _towerInteraction;
        private readonly ITowerInteractionView _towerInterctionView;
        private readonly ISelection _selection;
        private readonly IInputActions _input;

        public TowerInteractionViewModel(ITowerInteraction towerInteraction,
                                         ITowerInteractionView towerInterctionView,
                                         ISelection selection,
                                         IInputActions input)
        {
            _input = input;
            _selection = selection;
            _towerInteraction = towerInteraction;
            _towerInterctionView = towerInterctionView;

            _towerInteraction.OnTowerCostEvent += OnTowerInteract;

            _towerInterctionView.OnEnterButtonEvent += OnEnterButton;
            _towerInterctionView.OnExitButtonEvent += OnExitButton;

            _towerInterctionView.OnSellTowerEvent += OnSellTower;
            _towerInterctionView.OnUpgradeTowerEvent += OnUpgradeTower;
        }

        private void OnTowerInteract(uint upgradeCost, uint sellCost)
        {
            _towerInterctionView.SetCosts(upgradeCost, sellCost);
        }

        private void OnSellTower()
        {
            _selection.OnSellTower();
        }

        private void OnUpgradeTower()
        {
            _selection.OnUpgradeTower();
        }

        private void OnEnterButton()
        {
            _input.SetUIInput();
        }

        private void OnExitButton()
        {
            _input.SetPlayerInput();
        }

        public void Dispose()
        {
            _towerInteraction.OnTowerCostEvent -= OnTowerInteract;
            _towerInterctionView.OnEnterButtonEvent -= OnEnterButton;
            _towerInterctionView.OnExitButtonEvent -= OnExitButton;
            _towerInterctionView.OnSellTowerEvent -= OnSellTower;
            _towerInterctionView.OnUpgradeTowerEvent -= OnUpgradeTower;
        }
    }
}
