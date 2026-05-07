using Core.Heuristic;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.SearchAlgorithms
{
    public interface ISearchAlgorithm
    {
        public IList<INode> CalculateWay(INode startNode, INode finishNode, IHeuristicsProvider heuristicsController);
    }
}