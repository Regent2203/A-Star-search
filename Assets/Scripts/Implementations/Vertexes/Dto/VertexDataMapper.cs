using ThisProject.SaveSystem.Mappers;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexDataMapper : IMapper<VertexData, VertexDataDto, int>
    {
        public VertexDataDto ToDto(VertexData nodeData)
        {
            return new VertexDataDto(nodeData.Id, nodeData.NodePosition);
        }
    }
}