using EasyField.Fields;
using EasyField.Fields.FieldBuilders;
using EasyField.Implementations.Links;
using EasyField.ObjectsStorages;
using EasyField.PathSetters;
using EasyField.SaveSystem.Dto;
using UnityEngine;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesFieldBuilder : IFieldBuilder<VertexDataDto, LinkDataDto<int>>
    {
        private readonly SpatialField _field;
        private readonly PathSetter<VertexData> _pathSetter;
        private readonly VertexesNodesBuilder _nodesBuilder;
        private readonly VertexesLinksBuilder _linksBuilder;
        private readonly DictTypeStorage<VertexData, int> _nodeDatas;


        public VertexesFieldBuilder(SpatialField field, PathSetter<VertexData> pathSetter,
            VertexesNodesBuilder nodesBuilder, VertexesLinksBuilder linksBuilder,
            DictTypeStorage<VertexData, int> nodeDatas)
        {
            _field = field;
            _pathSetter = pathSetter;
            _nodesBuilder = nodesBuilder;
            _linksBuilder = linksBuilder;
            _nodeDatas = nodeDatas;
        }

        public void CreateNode(Vector2 pos)
        {
            _nodesBuilder.CreateItem(pos);
        }

        public void DeleteNode(int id)
        {
            var nodeData = _nodeDatas.GetItem(id);
            if (_pathSetter.StartNode == nodeData)
                _pathSetter.UpdateStartNode(null);            
            if (_pathSetter.FinishNode == nodeData)
                _pathSetter.UpdateFinishNode(null);            

            _linksBuilder.DeleteLinksFromNode(id);
            _linksBuilder.DeleteLinksToNode(id);

            _nodesBuilder.DeleteItem(id);
        }

        public bool TryCreateLink(VertexData from, VertexData to)
        {
            return _linksBuilder.TryCreateLinkItem(from, to);
        }

        public bool TryDeleteLink(VertexData from, VertexData to)
        {
            return _linksBuilder.TryDeleteLinkItem(from.Id, to.Id);
        }

        public void BuildFromDto(FieldSaveDto<VertexDataDto, LinkDataDto<int>> data)
        {
            ClearAll();

            foreach (var item in data.Nodes)
            {
                var id = item.Id;
                var pos = (Vector2)item.NodePosition;
                _nodesBuilder.CreateItem(id, pos);
            }

            foreach (var item in data.Links)
            {
                var from = _nodeDatas.GetItem(item.From);
                var to = _nodeDatas.GetItem(item.To);
                _linksBuilder.TryCreateLinkItem(from, to);
            }
        }

        public void CreateNewField(int sizeX, int sizeY)
        {
            ClearAll();
            _field.SetSize(new Vector2(sizeX, sizeY));
        }

        public void ClearAll()
        {
            _pathSetter.UpdateStartNode(null);
            _pathSetter.UpdateFinishNode(null);

            _nodesBuilder.ClearAll();
            _linksBuilder.ClearAll();
        }
    }
}