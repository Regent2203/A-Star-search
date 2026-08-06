using EasyField.ObjectsStorages;
using System.Collections.Generic;

namespace EasyField.Links.Providers
{
    public class StoredLinksProvider<TLinkData, TId> : ILinksProvider<TLinkData, TId>
        where TLinkData : ILinkData<TId>
    {
        private readonly Dictionary<TId, HashSet<TId>> _outgoingIndex = new Dictionary<TId, HashSet<TId>>();
        private readonly Dictionary<TId, HashSet<TId>> _incomingIndex = new Dictionary<TId, HashSet<TId>>();

        private readonly DictTypeStorage<TLinkData, LinkKey<TId>> _linkDatas;


        public StoredLinksProvider(DictTypeStorage<TLinkData, LinkKey<TId>> linkDatas) 
        {
            _linkDatas = linkDatas;
        }
                
        public bool TryGetLink(TId fromId, TId toId, out TLinkData link)
        {
            var key = new LinkKey<TId>(fromId, toId);

            return _linkDatas.TryGetItem(key, out link);
        }

        public void AddLink(TLinkData link)
        {
            var key = new LinkKey<TId>(link.From, link.To);
            var fromId = key.From;
            var toId = key.To;

            _linkDatas.AddItem(key, link);            

            if (!_outgoingIndex.TryGetValue(fromId, out var outgoing))
            {
                outgoing = new HashSet<TId>();
                _outgoingIndex[fromId] = outgoing;
            }
            outgoing.Add(toId);

            if (!_incomingIndex.TryGetValue(toId, out var incoming))
            {
                incoming = new HashSet<TId>();
                _incomingIndex[toId] = incoming;
            }
            incoming.Add(fromId);
        }

        public void RemoveLink(TLinkData link)
        {
            var key = new LinkKey<TId>(link.From, link.To);
            var fromId = key.From;
            var toId = key.To;

            _linkDatas.RemoveItem(key);

            if (_outgoingIndex.TryGetValue(fromId, out var outgoing))
            {
                outgoing.Remove(toId);
                if (outgoing.Count == 0) 
                    _outgoingIndex.Remove(fromId);
            }

            if (_incomingIndex.TryGetValue(toId, out var incoming))
            {
                incoming.Remove(fromId);
                if (incoming.Count == 0) 
                    _incomingIndex.Remove(toId);
            }
        }

        public void ClearAllLinks()
        {
            _outgoingIndex.Clear();
            _incomingIndex.Clear();

            _linkDatas.ClearData();
        }

        public IEnumerable<TLinkData> GetLinksFromNode(TId id)
        {
            if (_outgoingIndex.TryGetValue(id, out var targetIds))
            {
                foreach (var targetId in targetIds)
                {
                    yield return _linkDatas.GetItem(new LinkKey<TId>(id, targetId));
                }
            }
        }

        public IEnumerable<TLinkData> GetLinksToNode(TId id)
        {
            if (_incomingIndex.TryGetValue(id, out var sourceIds))
            {
                foreach (var sourceId in sourceIds)
                {
                    yield return _linkDatas.GetItem(new LinkKey<TId>(sourceId, id));
                }
            }
        }
    }
}


