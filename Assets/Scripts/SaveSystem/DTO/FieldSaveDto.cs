using System;
using System.Collections.Generic;

namespace ThisProject.SaveSystem.Dto
{
    public interface IFieldSaveDto<TNode, TLink>
    {
        public List<TNode> Nodes { get; set; }
        //public List<TLink> Links { get; set; }
    }

    [Serializable]
    public class FieldSaveDto<TNodeDataDto, TLinkDataDto> : IFieldSaveDto<TNodeDataDto, TLinkDataDto>
    {
        public List<TNodeDataDto> Nodes { get; set; } = new();
        //public List<TLinkDataDto> Links { get; set; } = new();
    }

    public class VertexFieldSaveDto : FieldSaveDto<Implementations.Vertexes.VertexDataDto, LinkDataDto>
    {
    }


    //todo
    [Serializable]
    public class LinkDataDto
    {
        //id from, id to, weight
    }
}