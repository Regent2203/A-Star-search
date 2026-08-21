using UnityEngine;

namespace EasyField.Heuristic.Functions
{
    /// <summary>
    /// Hexagonal heuristic for pointy-topped grid where odd rows are shifted right.
    /// </summary>
    public class OddRDistance : HexGridDistance
    {
        protected override Vector3Int OffsetToCube(int col, int row)
        {
            int x = col - (row - (row & 1)) / 2;
            int z = row;
            return new Vector3Int(x, -x - z, z);
        }
    }
}
