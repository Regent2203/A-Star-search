using System;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Fields.ViewMovers
{
    public interface IViewMover
    {
        public bool TryMoveView(INodeView view, Vector2 position);

        public event Action<INodeView, Vector2> ViewMoved;
    }
}