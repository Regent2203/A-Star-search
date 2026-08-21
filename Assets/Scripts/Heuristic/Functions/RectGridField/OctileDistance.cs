using UnityEngine;

namespace EasyField.Heuristic.Functions
{
    /// <summary>
    /// This heuristic function is used for rectangular grid field where movement is allowed in all eight directions, 
    /// and diagonal movement costs approximately 1.414 times of straight movement
    /// </summary>
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