using System;
using System.Collections.Generic;
using Zenject;

namespace MicroTowerDefence
{
    public class TowerBuildingView : ViewBase, ITowerBuildingView
    {
        public event Action<TowerType> BuildTowerEvent;
        public event Action<TowerType> EnterPreviewTowerEvent;
        public event Action<TowerType> ExitPreviewTowerEvent;

        private readonly List<BuildTowerButton> _buildTowerButtons = new ();

        [Inject]
        public void Initialize() 
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                _buildTowerButtons.Add(transform.GetChild(i).GetComponent<BuildTowerButton>());
            }

            _buildTowerButtons.ForEach(click => click.OnClickEvent += OnBuildTower);
            _buildTowerButtons.ForEach(enter => enter.OnPointerEnterEvent += OnEnterPreviewTower);
            _buildTowerButtons.ForEach(exit => exit.OnPointerExitEvent += OnExitPreviewTower);

            EnableButtons(false);
        }

        public void EnableButtons(bool isEnable)
        {
            foreach (var button in _buildTowerButtons)
            {
                button.enabled = isEnable;
            }
        }

        public void SetTowersCost(Dictionary<TowerType, uint> costTowers)
        {
            foreach (var button in _buildTowerButtons)
            {
                if (costTowers.TryGetValue(button.TowerType, out var cost))
                {
                    button.SetCost(cost);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        private void OnBuildTower(TowerType type)
        {
            BuildTowerEvent?.Invoke(type);
        }

        private void OnEnterPreviewTower(TowerType type)
        {
            EnterPreviewTowerEvent?.Invoke(type);
        }

        private void OnExitPreviewTower(TowerType type)
        {
            ExitPreviewTowerEvent?.Invoke(type);
        }

        private void OnDestroy()
        {
            _buildTowerButtons.ForEach(click => click.OnClickEvent -= OnBuildTower);
            _buildTowerButtons.ForEach(enter => enter.OnPointerEnterEvent -= OnEnterPreviewTower);
            _buildTowerButtons.ForEach(exit => exit.OnPointerExitEvent -= OnExitPreviewTower);
        }
    }
}