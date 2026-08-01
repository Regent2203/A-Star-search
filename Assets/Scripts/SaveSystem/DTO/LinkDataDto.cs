using System;

namespace ThisProject.SaveSystem.Dto
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