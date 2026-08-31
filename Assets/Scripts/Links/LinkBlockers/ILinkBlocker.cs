using System;

namespace EasyField.Links.LinkBlockers
{
    public interface ILinkBlocker<TLinkData>
        where TLinkData : ILinkData
    {
        public bool TryBlockLink(TLinkData linkData, bool block);

        public event Action<TLinkData, bool> LinkBlocked;
    }
}