using UnityEngine;

namespace ThisProject.Views
{
    public interface IView
    {
        public Vector2 GetSize();
        public Vector3 GetCenterCoords();
        public void Move(Vector2 position);
    }

    public interface IView<TId> : IView
    {
        TId Id { get; }
    }
}