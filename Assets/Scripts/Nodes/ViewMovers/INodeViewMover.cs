using System;
using UnityEngine;

namespace ThisProject.Nodes.ViewMovers
{
    public interface INodeViewMover<V>
        where V : MonoBehaviour, INodeView
    {
        public bool TryMoveView(V view, ref Vector2 position);

        public event Action<V, Vector2> ViewMoved;
    }
}