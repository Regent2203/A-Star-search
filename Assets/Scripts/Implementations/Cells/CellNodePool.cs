using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellNodePool : PoolableMemoryPool<Vector2Int, Vector2, CellType, CellNode>
    {
    }
}