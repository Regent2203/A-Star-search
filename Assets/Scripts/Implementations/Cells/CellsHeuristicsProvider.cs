using EasyField.Heuristic;
using EasyField.Heuristic.Functions;

namespace EasyField.Implementations.Cells
{
    public class CellsHeuristicsProvider : HeuristicsProvider<CellData>
    {
        public CellsHeuristicsProvider(CellsConfig cellsConfig, IHeuristicFunction heuristicFunction)
            : base(heuristicFunction, cellsConfig.GetMinimumCellTypeWeight()) { }
    }
}