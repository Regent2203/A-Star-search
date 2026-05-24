using ThisProject.PathDrawers;
using System.Collections.Generic;

namespace ThisProject.Implementations.Cells
{
    public class CellsPathDrawer : IPathDrawer<CellView>
    {
        private IReadOnlyList<CellView> _path;

        public void SetPath(IReadOnlyList<CellView> path)
        {
            _path = path;
        }

        public void ShowPath(bool show)
        {
            if (_path == null)
                return;

            //we ignore start node and finish node, since they have their own graphics
            int from = 1;
            int to = _path.Count - 1;

            for (int i = from; i < to; i++)
            {
                _path[i].ShowPathMarker(show);
            }
        }
    }
}
