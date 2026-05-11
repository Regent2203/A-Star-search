using Core.Nodes;
using Core.Views;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Core.Fields.Grids
{
    public class GridField<T, V> : GridFieldBase<T>
        where T : class, INode<Vector2Int>
        where V : class, IView
    {
        protected V _viewPrefab;
        protected Vector2 _scaleFactor;

        protected V[,] _views;


        [Inject]
        public void Construct(V cellViewPrefab)
        {
            _viewPrefab = cellViewPrefab;
        }

        protected override void Init()
        {
            base.Init();

            _scaleFactor = _grid.cellSize / _viewPrefab.GetSize();
        }

        public void SetData(T[,] nodes, V[,] views)
        {
            _nodes = nodes;
            _views = views;
        }

        public V GetViewById(Vector2Int id)
        {
            if (_views.IsWithinBounds(id.x, id.y))
                return _views[id.x, id.y];

            return null;
        }

        public V GetViewForNode(T node) => GetViewById(node.Id);

        public IReadOnlyList<V> GetViewsForNodes(IList<T> nodePath) => nodePath.Select(GetViewForNode).ToList();
    }
}