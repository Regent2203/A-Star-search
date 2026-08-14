using EasyField.Nodes;
using EasyField.SearchAlgorithms;
using System;
using System.Collections.Generic;

namespace EasyField.PathFinders
{
    public class CachePathFinder<TNodeData> : IPathFinder<TNodeData>
        where TNodeData : INodeData
    {
        private readonly Dictionary<PathKey, IList<TNodeData>> _cache = new();

        private readonly ISearchAlgorithm<TNodeData> _searchAlgorithm;
        

        public CachePathFinder(ISearchAlgorithm<TNodeData> searchAlgorithm)
        {
            _searchAlgorithm = searchAlgorithm;
        }

        public IList<TNodeData> GetPath(TNodeData startNode, TNodeData finishNode)
        {
            if (startNode == null || finishNode == null)
                return null;

            var key = new PathKey(startNode, finishNode);

            if (_cache.TryGetValue(key, out var cachedPath))
            {
                return cachedPath;
            }

            var calculatedPath = _searchAlgorithm.CalculateWay(startNode, finishNode);
            _cache[key] = calculatedPath;

            return calculatedPath;
        }

        /// <summary>
        /// Use this method inside FieldChanged() method in sceneControllers
        /// </summary>
        public void ClearCache()
        {
            _cache.Clear();
        }

        #region PathKey
        private readonly struct PathKey : IEquatable<PathKey>
        {
            public readonly TNodeData Start;
            public readonly TNodeData Finish;

            public PathKey(TNodeData start, TNodeData finish)
            {
                Start = start;
                Finish = finish;
            }

            public bool Equals(PathKey other)
            {
                return EqualityComparer<TNodeData>.Default.Equals(Start, other.Start) &&
                       EqualityComparer<TNodeData>.Default.Equals(Finish, other.Finish);
            }

            public override bool Equals(object obj) => obj is PathKey other && Equals(other);

            public override int GetHashCode()
            {
                return HashCode.Combine(Start, Finish);
            }
        }
        #endregion
    }
}