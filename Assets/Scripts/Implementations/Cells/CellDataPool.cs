using UnityEngine;
using Zenject;

namespace EasyField.Implementations.Cells
{
    public class CellDataPool : PoolableMemoryPool<Vector2Int, Vector2, CellType, CellData>
    {
    }
}