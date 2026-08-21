using UnityEngine;

namespace EasyField.Heuristic.Functions
{
    /// <summary>
    /// This heuristic function is used for spatial field where movement is allowed in any direction at any angle
    /// </summary>
    public class EuclideanDistance : IHeuristicFunction
    {
        public float Estimate(Vector2 p1, Vector2 p2)
        {
            var dx = Mathf.Abs(p2.x - p1.x);
            var dy = Mathf.Abs(p2.y - p1.y);

            return Mathf.Sqrt(Mathf.Pow(dx,2) + Mathf.Pow(dy, 2));
        }
    }
}