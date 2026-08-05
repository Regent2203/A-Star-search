using System;

namespace ThisProject.Links.LinkCostChangers
{
    public interface ILinkCostChanger<TLinkData>
        where TLinkData : ILinkData
    {
        public bool SetLinkCost(TLinkData linkData, float value);

        public event Action<TLinkData, float> LinkCostChanged;
    }
}