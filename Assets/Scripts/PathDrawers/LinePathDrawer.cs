using Core.Implementations.Cells;
using Core.Views;
using System.Collections.Generic;
using UnityEngine;

namespace Core.PathDrawers
{
    public class LinePathDrawer : IPathDrawer
    {
        private readonly LineRenderer _lineRenderer;
        private IList<CellView> _path;

        public LinePathDrawer(LineRenderer lineRenderer)
        {
            _lineRenderer = lineRenderer;
            _lineRenderer.positionCount = 0;
        }

        public void SetPath(IList<CellView> path)
        {
            _path = path;
        }

        public void ShowPath(bool show)
        {
            if (_path == null || _path.Count < 2)
            {
                _lineRenderer.positionCount = 0;
                return;
            }
            if (!show)
            {
                _lineRenderer.positionCount = 0;
                return;
            }

            _lineRenderer.positionCount = _path.Count;

            for (int i = 0; i < _path.Count; i++)
            {
                _lineRenderer.SetPosition(i, _path[i].GetCenterCoords());
            }
        }
    }
}