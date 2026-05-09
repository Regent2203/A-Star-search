using UnityEngine;

namespace Core.Heuristic.Functions
{
    public class OctileDistance : IHeuristicFunction
    {
        private const float StraightCost = 1.0f;
        private const float DiagonalCost = 1.414f;

        public float Estimate(Vector2 p1, Vector2 p2)
        {
            float dx = Mathf.Abs(p1.x - p2.x);
            float dy = Mathf.Abs(p1.y - p2.y);

            return StraightCost * (dx + dy) + (DiagonalCost - 2 * StraightCost) * Mathf.Min(dx, dy);
        }
    }
}