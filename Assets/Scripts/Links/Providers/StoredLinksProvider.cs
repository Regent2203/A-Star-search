using System;
using System.Collections.Generic;
using ThisProject.Nodes;

namespace ThisProject.Links.Providers
{
    public class StoredLinksProvider<T, TId> : ILinksProvider<T, TId>
        where T : INodeData<TId>
        //    where TId : IEquatable<TId>
    {
        // Основное хранилище связей по их уникальному ключу
        private readonly Dictionary<LinkKey<TId>, ILinkData<TId>> _links = new Dictionary<LinkKey<TId>, ILinkData<TId>>();

        // Индексы для быстрого поиска направлений без дублирования объектов связей
        private readonly Dictionary<TId, HashSet<TId>> _outgoingIndex = new Dictionary<TId, HashSet<TId>>();
        private readonly Dictionary<TId, HashSet<TId>> _incomingIndex = new Dictionary<TId, HashSet<TId>>();

        public bool TryAddLink(ILinkData<TId> link)
        {
            if (link == null) return false;

            var key = new LinkKey<TId>(link.From, link.To);

            // TryAdd — более эффективный атомарный метод в современном .NET
            if (!_links.TryAdd(key, link))
                return false; // Связь уже существует

            // Обновляем индекс исходящих связей
            if (!_outgoingIndex.TryGetValue(link.From, out var outgoing))
            {
                outgoing = new HashSet<TId>();
                _outgoingIndex[link.From] = outgoing;
            }
            outgoing.Add(link.To);

            // Обновляем индекс входящих связей
            if (!_incomingIndex.TryGetValue(link.To, out var incoming))
            {
                incoming = new HashSet<TId>();
                _incomingIndex[link.To] = incoming;
            }
            incoming.Add(link.From);

            return true;
        }

        public bool TryRemoveLink(TId fromId, TId toId)
        {
            var key = new LinkKey<TId>(fromId, toId);

            if (!_links.Remove(key))
                return false; // Связи не было

            // Очищаем исходящий индекс
            if (_outgoingIndex.TryGetValue(fromId, out var outgoing))
            {
                outgoing.Remove(toId);
                if (outgoing.Count == 0) _outgoingIndex.Remove(fromId);
            }

            // Очищаем входящий индекс
            if (_incomingIndex.TryGetValue(toId, out var incoming))
            {
                incoming.Remove(fromId);
                if (incoming.Count == 0) _incomingIndex.Remove(toId);
            }

            return true;
        }

        public IEnumerable<ILinkData<TId>> GetLinksFromNode(T node)
        {
            if (_outgoingIndex.TryGetValue(node.Id, out var targetIds))
            {
                foreach (var targetId in targetIds)
                {
                    yield return _links[new LinkKey<TId>(node.Id, targetId)];
                }
            }
        }

        public IEnumerable<ILinkData<TId>> GetLinksToNode(T node)
        {
            if (_incomingIndex.TryGetValue(node.Id, out var sourceIds))
            {
                foreach (var sourceId in sourceIds)
                {
                    yield return _links[new LinkKey<TId>(sourceId, node.Id)];
                }
            }
        }
    }
}


