using UnityEngine;

namespace ThisProject.Nodes
{
    public interface INodeView
    {
        public Vector2 GetSize();
        public Vector3 GetCenterCoords();
        public void Move(Vector2 position);
    }

    public interface INodeView<TId> : INodeView
    {
        TId Id { get; }
    }
}