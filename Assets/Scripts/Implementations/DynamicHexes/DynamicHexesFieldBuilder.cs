using EasyField.Fields;
using EasyField.Fields.FieldBuilders;
using EasyField.Implementations.Cells;
using EasyField.Implementations.Cells.DynamicCells;
using EasyField.PathSetters;
using UnityEngine;

namespace EasyField.Implementations.Hexes.DynamicHexes
{
    public class DynamicHexesFieldBuilder : IFieldBuilder<DynamicCellsFieldSaveDto>
    {
        private readonly HexOrientationType _hexOrientationType;
        private readonly HexOffsetType _hexOffsetType;

        private readonly CellsConfig _config;
        private readonly HexGridField _field;
        private readonly PathSetter<CellData> _pathSetter;
        private readonly CellsNodesCreator _nodesCreator;
        private readonly DynamicCellsLinksCreator _linksCreator;
        private readonly CellDataStorage _nodeDatas;
        private readonly CellViewStorage _nodeViews;


        public DynamicHexesFieldBuilder(CellsConfig config, HexGridField field, PathSetter<CellData> pathSetter,
            CellsNodesCreator nodesCreator, DynamicCellsLinksCreator linksCreator,
            CellDataStorage nodeDatas, CellViewStorage nodeViews)
        {
            _config = config;
            _field = field;
            _pathSetter = pathSetter;
            _nodesCreator = nodesCreator;
            _linksCreator = linksCreator;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;

            _hexOrientationType = _field.GetHexOrientationType();
            _hexOffsetType = _field.GetHexOffsetType();
        }

        public bool TryCreateNode(Vector2 pos)
        {
            var index = (Vector2Int)_field.Grid.WorldToCell(pos);
            var viewPos = _field.Grid.CellToWorld((Vector3Int)index);

            if (!_nodeDatas.HasItem(index))
            {
                _nodesCreator.CreateItem(index, index, viewPos, _config.DefaultCellType);
                return true;
            }

            return false;
        }

        public bool TryDeleteNode(Vector2Int id)
        {
            if (_nodeDatas.TryGetItem(id, out var nodeData))
            {
                if (_pathSetter.StartNode == nodeData)
                    _pathSetter.UpdateStartNode(null);
                if (_pathSetter.FinishNode == nodeData)
                    _pathSetter.UpdateFinishNode(null);

                _linksCreator.DeleteLinksFromNode(id);
                _linksCreator.DeleteLinksToNode(id);

                _nodesCreator.DeleteItem(id);
                return true;
            }

            return false;
        }

        public bool TryCreateLink(CellData from, CellData to, float? cost = null)
        {
            return _linksCreator.TryCreateLinkItem(from, to, cost);
        }

        public bool TryDeleteLink(CellData from, CellData to)
        {
            return _linksCreator.TryDeleteLinkItem(from.Id, to.Id);
        }

        public void BuildFromDto(DynamicCellsFieldSaveDto data)
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

            foreach (var item in data.Links)
            {
                var from = _nodeDatas.GetItem(item.From);
                var to = _nodeDatas.GetItem(item.To);
                _linksCreator.TryCreateLinkItem(from, to, item.Cost);
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
            _linksCreator.ClearAll();
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
            if (_hexOrientationType == HexOrientationType.FlatTopped)
            {
                (y, x) = (x, y);
            }

            if (_hexOffsetType == HexOffsetType.Even)
            {
                if ((y & 1) == 1)
                    x -= 1;
            }

            var localPos = _field.Grid.CellToLocal(new Vector3Int(x, y, 0));
            var viewPos = _field.Grid.transform.TransformPoint(localPos);

            return viewPos;
        }
    }
}