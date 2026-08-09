using System;
using System.Collections.Generic;

namespace EasyField.SaveSystem.Dto
{
    [Serializable]
    public class FieldSaveDto<TNodeDataDto, TLinkDataDto>
    {
        public List<TNodeDataDto> Nodes = new();
        public List<TLinkDataDto> Links = new();
    }

    [Serializable]
    public class FieldSaveDto<TNodeDataDto>
    {
        public List<TNodeDataDto> Nodes = new();
    }
}