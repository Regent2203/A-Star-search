using Core.Nodes;
using Core.ObjectsStorages;
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

        protected GridTypeStorage<V> _views;

        public GridTypeStorage<V> Views => _views;


        [Inject]
        public void Construct(GridTypeStorage<V> views, V cellViewPrefab)
        {
            _views = views;
            _viewPrefab = cellViewPrefab;
        }

        protected override void Init()
        {
            base.Init();

            _scaleFactor = _grid.cellSize / _viewPrefab.GetSize();
        }

        public void SetFieldData(T[,] nodes, V[,] views)
        {
            _nodes.SetData(nodes);
            _views.SetData(views);
        }

        public V GetViewById(Vector2Int id)
        {
            return _views.GetById(id);
        }

        public V GetViewForNode(T node) => GetViewById(node.Id);

        public IReadOnlyList<V> GetViewsForNodes(IList<T> nodePath) => nodePath.Select(GetViewForNode).ToList();
    }
}