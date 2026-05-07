using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    public interface IGridNeighboursProvider<T> where T : INode<T>
    {
        public IReadOnlyList<T> GetNeighbours(int i, int j, T[,] gridNodes);
    }
}