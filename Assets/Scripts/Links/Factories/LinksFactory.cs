using Core.Nodes;

namespace Core.Links.Factories
{
    public class LinksFactory<T> : ILinksFactory<T> where T : class, INode
    {
        public ILink<T> CreateLink(T from, T to, float cost)
        {
            return new Link<T>(from, to, cost);
        }
    }
}