using UnityEngine;

namespace ThisProject.Fields
{
    public interface IField
    {
        public Transform Container { get; }
        public BoxCollider2D Box { get; }
        public Vector2 ScaleFactor { get; }

        public bool AdjustPoint(ref Vector2 pos, Vector2 size);
    }
}
