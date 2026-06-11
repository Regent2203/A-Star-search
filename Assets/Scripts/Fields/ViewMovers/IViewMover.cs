using System;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields.ViewMovers
{
    public interface IViewMover
    {
        public bool TryMoveView(IView view, Vector2 position);

        public event Action<IView, Vector2> ViewMoved;
    }
}