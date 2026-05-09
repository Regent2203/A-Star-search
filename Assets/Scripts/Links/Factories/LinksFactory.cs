using Core.Nodes;

namespace Core.Links.Factories
{
    /// <summary>
    /// Used to create links for cells in grid during search algorithm work (not beforehand)
    /// </summary>
    public class LinksFactory<T, TId> : ILinksFactory<T, TId> where T : class, INode<T, TId>
    {
        public ILink<T, TId> CreateLink(T from, T to, float cost)
        {
            return new Link<T, TId>(from, to, cost);
        }
    }
}