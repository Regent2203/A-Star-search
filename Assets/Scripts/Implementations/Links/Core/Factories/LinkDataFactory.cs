using EasyField.Links.Implementations;

namespace EasyField.Links.Factories
{
    public class LinkDataFactory<TId>
    {
        protected readonly LinkDataPool<TId> _linkDatasPool;


        public LinkDataFactory(LinkDataPool<TId> linkDatasPool)
        {
            _linkDatasPool = linkDatasPool;
        }

        public LinkData<TId> CreateItem(TId fromId, TId toId, float cost)
        {            
            var linkData = _linkDatasPool.Spawn(fromId, toId, cost);

            return linkData;
        }

        public void DeleteItem(LinkData<TId> item)
        {
            _linkDatasPool.Despawn(item);
        }
    }
}