using System;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields.ViewMovers
{
    public class ViewMover : IViewMover
    {
        private readonly IField _field;

        public event Action<IView, Vector2> ViewMoved;


        public ViewMover(IField field) 
        {
            _field = field;
        }

        public bool TryMoveView(IView view, Vector2 position)
        {
            if (view == null)
                return false;

            var size = view.GetSize() / 2;

            if (_field.AdjustPoint(ref position, size))
            {
                view.Move(position);
                ViewMoved?.Invoke(view, position);

                return true;
            }

            return false;
        }
    }
}