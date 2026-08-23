using EasyField.Nodes;
using UnityEngine;

namespace EasyField.Links.CostProviders
{
    public class DiagonalCostProvider<TNodeData> : ICostProvider<TNodeData>
        where TNodeData : INodeData<Vector2Int>
    {
        private readonly ICostProvider<TNodeData> _baseCostProvider;


        public DiagonalCostProvider(ICostProvider<TNodeData> baseCostProvider)
        {
            _baseCostProvider = baseCostProvider;
        }

        public float GetCost(TNodeData from, TNodeData to)
        {
            var baseCost = _baseCostProvider.GetCost(from, to);

            if (IsDiagonal(from, to))
            {
                return baseCost * PathfindingConstants.DiagonalCost;
            }

            return baseCost;
        }

        private bool IsDiagonal(TNodeData from, TNodeData to)
        {
            var dX = Mathf.Abs(from.Id.x - to.Id.x);
            var dY = Mathf.Abs(from.Id.y - to.Id.y);

            if (dX > 1 || dY > 1)
            {
                Debug.LogError($"Comparing non-neighbour nodes! This must not occur. Id: {from.Id} and {to.Id}");
                return false;
            }
            
            return dX == 1 && dY == 1;
        }
    }
}