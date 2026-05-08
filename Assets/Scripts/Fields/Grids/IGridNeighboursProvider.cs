using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    public interface IGridNeighboursProvider<T, TId> where T : class, INode<T, TId>
    {
        public IReadOnlyList<T> GetNeighbours(int i, int j, T[,] gridNodes);
    }
}