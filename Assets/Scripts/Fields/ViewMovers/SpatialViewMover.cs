using System;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields.ViewMovers
{
    public class SpatialViewMover : IViewMover
    {
        private readonly IVisibleField _field;

        public event Action<IView, Vector2> ViewMoved;


        public SpatialViewMover(IVisibleField field) 
        {
            _field = field;
        }

        public bool TryMoveView(IView view, Vector2 position)
        {
            if (view == null)
                return false;

            if (_field.CheckAndAdjustPoint(ref position))
            {
                view.Move(position);
                ViewMoved?.Invoke(view, position);

                return true;
            }

            return false;
        }
    }
}