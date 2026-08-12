using EasyField.Fields;
using EasyField.Fields.FieldBuilders;
using EasyField.Implementations.Vertexes;
using EasyField.ObjectsStorages;
using EasyField.PathSetters;
using EasyField.SaveSystem.Dto;
using System.Drawing;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace EasyField.Implementations.Cells
{
    public class CellsFieldBuilder : IFieldBuilder<CellsFieldSaveDto>
    {
        private readonly CellsConfig _config;
        private readonly GridField _field;
        private readonly PathSetter<CellData> _pathSetter;
        private readonly CellsNodesBuilder _nodesBuilder;
        private readonly GridTypeStorage<CellData> _nodeDatas;
        private readonly GridTypeStorage<CellView> _nodeViews;


        public CellsFieldBuilder(CellsConfig config, GridField field, PathSetter<CellData> pathSetter, CellsNodesBuilder nodesBuilder,
            GridTypeStorage<CellData> nodeDatas, GridTypeStorage<CellView> nodeViews)
        {
            _config = config;
            _field = field;
            _pathSetter = pathSetter;
            _nodesBuilder = nodesBuilder;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
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
                var cellType = item.CellType;

                var localX = nodePos.x - (size.x / 2f);
                var localY = nodePos.y - (size.y / 2f);
                var localPos = new Vector3(localX * _field.Grid.cellSize.x, localY * _field.Grid.cellSize.y, 0);
                var viewPos = _field.Grid.transform.TransformPoint(localPos);

                _nodesBuilder.CreateItem(id, nodePos, viewPos, cellType);
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

                    var localX = x - (size.x / 2f);
                    var localY = y - (size.y / 2f);
                    var localPos = new Vector3(localX * _field.Grid.cellSize.x, localY * _field.Grid.cellSize.y, 0);

                    var nodePos = id;                    
                    var viewPos = _field.Grid.transform.TransformPoint(localPos);
                    
                    _nodesBuilder.CreateItem(id, nodePos, viewPos, _config.DefaultCellType);
                }
            }
        }

        public void ClearAll()
        {
            _pathSetter.UpdateStartNode(null);
            _pathSetter.UpdateFinishNode(null);

            _nodesBuilder.ClearAll();
        }

        private void PrepareNewField(Vector2Int size)
        {
            _nodeDatas.Init(size);
            _nodeViews.Init(size);
            _field.SetSize(size);
        }
    }
}