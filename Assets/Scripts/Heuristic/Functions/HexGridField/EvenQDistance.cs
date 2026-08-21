using UnityEngine;

namespace EasyField.Heuristic.Functions
{
    /// <summary>
    /// Hexagonal heuristic for flat-topped grid where even columns are shifted down.
    /// </summary>
    public class EvenQDistance : HexGridDistance
    {
        protected override Vector3Int OffsetToCube(int col, int row)
        {
            int x = col - (row - (row & 1)) / 2;
            int z = row;
            return new Vector3Int(x, -x - z, z);
        }
    }
}