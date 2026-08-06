using EasyField.SaveSystem.Mappers;

namespace EasyField.Implementations.Vertexes
{
    public class VertexDataMapper : INodeMapper<VertexData, VertexDataDto, int>
    {
        public VertexDataDto ToDto(VertexData nodeData)
        {
            return new VertexDataDto(nodeData.Id, nodeData.NodePosition);
        }
    }
}