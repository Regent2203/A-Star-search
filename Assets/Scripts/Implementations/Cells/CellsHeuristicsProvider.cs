using Core.Heuristic;
using Core.Heuristic.Functions;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellsHeuristicsProvider : HeuristicsProvider<CellNode, Vector2Int>
    {
        public CellsHeuristicsProvider(CellsConfig cellsConfig, IHeuristicFunction heuristicFunction)
            : base(heuristicFunction, cellsConfig.GetMinimumCellTypeWeight()) { }
    }
}