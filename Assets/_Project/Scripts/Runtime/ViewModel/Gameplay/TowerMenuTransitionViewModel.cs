using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class TowerMenuTransitionViewModel : IDisposable
    {
        private readonly IPrepare _prepare;
        private readonly ISelection _selection;
        private readonly ITowerBuildingView _towerBuildingView;
        private readonly ITowerInteractionView _towerInteractionView;

        public TowerMenuTransitionViewModel(IPrepare prepare,
                                            ISelection selection,
                                            ITowerBuildingView towerBuildingView,
                                            ITowerInteractionView towerInteractionView)
        {
            _prepare = prepare;
            _selection = selection;
            _towerBuildingView = towerBuildingView;
            _towerInteractionView = towerInteractionView;

            _prepare.OnPrepareEvent += () => SetBuildingMenu(true);
            _selection.OnBuildingEvent += SetBuildingMenu;
            _selection.OnInteractionEvent += SetInterationMenu;
        }

        private void SetBuildingMenu(bool isBuilding)
        {
            if (isBuilding)
            {
                _towerInteractionView.Hide();
                _towerBuildingView.Show();
            }
            else
            {
                _towerInteractionView.Hide();
                _towerBuildingView.Show();
            }
        }

        private void SetInterationMenu(bool isInteraction)
        {
            if (isInteraction)
            {
                _towerBuildingView.Hide();
                _towerInteractionView.Show();
            }
            else
            {
                _towerInteractionView.Hide();
                _towerBuildingView.Show();
            }
        }

        public void Dispose()
        {
            _prepare.OnPrepareEvent -= () => SetBuildingMenu(true);
            _selection.OnBuildingEvent -= SetBuildingMenu;
            _selection.OnInteractionEvent -= SetInterationMenu;
        }
    }
}
