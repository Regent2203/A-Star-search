using System.Collections.Generic;

namespace Core.Fields.Grids
{
    public interface IGridNeighboursProvider<T>
    {
        public IReadOnlyList<T> GetNeighbours(T node, T[,] gridNodes);
    }
}