using Core.Implementations.Cells;
using UnityEngine;

namespace Core.Heuristic
{
    public class CellsHeuristicsProvider : HeuristicsProvider<CellNode, Vector2Int>
    {
        public CellsHeuristicsProvider(CellsConfig cellsConfig, IHeuristicFunction heuristicFunction)
            : base(heuristicFunction, cellsConfig.GetMinimumCellTypeWeight()) { }
    }
}