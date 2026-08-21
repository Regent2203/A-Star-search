using UnityEngine;

namespace EasyField.Heuristic.Functions
{
    /// <summary>
    /// Base class for hexagonal grid distance heuristics using a rectangular array layout.
    /// </summary>
    public abstract class HexGridDistance : IHeuristicFunction
    {
        private const float TieBreaker = 1.001f;

        public float Estimate(Vector2 p1, Vector2 p2)
        {
            int col1 = Mathf.RoundToInt(p1.x);
            int row1 = Mathf.RoundToInt(p1.y);
            int col2 = Mathf.RoundToInt(p2.x);
            int row2 = Mathf.RoundToInt(p2.y);

            Vector3Int cube1 = OffsetToCube(col1, row1);
            Vector3Int cube2 = OffsetToCube(col2, row2);

            int dx = Mathf.Abs(cube1.x - cube2.x);
            int dy = Mathf.Abs(cube1.y - cube2.y);
            int dz = Mathf.Abs(cube1.z - cube2.z);

            int hexDistance = Mathf.Max(dx, Mathf.Max(dy, dz));

            return hexDistance * TieBreaker;
        }

        /// <summary>
        /// Implements specific offset to cube coordinate conversion logic.
        /// </summary>
        protected abstract Vector3Int OffsetToCube(int col, int row);
    }
}