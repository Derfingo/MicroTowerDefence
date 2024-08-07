using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class TowerPreview : IPreview
    {
        public event Action<TowerBase> OnBuildEvent;

        private readonly IInputActions _input;
        private readonly TowerFactory _factory;
        private readonly Coins _coins;

        private TowerBase _preview;

        public TowerPreview(IInputActions input, TowerFactory factory, Coins coins)
        {
            _coins = coins;
            _input = input;
            _factory = factory;
        }

        public void OnBuilPreview()
        {
            OnBuildEvent?.Invoke(_preview);
            _preview.Undo();
            _preview = null;
            _input.SetPlayerInput();
        }

        public void OnShowPreview(TowerType type, Vector3 position)
        {
            _input.SetUIInput();
            _preview = _factory.Get(type);
            var cost = _factory.GetCostToBuild(_preview.TowerType);

            if (_coins.Check(cost))
            {
                _preview.Position = position;
                _preview.ShowTargetRadius(true);
                _preview.Show();
            }
            else
            {
                _preview.Reclaim();
                // show not enough coins
            }
        }

        public void OnHidePreview()
        {
            _input.SetPlayerInput();

            if (_preview != null)
            {
                _preview.Reclaim();
                _preview = null;
            }
        }
    }
}
