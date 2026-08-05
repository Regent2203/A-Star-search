using System;

namespace ThisProject.Links.LinkCostChangers
{
    public class LinkCostChanger<TLinkData> : ILinkCostChanger<TLinkData>
        where TLinkData : ILinkData
    {
        public event Action<TLinkData, float> LinkCostChanged;


        public bool ChangeLinkCost(TLinkData linkData, float value)
        {
            if (linkData == null)
                return false;

            linkData.ChangeCost(value);
            LinkCostChanged?.Invoke(linkData, linkData.Cost);

            return true;
        }

        public bool SetLinkCost(TLinkData linkData, float value)
        {
            if (linkData == null)
                return false;

            linkData.SetCost(value);
            LinkCostChanged?.Invoke(linkData, linkData.Cost);

            return true;
        }
    }
}
