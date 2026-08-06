using EasyField.Links.CostProviders;

namespace EasyField.Implementations.Cells
{
    public class CellWeightGetter : IWeightGetter<CellData>
    {
        public float GetWeight(CellData node)
        {
            return node.CellType.MoveCost;
        }
    }
}