using Fields;
using Nodes;
using System.Collections.Generic;

namespace Core.SearchAlgorithms
{
    public interface ISearchAlgorithm
    {
        public IList<INode> GetPath(AbstractField field);
    }
}
