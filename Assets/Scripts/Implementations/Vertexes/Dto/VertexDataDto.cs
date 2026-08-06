using System;
using EasyField.SaveSystem.Dto;

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