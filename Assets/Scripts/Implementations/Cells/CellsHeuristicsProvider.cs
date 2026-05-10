using Core.Heuristic;
using Core.Heuristic.Functions;

namespace Core.Implementations.Cells
{
    public class CellsHeuristicsProvider : HeuristicsProvider<CellNode>
    {
        public CellsHeuristicsProvider(CellsConfig cellsConfig, IHeuristicFunction heuristicFunction)
            : base(heuristicFunction, cellsConfig.GetMinimumCellTypeWeight()) { }
    }
}