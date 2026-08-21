using UnityEngine;

namespace EasyField.Heuristic.Functions
{
    /// <summary>
    /// Defines a contract for heuristic functions used in pathfinding algorithms 
    /// to estimate the movement cost between two points.
    /// </summary>
    public interface IHeuristicFunction
    {
        /// <summary>
        /// Estimates the heuristic cost from the start point to the destination point.
        /// </summary>
        /// <param name="p1">The starting position.</param>
        /// <param name="p2">The target position.</param>
        /// <returns>The estimated movement cost.</returns>
        public float Estimate(Vector2 p1, Vector2 p2);
    }
}