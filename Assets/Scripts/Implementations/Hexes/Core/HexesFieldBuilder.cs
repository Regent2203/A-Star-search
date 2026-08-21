using EasyField.Fields;
using EasyField.Fields.FieldBuilders;
using EasyField.Implementations.Cells;
using EasyField.PathSetters;
using UnityEngine;

namespace EasyField.Implementations.Hexes
{
    public class HexesFieldBuilder : IFieldBuilder<CellsFieldSaveDto>
    {
        private readonly CellsConfig _config;
        private readonly HexGridField _field;
        private readonly PathSetter<CellData> _pathSetter;
        private readonly CellsNodesCreator _nodesCreator;
        private readonly CellDataStorage _nodeDatas;
        private readonly CellViewStorage _nodeViews;
        private readonly int _offsetModulo; //0 or 1


        public HexesFieldBuilder(CellsConfig config, HexGridField field, PathSetter<CellData> pathSetter, CellsNodesCreator nodesCreator,
            CellDataStorage nodeDatas, CellViewStorage nodeViews)
        {
            _config = config;
            _field = field;
            _pathSetter = pathSetter;
            _nodesCreator = nodesCreator;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
            _offsetModulo = (int)_field.GetHexOffsetType();
        }

        public void BuildFromDto(CellsFieldSaveDto data)
        {
            var size = (Vector2Int)data.FieldSize;

            ClearAll();
            PrepareNewField(size);

            foreach (var item in data.Nodes)
            {
                var id = item.Id;
                var nodePos = (Vector2)item.NodePosition;
                var cellType = _config.CellTypes[item.CellType];

                var viewPos = IndexToViewPos((int)nodePos.x, (int)nodePos.y, size);

                _nodesCreator.CreateItem(id, nodePos, viewPos, cellType);
            }
        }

        public void CreateNewField(int sizeX, int sizeY)
        {
            var size = new Vector2Int(sizeX, sizeY);

            ClearAll();
            PrepareNewField(size);

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var id = new Vector2Int(x, y);
                    var nodePos = id;
                    var viewPos = IndexToViewPos(x, y, size);

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
            _nodeDatas.Init(size);
            _nodeViews.Init(size);
            _field.SetSize(size);
        }

        private Vector3 IndexToViewPos(int x, int y, Vector2Int size)
        {
            var localX = x - (size.x - 1) / 2f;
            var localY = y - (size.y - 1) / 2f;
            
            if (y % 2 == _offsetModulo)
                localX += 0.5f; //horizontal offset for odd/even rows
            

            var localPos = new Vector3(localX * _field.Grid.cellSize.x, localY * 0.75f * _field.Grid.cellSize.y, 0);

            var viewPos = _field.Grid.transform.TransformPoint(localPos);

            return viewPos;
        }
    }
}
