using Core.Implementations.Cells;

namespace Core.Heuristic
{
    public class CellsHeuristicsProvider : HeuristicsProvider
    {
        public CellsHeuristicsProvider(CellsConfig cellsConfig, IHeuristicFunction heuristicFunction) : base(heuristicFunction)
        {
            SetMinimumStepCost(cellsConfig.GetMinimumCellTypeWeight());
        }
    }
}