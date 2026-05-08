using UnityEngine;

namespace Core.Heuristic
{
    public class OctileDistance : IHeuristicFunction
    {
        public float Estimate(Vector2 p1, Vector2 p2)
        {
            float dx = Mathf.Abs(p1.x - p2.x);
            float dy = Mathf.Abs(p1.y - p2.y);

            float straightCost = 1.0f;
            float diagonalCost = 1.414f;

            return straightCost * (dx + dy) + (diagonalCost - 2 * straightCost) * Mathf.Min(dx, dy);
        }
    }
}