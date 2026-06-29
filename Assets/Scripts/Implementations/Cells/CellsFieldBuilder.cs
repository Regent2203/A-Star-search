using ThisProject.Fields;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    public class CellsFieldBuilder
    {
        private readonly GridField _field;
        private readonly GridTypeStorage<CellNode> _nodes;
        private readonly GridTypeStorage<CellView> _views;        
        private readonly CellNodeFactory _nodesFactory;
        private readonly CellViewFactory _viewsFactory;


        public CellsFieldBuilder(GridField field, GridTypeStorage<CellNode> nodes, GridTypeStorage<CellView> views,
            CellNodeFactory nodeFactory, CellViewFactory viewFactory)
        {
            _field = field;
            _nodes = nodes;
            _views = views;            
            _nodesFactory = nodeFactory;
            _viewsFactory = viewFactory;
        }

        public void PopulateField(Vector2Int size, CellType cellType)
        {
            _nodes.Init(size);
            _views.Init(size);
            _field.SetSize(size);

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var index = new Vector2Int(x, y);

                    var localX = x - (size.x / 2f);
                    var localY = y - (size.y / 2f);
                    var localPos = new Vector3(localX * _field.Grid.cellSize.x, localY * _field.Grid.cellSize.y, 0);

                    var nodePos = index;
                    var node = _nodesFactory.Create(index, nodePos, cellType);

                    var viewPos = _field.Grid.transform.TransformPoint(localPos);
                    var view = _viewsFactory.Create(index, viewPos, _field.ScaleFactor, _field.Container);

                    _nodes.TryAddItem(index, node);
                    _views.TryAddItem(index, view);
                }
            }
        }
    }
}