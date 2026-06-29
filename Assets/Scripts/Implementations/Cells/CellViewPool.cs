using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellViewPool : MonoPoolableMemoryPool<Vector2Int, Vector2, CellView>
    {
    }
}