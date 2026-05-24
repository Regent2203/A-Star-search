using ThisProject.Links.Factories.CostProviders;

namespace ThisProject.Implementations.Cells
{
    public class CellNodeWeightGetter : IWeightGetter<CellNode>
    {
        public float GetWeight(CellNode node)
        {
            return node.CellType.MoveCost;
        }
    }
}