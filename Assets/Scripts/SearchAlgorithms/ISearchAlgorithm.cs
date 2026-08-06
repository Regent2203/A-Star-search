using System.Collections.Generic;
using EasyField.Nodes;

namespace EasyField.SearchAlgorithms
{
    public interface ISearchAlgorithm<T>
        where T : INodeData
    {
        public IList<T> CalculateWay(T startNode, T finishNode);
    }
}