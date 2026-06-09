using System;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields.ViewMovers
{
    public class SpatialViewMover : IViewMover
    {
        private readonly BoxCollider2D _box;

        public event Action<IView, Vector2> ViewMoved;


        public SpatialViewMover(IVisibleField field) 
        {
            _box = field.Box;
        }

        public bool TryMoveView(IView view, Vector2 position)
        {
            if (view == null)
                return false;

            if (!IsInsideBorders(position))
                return false;

            view.Move(position);
            ViewMoved?.Invoke(view, position);

            return true;
        }

        private bool IsInsideBorders(Vector2 nodePosition)
        {
            return _box.OverlapPoint(nodePosition);
        }
    }
}