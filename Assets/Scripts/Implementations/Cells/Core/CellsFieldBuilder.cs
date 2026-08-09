using EasyField.Fields;
using EasyField.ObjectsStorages;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellsFieldBuilder
    {
        private readonly GridField _field;
        private readonly GridTypeStorage<CellData> _nodeDatas;
        private readonly GridTypeStorage<CellView> _nodeViews;        
        private readonly CellDataFactory _nodeDatasFactory;
        private readonly CellViewFactory _nodeViewsFactory;


        public CellsFieldBuilder(GridField field, GridTypeStorage<CellData> nodeDatas, GridTypeStorage<CellView> nodeViews,
            CellDataFactory nodeDatasFactory, CellViewFactory nodeViewsFactory)
        {
            _field = field;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;            
            _nodeDatasFactory = nodeDatasFactory;
            _nodeViewsFactory = nodeViewsFactory;
        }

        public void PopulateField(Vector2Int size, CellType cellType)
        {
            _nodeDatas.Init(size);
            _nodeViews.Init(size);
            _field.SetSize(size);

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var id = new Vector2Int(x, y);

                    var localX = x - (size.x / 2f);
                    var localY = y - (size.y / 2f);
                    var localPos = new Vector3(localX * _field.Grid.cellSize.x, localY * _field.Grid.cellSize.y, 0);

                    var nodePos = id;
                    var nodeData = _nodeDatasFactory.CreateItem(id, nodePos, cellType);

                    var viewPos = _field.Grid.transform.TransformPoint(localPos);
                    var nodeView = _nodeViewsFactory.CreateItem(id, _field.ScaleFactor);
                    nodeView.Move(viewPos);


                    _nodeDatas.AddItem(id, nodeData);
                    _nodeViews.AddItem(id, nodeView);
                }
            }
        }
    }
}