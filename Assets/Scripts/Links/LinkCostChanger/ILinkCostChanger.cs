using System;

namespace EasyField.Links.LinkCostChangers
{
    public interface ILinkCostChanger<TLinkData>
        where TLinkData : ILinkData
    {
        public bool ChangeLinkCost(TLinkData linkData, float value);

        public event Action<TLinkData, float> LinkCostChanged;
    }
}