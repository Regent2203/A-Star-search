using EasyField.SaveSystem.Dto.Mappers;

namespace EasyField.Implementations.Vertexes
{
    public class VertexDataMapper : INodeDataMapper<VertexData, VertexDataDto, int>
    {
        public VertexDataDto ToDto(VertexData nodeData)
        {
            return new VertexDataDto(nodeData.Id, nodeData.NodePosition);
        }
    }
}