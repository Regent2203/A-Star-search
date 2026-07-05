using System;
using UnityEngine;

namespace ThisProject.Nodes.ViewMovers
{
    public interface INodeViewMover
    {
        public bool TryMoveView(INodeView view, ref Vector2 position);

        public event Action<INodeView, Vector2> ViewMoved;
    }
}