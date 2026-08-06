using EasyField.Links.Implementations;

namespace EasyField.Links.Factories
{
    public class LinkViewFactory<TId>
    {
        private readonly LinkViewPool<TId> _linkViewsPool;

        public LinkViewFactory(LinkViewPool<TId> linkViewsPool)
        {
            _linkViewsPool = linkViewsPool;
        }

        public LinkView<TId> CreateItem(TId fromId, TId toId, float cost, PlacementType placementType)
        {
            var linkView = _linkViewsPool.Spawn(fromId, toId, cost, placementType);

            return linkView;
        }

        public void DeleteItem(LinkView<TId> item)
        {
            _linkViewsPool.Despawn(item);
        }
    }
}