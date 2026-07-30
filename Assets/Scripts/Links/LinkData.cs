using System;

namespace ThisProject.Links
{
    public class LinkData<TId> : ILinkData<TId>
    //where TId : IEquatable<TId>
    {
        public TId From { get; }
        public TId To { get; }
        
        public float Cost { get; private set; }

        public LinkData(TId fromId, TId toId, float cost)
        {
            From = fromId;
            To = toId;
            Cost = cost;
        }

        public void ChangeCost(float value)
        {
            Cost += value;
        }
    }
}