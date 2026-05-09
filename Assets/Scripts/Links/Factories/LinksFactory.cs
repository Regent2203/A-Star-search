using Core.Nodes;

namespace Core.Links.Factories
{
    public class LinksFactory<T, TId> : ILinksFactory<T, TId> where T : class, INode<T, TId>
    {
        public ILink<T, TId> CreateLink(T from, T to, float cost)
        {
            return new Link<T, TId>(from, to, cost);
        }
    }
}