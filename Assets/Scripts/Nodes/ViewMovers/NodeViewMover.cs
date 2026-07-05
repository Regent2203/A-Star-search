using System;
using ThisProject.Fields;
using UnityEngine;

namespace ThisProject.Nodes.ViewMovers
{
    public class NodeViewMover : INodeViewMover
    {
        private readonly IField _field;

        public event Action<INodeView, Vector2> ViewMoved;


        public NodeViewMover(IField field) 
        {
            _field = field;
        }

        public bool TryMoveView(INodeView view, ref Vector2 position)
        {
            if (view == null)
                return false;

            var offset = view.GetSize() / 2;
            position = position.Clamp(_field.Box.bounds, offset);

            view.Move(position);
            ViewMoved?.Invoke(view, position);

            return true;
        }
    }
}