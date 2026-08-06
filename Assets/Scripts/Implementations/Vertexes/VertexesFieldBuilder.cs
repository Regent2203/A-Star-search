using EasyField.ObjectsStorages;
using EasyField.SaveSystem.Dto;
using UnityEngine;

namespace EasyField.Implementations.Vertexes
{
    public class VertexesFieldBuilder //: IFieldBuilder
    {
        private readonly VertexesNodesBuilder _nodesBuilder;
        private readonly VertexesLinksBuilder _linksBuilder;

        private readonly DictTypeStorage<VertexData, int> _nodeDatas;


        public VertexesFieldBuilder(VertexesNodesBuilder nodesBuilder, VertexesLinksBuilder linksBuilder,
            DictTypeStorage<VertexData, int> nodeDatas)
        {
            _nodesBuilder = nodesBuilder;
            _linksBuilder = linksBuilder;

            _nodeDatas = nodeDatas;
        }

        //temp
        public void TestPopulate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var pos = new Vector3(UnityEngine.Random.value * 40 - 20, UnityEngine.Random.value * 40 - 20, 0);
                _nodesBuilder.CreateItem(pos);
            }
        }

        public void CreateNode(Vector2 pos)
        {
            _nodesBuilder.CreateItem(pos);
        }

        public void DeleteNode(VertexView view)
        {
            _nodesBuilder.DeleteItem(view.Id);
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
                _linksBuilder.TryCreateLink(from, to);
            }
        }

        public void ClearAll()
        {
            _nodesBuilder.ClearAll();
            _linksBuilder.ClearAll();
        }
    }
}