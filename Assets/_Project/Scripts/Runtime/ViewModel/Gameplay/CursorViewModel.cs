using UnityEngine;

namespace MicroTowerDefence
{
    public class CursorViewModel : IViewModel<Vector3>
    {
        public ReactiveProperty<Vector3> Property { get; } = new ();

        private readonly IGrid _grid;
        private readonly ISelection _selection;
        private readonly ICursorView _cursorView;

        private bool _isBuilding;

        public CursorViewModel(IGrid grid, ISelection selection, ICursorView cursorView)
        {
            _grid = grid;
            _selection = selection;
            _cursorView = cursorView;

            _grid.OnGridEvent += OnUpdateCursor;
            _selection.OnBuildingEvent += IsBuilding;
        }

        private void IsBuilding(bool isBuilding) => _isBuilding = isBuilding;

        private void OnUpdateCursor(Vector3 position, bool isVisable)
        {
            if (_isBuilding) return;

            _cursorView.UpdateCursor(position, isVisable);
        }

        ~CursorViewModel()
        {
            _grid.OnGridEvent -= OnUpdateCursor;
            _selection.OnBuildingEvent -= IsBuilding;
        }
    }
}
