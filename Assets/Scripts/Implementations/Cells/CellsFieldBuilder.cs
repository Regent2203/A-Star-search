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
        private readonly CellNodePool _nodesPool;
        private readonly CellViewPool _viewsPool;


        public CellsFieldBuilder(GridField field, GridTypeStorage<CellNode> nodes, GridTypeStorage<CellView> views,
            CellNodePool nodesPool, CellViewPool viewsPool)
        {
            _field = field;
            _nodes = nodes;
            _views = views;            
            _nodesPool = nodesPool;
            _viewsPool = viewsPool;
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
                    var node = _nodesPool.Spawn(index, nodePos, cellType);

                    var viewPos = _field.Grid.transform.TransformPoint(localPos);
                    var view = _viewsPool.Spawn(index, _field.ScaleFactor);
                    view.Move(viewPos);


                    _nodes.AddItem(index, node);
                    _views.AddItem(index, view);
                }
            }
        }
    }
}