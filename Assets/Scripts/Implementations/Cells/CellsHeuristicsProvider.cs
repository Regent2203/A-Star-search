using Core.Implementations.Cells;

namespace Core.Heuristic
{
    public class CellsHeuristicsProvider : HeuristicsProvider<CellNode>
    {
        public CellsHeuristicsProvider(CellsConfig cellsConfig, IHeuristicFunction heuristicFunction)
            : base(heuristicFunction, cellsConfig.GetMinimumCellTypeWeight()) { }
    }
}