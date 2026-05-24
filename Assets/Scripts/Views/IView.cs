using UnityEngine;

namespace ThisProject.Views
{
    public interface IView
    {
        public Vector2 GetSize();
        public Vector3 GetCenterCoords();
    }

    public interface IView<out TId> : IView
    {
        TId Id { get; }
    }
}