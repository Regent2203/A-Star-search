using Core.PathDrawers;
using System.Collections.Generic;

namespace Core.Implementations.Cells
{
    /// <summary>
    /// Works with class CellView, draws nodePath by calling method "ShowPathMarker" on cells
    /// </summary>
    public class CellsPathDrawer : IPathDrawer
    {
        private IList<CellView> _path;

        public void SetPath(IList<CellView> path)
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
