using System;
using UnityEngine;

namespace MicroTowerDefence
{
    public class CursorViewModel : IViewModel<Vector3>, IDisposable
    {
        public ReactiveProperty<Vector3> Property { get; } = new ();

        private readonly IGrid _grid;
        private readonly ISelection _selection;
        private readonly ICursorView _cursorView;

        private bool _isBuilding;
        private bool _isInteraction;

        public CursorViewModel(IGrid grid, ISelection selection, ICursorView cursorView)
        {
            _grid = grid;
            _selection = selection;
            _cursorView = cursorView;

            _grid.OnGridEvent += OnUpdateCursor;
            _selection.OnBuildingEvent += IsBuilding;
            _selection.OnInteractionEvent += IsInteraction;
        }

        private void IsBuilding(bool isBuilding) => _isBuilding = isBuilding;
        private void IsInteraction(bool isInteraction) => _isInteraction = isInteraction;

        private void OnUpdateCursor(Vector3 position, bool isVisable)
        {
            if (_isBuilding || _isInteraction) return;

            _cursorView.UpdateCursor(position, isVisable);
        }

        public void Dispose()
        {
            _grid.OnGridEvent -= OnUpdateCursor;
            _selection.OnBuildingEvent -= IsBuilding;
            _selection.OnInteractionEvent -= IsInteraction;
        }
    }
}
