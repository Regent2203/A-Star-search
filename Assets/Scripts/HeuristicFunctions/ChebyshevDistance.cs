using UnityEngine;

namespace Core.HeuristicFunctions
{
    public class ChebyshevDistance : IHeuristicFunction
    {
        public float EstimateCost(IView node1, IView node2)
        {
            var p1 = node1.GetCenterCoords();
            var p2 = node2.GetCenterCoords();

            var dx = Mathf.Abs(p2.x - p1.x);
            var dy = Mathf.Abs(p2.y - p1.y);

            return Mathf.Max(dx, dy);
        }
    }
}