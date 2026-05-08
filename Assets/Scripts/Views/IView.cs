using UnityEngine;

namespace Core.Views
{
    public interface IView
    {
        public Vector2 GetSize();
        public Vector3 GetCenterCoords();
    }
}