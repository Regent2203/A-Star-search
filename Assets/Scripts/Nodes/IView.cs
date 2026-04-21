using UnityEngine;

namespace Core
{
    public interface IView
    {
        public Vector2 GetSize();
        public Vector3 GetCenterCoords();
    }
}
