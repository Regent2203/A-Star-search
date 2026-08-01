using ThisProject.Links.CostProviders;

namespace ThisProject.Implementations.Cells
{
    public class CellWeightGetter : IWeightGetter<CellData>
    {
        public float GetWeight(CellData node)
        {
            return node.CellType.MoveCost;
        }
    }
}