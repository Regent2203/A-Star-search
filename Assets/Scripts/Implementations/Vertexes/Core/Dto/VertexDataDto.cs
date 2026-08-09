using EasyField.Nodes.Dto;
using System;

namespace EasyField.Implementations.Vertexes
{
    [Serializable]
    public class VertexDataDto : NodeDataDto<int>
    {
        public VertexDataDto(int id, Vector2Dto nodePosition) : base(id, nodePosition)
        {
        }
    }
}