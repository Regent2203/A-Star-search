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
    }
}