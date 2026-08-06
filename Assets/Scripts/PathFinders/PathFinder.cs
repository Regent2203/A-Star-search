using System.Collections.Generic;
using EasyField.Nodes;
using EasyField.SearchAlgorithms;

namespace EasyField.PathFinders
{
    //todo remove this class??
    public class PathFinder<T, TId> : IPathFinder<T>
        where T : INodeData<TId>
    {
        private readonly ISearchAlgorithm<T> _searchAlgorithm;


        public PathFinder(ISearchAlgorithm<T> searchAlgorithm)
        {
            _searchAlgorithm = searchAlgorithm;
        }

        public IList<T> GetPath(T startNode, T finishNode)
        {
            if (startNode == null || finishNode == null) 
                return null;

            return _searchAlgorithm.CalculateWay(startNode, finishNode);
        }
    }
}
