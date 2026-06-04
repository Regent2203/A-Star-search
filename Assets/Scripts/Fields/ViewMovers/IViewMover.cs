using System;
using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Fields.ViewMovers
{
    public interface IViewMover
    {
        public bool TryMoveView(IView node, Vector2 nodePosition);

        public event Action<IView, Vector2> ViewMoved;
    }
}