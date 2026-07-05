using UnityEngine;

namespace ThisProject.Fields
{
    public interface IField
    {
        public Transform NodesContainer { get; }
        public Transform LinksContainer { get; }
        public BoxCollider2D Box { get; }
        public Vector2 ScaleFactor { get; }
    }
}
