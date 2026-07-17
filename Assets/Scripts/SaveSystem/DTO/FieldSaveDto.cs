using System;
using System.Collections.Generic;

namespace ThisProject.SaveSystem.Dto
{
    [Serializable]
    public class FieldSaveDto<TNodeDataDto, TLinkDataDto>
    {
        public List<TNodeDataDto> Nodes = new();
        public List<TLinkDataDto> Links = new();
    }
}