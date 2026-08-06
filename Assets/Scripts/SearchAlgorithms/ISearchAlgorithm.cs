using EasyField.Nodes;
using System.Collections.Generic;

namespace EasyField.SearchAlgorithms
{
    public interface ISearchAlgorithm<TNodeData>
        where TNodeData : INodeData
    {
        public IList<TNodeData> CalculateWay(TNodeData startNode, TNodeData finishNode);
    }
}