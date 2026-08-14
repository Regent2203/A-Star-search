using System;
using UnityEngine;

namespace EasyField.Links.LinkCostChangers
{
    public class LinkCostSetter<TLinkData> : ILinkCostChanger<TLinkData>
        where TLinkData : ILinkData
    {
        public event Action<TLinkData, float> LinkCostChanged;


        public bool ChangeLinkCost(TLinkData linkData, float value)
        {
            if (linkData == null)
                return false;

            if (Mathf.Approximately(value, linkData.Cost))
                return false;

            linkData.SetCost(Mathf.Max(0,value));
            LinkCostChanged?.Invoke(linkData, linkData.Cost);

            return true;
        }
    }
}
