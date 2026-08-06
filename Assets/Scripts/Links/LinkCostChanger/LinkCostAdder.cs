using System;
using UnityEngine;

namespace EasyField.Links.LinkCostChangers
{
    public class LinkCostAdder<TLinkData> : ILinkCostChanger<TLinkData>
        where TLinkData : ILinkData
    {
        public event Action<TLinkData, float> LinkCostChanged;


        public bool SetLinkCost(TLinkData linkData, float value)
        {
            if (linkData == null)
                return false;

            if (Mathf.Approximately(value, 0f))
                return false;

            linkData.SetCost(linkData.Cost + value);
            LinkCostChanged?.Invoke(linkData, linkData.Cost);

            return true;
        }
    }
}