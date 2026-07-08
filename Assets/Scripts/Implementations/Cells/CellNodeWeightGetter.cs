using ThisProject.Links.Factories.CostProviders;

namespace ThisProject.Implementations.Cells
{
    public class CellNodeWeightGetter : IWeightGetter<CellData>
    {
        public float GetWeight(CellData node)
        {
            return node.CellType.MoveCost;
        }
    }
}