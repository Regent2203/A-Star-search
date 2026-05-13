using Core.Links.Factories.CostProviders;

namespace Core.Implementations.Cells
{
    public class CellNodeWeightGetter : IWeightGetter<CellNode>
    {
        public float GetWeight(CellNode node)
        {
            return node.CellType.MoveCost;
        }
    }
}