using UnityEngine;

namespace MicroTowerDefence
{
    public class CursorViewModel : IViewModel<Vector3>
    {
        public ReactiveProperty<Vector3> Property { get; } = new ();

        private readonly IGrid _grid;
        private readonly ICursorView _cursorView;

        public CursorViewModel(IGrid grid, ICursorView cursorView)
        {
            _grid = grid;
            _cursorView = cursorView;
            _grid.OnUpdateCursorEvent += OnUpdateCursor;
        }

        private void OnUpdateCursor(Vector3 position, bool isVisable)
        {
            _cursorView.UpdateCursor(position, isVisable);
        }

        ~CursorViewModel()
        {
            _grid.OnUpdateCursorEvent -= OnUpdateCursor;
        }
    }
}
