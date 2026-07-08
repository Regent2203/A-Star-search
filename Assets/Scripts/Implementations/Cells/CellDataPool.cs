using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellDataPool : PoolableMemoryPool<Vector2Int, Vector2, CellType, CellData>
    {
    }
}