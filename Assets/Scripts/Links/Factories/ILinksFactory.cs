using Core.Nodes;

namespace Core.Links.Factories
{
    public interface ILinksFactory<T, TId> where T : class, INode<T, TId>
    {
        public ILink<T, TId> CreateLink(T from, T to, float cost);
    }
}