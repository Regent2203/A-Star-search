using EasyField.Nodes;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EasyField.PathDrawers
{
    public static class LinePathDrawer
    {
        public const string LineRendererId = "PathLine";
    }

    public class LinePathDrawer<TNodeView> : IPathDrawer<TNodeView>
        where TNodeView : INodeView
    {
        private readonly LineRenderer _lineRenderer;
        private IReadOnlyList<TNodeView> _path;        


        public LinePathDrawer([Inject(Id = LinePathDrawer.LineRendererId)] LineRenderer lineRenderer)
        {
            _lineRenderer = lineRenderer;
            _lineRenderer.positionCount = 0;
        }

        public void SetPath(IReadOnlyList<TNodeView> path)
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