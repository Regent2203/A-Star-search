using UnityEngine;

namespace Nodes
{
    public interface IView
    {
        public Vector2 GetSize();
        public Vector3 GetCenterCoords();
    }
}
