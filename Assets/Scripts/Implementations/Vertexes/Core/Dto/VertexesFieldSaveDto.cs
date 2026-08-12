using EasyField.Implementations.Links;
using EasyField.SaveSystem.Dto;
using System;

namespace EasyField.Implementations.Vertexes
{
    [Serializable]
    public class VertexesFieldSaveDto : FieldSaveDto<VertexDataDto, LinkDataDto<int>>
    {
        public Vector2Dto FieldSize = new();
    }
}