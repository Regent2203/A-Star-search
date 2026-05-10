using Core.Nodes;

namespace Core.Links.Factories
{
    public interface ILinksFactory<T> where T : class, INode
    {
        public ILink<T> CreateLink(T from, T to, float cost);
    }
}