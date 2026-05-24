using ThisProject.Views;
using System.Collections.Generic;
using UnityEngine;

namespace ThisProject.PathDrawers
{
    public class LinePathDrawer : IPathDrawer<IView>
    {
        private readonly LineRenderer _lineRenderer;
        private IReadOnlyList<IView> _path;

        public LinePathDrawer(LineRenderer lineRenderer)
        {
            _lineRenderer = lineRenderer;
            _lineRenderer.positionCount = 0;
        }

        public void SetPath(IReadOnlyList<IView> path)
        {
            _path = path;

            if (_path == null)
            {
                _lineRenderer.positionCount = 0;
            }
            else
            {
                _lineRenderer.positionCount = _path.Count;

                for (int i = 0; i < _path.Count; i++)
                {
                    _lineRenderer.SetPosition(i, _path[i].GetCenterCoords() - _lineRenderer.transform.position);
                }
            }
        }

        public void ShowPath(bool show)
        {
            _lineRenderer.enabled = show;
        }
    }
}