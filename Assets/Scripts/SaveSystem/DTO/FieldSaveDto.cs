using System.Collections.Generic;
using System;

namespace ThisProject.SaveSystem.Dto
{
    [Serializable]
    public struct FieldSaveDto<D, TId>
        where D : NodeDataDto<TId>
    {
        public List<D> Nodes;
        //public List<T> Links { get; set; } //todo
    }
}