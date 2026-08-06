using EasyField.Nodes;
using EasyField.SearchAlgorithms;
using System.Collections.Generic;

namespace EasyField.PathFinders
{
    //todo remove this class??
    public class PathFinder<TNodeData, TId> : IPathFinder<TNodeData>
        where TNodeData : INodeData<TId>
    {
        private readonly ISearchAlgorithm<TNodeData> _searchAlgorithm;


        public PathFinder(ISearchAlgorithm<TNodeData> searchAlgorithm)
        {
            _searchAlgorithm = searchAlgorithm;
        }

        public IList<TNodeData> GetPath(TNodeData startNode, TNodeData finishNode)
        {
            if (startNode == null || finishNode == null) 
                return null;

            return _searchAlgorithm.CalculateWay(startNode, finishNode);
        }
    }
}
