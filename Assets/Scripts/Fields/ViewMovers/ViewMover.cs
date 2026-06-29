using System;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Fields.ViewMovers
{
    public class ViewMover : IViewMover
    {
        private readonly IField _field;

        public event Action<INodeView, Vector2> ViewMoved;


        public ViewMover(IField field) 
        {
            _field = field;
        }

        public bool TryMoveView(INodeView view, Vector2 position)
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