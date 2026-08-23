using EasyField.Fields;
using EasyField.Fields.FieldBuilders;
using EasyField.PathSetters;
using UnityEngine;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesFieldBuilder : IFieldBuilder<VertexesFieldSaveDto>
    {
        private readonly Vector2 _nodeViewOffset;

        private readonly SpatialField _field;
        private readonly PathSetter<VertexData> _pathSetter;
        private readonly VertexesNodesCreator _nodesCreator;
        private readonly VertexesLinksCreator _linksCreator;
        private readonly VertexDataStorage _nodeDatas;


        public VertexesFieldBuilder(SpatialField field, PathSetter<VertexData> pathSetter,
            VertexesNodesCreator nodesCreator, VertexesLinksCreator linksCreator,
            VertexDataStorage nodeDatas, VertexView nodeView)
        {
            _field = field;
            _pathSetter = pathSetter;
            _nodesCreator = nodesCreator;
            _linksCreator = linksCreator;
            _nodeDatas = nodeDatas;

            _nodeViewOffset = nodeView.GetSize() / 2;
        }

        public void CreateNode(Vector2 pos)
        {
            pos = pos.Clamp(_field.Box.bounds, _nodeViewOffset);

            _nodesCreator.CreateItem(pos);
        }

        public void DeleteNode(int id)
        {
            var nodeData = _nodeDatas.GetItem(id);
            if (_pathSetter.StartNode == nodeData)
                _pathSetter.UpdateStartNode(null);            
            if (_pathSetter.FinishNode == nodeData)
                _pathSetter.UpdateFinishNode(null);            

            _linksCreator.DeleteLinksFromNode(id);
            _linksCreator.DeleteLinksToNode(id);

            _nodesCreator.DeleteItem(id);
        }

        public bool TryCreateLink(VertexData from, VertexData to)
        {
            return _linksCreator.TryCreateLinkItem(from, to);
        }

        public bool TryDeleteLink(VertexData from, VertexData to)
        {
            return _linksCreator.TryDeleteLinkItem(from.Id, to.Id);
        }

        public void BuildFromDto(VertexesFieldSaveDto data)
        {
            CreateNewField((Vector2)data.FieldSize);

            foreach (var item in data.Nodes)
            {
                var id = item.Id;
                var pos = (Vector2)item.NodePosition;
                _nodesCreator.CreateItem(id, pos);
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
            CreateNewField(new Vector2(sizeX, sizeY));
        }

        private void CreateNewField(Vector2 fieldSize)
        {
            ClearAll();
            _field.SetSize(fieldSize);
        }

        public void ClearAll()
        {
            _pathSetter.UpdateStartNode(null);
            _pathSetter.UpdateFinishNode(null);

            _nodesCreator.ClearAll();
            _linksCreator.ClearAll();
        }
    }
}