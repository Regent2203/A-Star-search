using Core.Nodes;
using UnityEngine;

namespace Core.HeuristicFunctions
{
    public class EuclideanDistance : IHeuristicFunction
    {
        public float EstimateCost(IEstimatable node1, IEstimatable node2)
        {
            var p1 = node1.GetEstimatedPosition();
            var p2 = node2.GetEstimatedPosition();

            var dx = Mathf.Abs(p2.x - p1.x);
            var dy = Mathf.Abs(p2.y - p1.y);

            return Mathf.Sqrt(Mathf.Pow(dx,2) + Mathf.Pow(dy, 2));
        }
    }
}
