using UnityEngine;
using Zenject;

namespace MicroTowerDefence
{
    public class LevelCycle : MonoBehaviour, IPause
    {
        private IUpdate[] _updates;
        private ILateUpdate[] _lateUpdates;

        private bool _isPause;

        [Inject]
        public void Initialize(IUpdate[] updates, ILateUpdate[] lateUpdates)
        {
            _updates = updates;
            _lateUpdates = lateUpdates;
        }

        private void Update()
        {
            if (_isPause) return;

            UpdateControllers();
            Physics.SyncTransforms();
        }

        private void LateUpdate()
        {
            if (_isPause) return;

            LateUpdateControllers();
        }

        private void UpdateControllers()
        {
            if (_isPause) return;

            foreach (var update in _updates)
            {
                update.GameUpdate();
            }
        }

        private void LateUpdateControllers()
        {
            if (_isPause) return;

            foreach (var lateUpdate in _lateUpdates)
            {
                lateUpdate.GameLateUpdate();
            }
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}