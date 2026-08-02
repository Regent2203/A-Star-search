using ThisProject.Links.Implementations;

namespace ThisProject.Links.Factories
{
    public class LinkViewFactory<TId>
    {
        private readonly LinkViewPool<TId> _linkViewsPool;

        public LinkViewFactory(LinkViewPool<TId> linkViewsPool)
        {
            _linkViewsPool = linkViewsPool;
        }

        public LinkView<TId> CreateItem(TId fromId, TId toId, PlacementType placementType)
        {
            var linkView = _linkViewsPool.Spawn(fromId, toId, placementType);

            return linkView;
        }

        public void DeleteItem(LinkView<TId> item)
        {
            _linkViewsPool.Despawn(item);
        }
    }
}