using UnityEngine;

namespace EasyField.Heuristic.Functions
{
    /// <summary>
    /// Hexagonal heuristic for flat-topped grid where even columns are shifted up.
    /// </summary>
    public class OddQDistance : HexGridDistance
    {
        protected override Vector3Int OffsetToCube(int col, int row)
        {
            int x = col;
            int z = row - (col - (col & 1)) / 2;
            return new Vector3Int(x, -x - z, z);
        }
    }
}