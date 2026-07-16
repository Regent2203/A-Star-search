using Newtonsoft.Json;
using System;
using ThisProject.SaveSystem.Dto;

namespace ThisProject.Implementations.Vertexes
{
    [Serializable]
    public class VertexDataDto : NodeDataDto<int>
    {
        public VertexDataDto(int id, Vector2Dto nodePosition)
        {
            Id = id;
            NodePosition = nodePosition;
        }
    }
}