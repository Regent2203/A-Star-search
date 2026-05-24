using ThisProject.Heuristic;
using ThisProject.Heuristic.Functions;

namespace ThisProject.Implementations.Cells
{
    public class CellsHeuristicsProvider : HeuristicsProvider<CellNode>
    {
        public CellsHeuristicsProvider(CellsConfig cellsConfig, IHeuristicFunction heuristicFunction)
            : base(heuristicFunction, cellsConfig.GetMinimumCellTypeWeight()) { }
    }
}