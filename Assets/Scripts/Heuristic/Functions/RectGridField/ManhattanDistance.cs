using UnityEngine;

namespace EasyField.Heuristic.Functions
{
    /// <summary>
    /// This heuristic function is used for rectangular grid field where movement is allowed in four orthogonal directions only,
    /// and diagonal movement is not allowed
    /// </summary>
    public class ManhattanDistance : IHeuristicFunction
    {
        private const float TieBreaker = 1.001f;

        public float Estimate(Vector2 p1, Vector2 p2)
        {
            var dx = Mathf.Abs(p2.x - p1.x);
            var dy = Mathf.Abs(p2.y - p1.y);

            return (dx + dy) * TieBreaker; //1.001f makes path more straight
        }
    }
}