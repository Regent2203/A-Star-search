using System.Collections.Generic;

namespace EasyField.Links.Providers
{
    public interface ILinksProvider<TLinkData, TId>
        where TLinkData : ILinkData<TId>
    {
        public IEnumerable<TLinkData> GetLinksFromNode(TId id);
        public IEnumerable<TLinkData> GetLinksToNode(TId id);
    }
}