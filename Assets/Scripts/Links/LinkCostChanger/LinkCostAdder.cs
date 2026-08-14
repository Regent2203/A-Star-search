using System;
using UnityEngine;

namespace EasyField.Links.LinkCostChangers
{
    public class LinkCostAdder<TLinkData> : ILinkCostChanger<TLinkData>
        where TLinkData : ILinkData
    {
        public event Action<TLinkData, float> LinkCostChanged;


        public bool ChangeLinkCost(TLinkData linkData, float value)
        {
            if (linkData == null)
                return false;

            if (Mathf.Approximately(value, 0f))
                return false;

            linkData.SetCost(Mathf.Max(0, linkData.Cost + value));
            LinkCostChanged?.Invoke(linkData, linkData.Cost);

            return true;
        }
    }
}