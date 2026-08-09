using System;

namespace EasyField.Implementations.Links
{
    [Serializable]
    public class LinkDataDto<TId>
    {
        public TId From;
        public TId To;
        public float Cost;

        public LinkDataDto(TId from, TId to, float cost)
        {            
            From = from;
            To = to;
            Cost = cost;
        }
    }
}