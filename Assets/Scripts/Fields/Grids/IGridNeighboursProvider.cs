using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    public interface IGridNeighboursProvider
    {
        public IReadOnlyList<INode> GetNeighbours(int i, int j, INode[,] gridNodes);
    }
}