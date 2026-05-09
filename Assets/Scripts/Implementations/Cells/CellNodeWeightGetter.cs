using Core.CostProviders;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellNodeWeightGetter : IWeightGetter<CellNode, Vector2Int>
    {
        public float GetWeight(CellNode node)
        {
            return node.CellType.Weight;
        }
    }
}