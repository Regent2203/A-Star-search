using System;

namespace EasyField.Links.LinkBlockers
{
    public class LinkBlocker<TLinkData> : ILinkBlocker<TLinkData>
        where TLinkData : ILinkData
    {
        public event Action<TLinkData, bool> LinkBlocked;

        public bool TryBlockLink(TLinkData linkData, bool block)
        {
            if (linkData == null)
                return false;

            if (linkData.TrySetBlocked(block))
            {
                LinkBlocked?.Invoke(linkData, linkData.IsBlocked);
                return true;
            }

            return false;
        }
    }
}