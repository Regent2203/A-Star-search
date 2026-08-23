using EasyField.Fields;
using EasyField.Fields.FieldBuilders;
using EasyField.Implementations.Cells;
using EasyField.PathSetters;
using UnityEngine;

namespace EasyField.Implementations.Hexes
{
    public class HexesFieldBuilder : IFieldBuilder<CellsFieldSaveDto>
    {
        private readonly HexOrientationType _hexOrientationType;
        private readonly int _offsetModulo; //0 or 1

        private readonly CellsConfig _config;
        private readonly HexGridField _field;
        private readonly PathSetter<CellData> _pathSetter;
        private readonly CellsNodesCreator _nodesCreator;
        private readonly CellDataStorage _nodeDatas;
        private readonly CellViewStorage _nodeViews;        


        public HexesFieldBuilder(CellsConfig config, HexGridField field, PathSetter<CellData> pathSetter, CellsNodesCreator nodesCreator,
            CellDataStorage nodeDatas, CellViewStorage nodeViews)
        {
            _config = config;
            _field = field;
            _pathSetter = pathSetter;
            _nodesCreator = nodesCreator;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
            
            _hexOrientationType = _field.GetHexOrientationType();
            _offsetModulo = (int)_field.GetHexOffsetType();
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

                var viewPos = IndexToViewPos(nodePos.x, nodePos.y);

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

        private Vector3 IndexToViewPos(float x, float y)
        {
            Vector3 localPos = Vector3.zero;

            switch (_hexOrientationType)
            {
                case HexOrientationType.PointyTopped:
                    if (y % 2 != _offsetModulo)
                        x += 0.5f; //horizontal offset (right) for odd/even rows
                    localPos = new Vector3(x * _field.Grid.cellSize.x, y * 0.75f * _field.Grid.cellSize.y, 0);
                    break;

                case HexOrientationType.FlatTopped:
                    if (x % 2 != _offsetModulo)
                        y += 0.5f; //vertical offset (up) for odd/even columns
                    localPos = new Vector3(x * 0.75f * _field.Grid.cellSize.y, y * _field.Grid.cellSize.x, 0);
                    break;
            }            

            var viewPos = _field.Grid.transform.TransformPoint(localPos);

            return viewPos;
        }
    }
}
