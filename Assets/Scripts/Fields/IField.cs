using UnityEngine;

namespace ThisProject.Fields
{
    public interface IField
    {
        public Transform Container { get; }
        public BoxCollider2D Box { get; }
        public Vector2 ScaleFactor { get; }
    }
}
