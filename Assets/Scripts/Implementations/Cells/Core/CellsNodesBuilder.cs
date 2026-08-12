using EasyField.Fields;
using EasyField.ObjectsStorages;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellsNodesBuilder
    {        
        private readonly GridField _field;
        private readonly GridTypeStorage<CellData> _nodeDatas;
        private readonly GridTypeStorage<CellView> _nodeViews;
        private readonly CellDataFactory _nodeDatasFactory;
        private readonly CellViewFactory _nodeViewsFactory;


        public CellsNodesBuilder(GridField field, GridTypeStorage<CellData> nodeDatas, GridTypeStorage<CellView> nodeViews,
            CellDataFactory nodeDatasFactory, CellViewFactory nodeViewsFactory) 
        {            
            _field = field;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
            _nodeDatasFactory = nodeDatasFactory;
            _nodeViewsFactory = nodeViewsFactory;
        }

        public void CreateItem(Vector2Int id, Vector2 nodePos, Vector2 viewPos, CellType cellType)
        {
            var nodeData = _nodeDatasFactory.CreateItem(id, nodePos, cellType);
            var nodeView = _nodeViewsFactory.CreateItem(id, _field.ScaleFactor);
            nodeView.Move(viewPos);
            nodeView.UpdateSprite(cellType.Sprite);

            _nodeDatas.AddItem(id, nodeData);
            _nodeViews.AddItem(id, nodeView);
        }

        public void DeleteItem(Vector2Int id)
        {
            var nodeData = _nodeDatas.GetItem(id);
            var nodeView = _nodeViews.GetItem(id);

            _nodeDatasFactory.DeleteItem(nodeData);
            _nodeViewsFactory.DeleteItem(nodeView);

            _nodeDatas.RemoveItem(id);
            _nodeViews.RemoveItem(id);
        }

        public void ClearAll()
        {
            foreach (var data in _nodeDatas.AllItems)
            {
                _nodeDatasFactory.DeleteItem(data);
            }
            _nodeDatas.ClearData();

            foreach (var view in _nodeViews.AllItems)
            {
                _nodeViewsFactory.DeleteItem(view);
            }
            _nodeViews.ClearData();
        }
    }
}