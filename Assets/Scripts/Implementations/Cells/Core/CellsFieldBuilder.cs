using EasyField.Fields;
using EasyField.Fields.FieldBuilders;
using EasyField.PathSetters;
using UnityEngine;

namespace EasyField.Implementations.Cells
{
    public class CellsFieldBuilder : IFieldBuilder<CellsFieldSaveDto>
    {
        private readonly CellsConfig _config;
        private readonly RectGridField _field;
        private readonly PathSetter<CellData> _pathSetter;
        private readonly CellsNodesCreator _nodesCreator;
        private readonly CellDataStorage _nodeDatas;
        private readonly CellViewStorage _nodeViews;


        public CellsFieldBuilder(CellsConfig config, RectGridField field, PathSetter<CellData> pathSetter, CellsNodesCreator nodesCreator,
            CellDataStorage nodeDatas, CellViewStorage nodeViews)
        {
            _config = config;
            _field = field;
            _pathSetter = pathSetter;
            _nodesCreator = nodesCreator;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
        }

        public void BuildFromDto(CellsFieldSaveDto data)
        {
            var size = (Vector2Int)data.FieldSize;

            PrepareNewField(size);

            foreach (var item in data.Nodes)
            {
                var id = item.Id;
                var nodePos = (Vector2)item.NodePosition;
                var cellType = _config.CellTypes[item.CellTypeId];

                var viewPos = IndexToViewPos((int)nodePos.x, (int)nodePos.y);

                _nodesCreator.CreateItem(id, nodePos, viewPos, cellType);
            }
        }

        public void CreateNewField(int sizeX, int sizeY)
        {
            var size = new Vector2Int(sizeX, sizeY);
            
            PrepareNewField(size);

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var id = new Vector2Int(x, y);                    
                    var nodePos = id;
                    var viewPos = IndexToViewPos(x, y);
                    
                    _nodesCreator.CreateItem(id, nodePos, viewPos, _config.DefaultCellType);
                }
            }
        }

        public void ClearAll()
        {
            _pathSetter.UpdateStartNode(null);
            _pathSetter.UpdateFinishNode(null);

            _nodesCreator.ClearAll();
        }

        private void PrepareNewField(Vector2Int size)
        {
            ClearAll();

            _nodeDatas.Init(size);
            _nodeViews.Init(size);
            _field.SetSize(size);
        }

        private Vector3 IndexToViewPos(int x, int y)
        {
            var localPos = _field.Grid.CellToLocal(new Vector3Int(x, y, 0));
            var viewPos = _field.Grid.transform.TransformPoint(localPos);

            return viewPos;
        }
    }
}