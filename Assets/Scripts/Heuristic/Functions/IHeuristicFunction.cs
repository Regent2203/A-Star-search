using UnityEngine;

namespace Core.Heuristic
{
    public interface IHeuristicFunction
    {
        public float Estimate(Vector2 p1, Vector2 p2);
    }
}
