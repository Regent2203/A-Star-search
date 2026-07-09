using System;
using ThisProject.SaveSystem.Dto;

namespace ThisProject.Implementations.Vertexes
{
    [Serializable]
    public class VertexDataDto : NodeDataDto<int>
    {
        public VertexDataDto(VertexData nodeData)
        {
            Id = nodeData.Id;
            NodePosition = nodeData.NodePosition;
        }
    }
}