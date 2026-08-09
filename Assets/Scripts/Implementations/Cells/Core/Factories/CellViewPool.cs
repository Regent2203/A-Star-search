using UnityEngine;
using Zenject;

namespace EasyField.Implementations.Cells
{
    public class CellViewPool : MonoPoolableMemoryPool<Vector2Int, Vector2, CellView>
    {
    }
}