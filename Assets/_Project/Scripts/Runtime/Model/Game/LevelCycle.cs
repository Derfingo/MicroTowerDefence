using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelCycle : MonoBehaviour, IPause
    {
        private IReset[] _resets;
        private IUpdate[] _updates;
        private ILateUpdate[] _lateUpdates;

        private bool _isPause;

        [Inject]
        public void Initialize(IReset[] resets,
                               IUpdate[] updates,
                               ILateUpdate[] lateUpdates)
        {
            
            _resets = resets;
            _updates = updates;
            _lateUpdates = lateUpdates;
        }

        private void Update()
        {
            if (_isPause)
            {
                return;
            }

            UpdateControllers();
            Physics.SyncTransforms();
        }

        private void LateUpdate()
        {
            if (_isPause)
            {
                return;
            }

            LateUpdateControllers();
        }

        private void UpdateControllers()
        {
            foreach (var update in _updates)
            {
                update.GameUpdate();
            }
        }

        private void LateUpdateControllers()
        {
            foreach (var lateUpdate in _lateUpdates)
            {
                lateUpdate.GameLateUpdate();
            }
        }

        public void ResetValues()
        {
            foreach (var reset in _resets)
            {
                reset.Reset();
            }
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}