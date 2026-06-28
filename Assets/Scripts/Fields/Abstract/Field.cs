using UnityEngine;

namespace ThisProject.Fields
{
    public abstract class Field : MonoBehaviour, IField
    {
        [SerializeField]
        protected BoxCollider2D _collider;
        [SerializeField]
        protected Transform _container;
        

        public BoxCollider2D Box => _collider;
        public Transform Container => _container;

        public abstract Vector2 ScaleFactor { get; }


        public bool AdjustPoint(ref Vector2 pos, Vector2 size)
        {
            if (!Box.OverlapPoint(pos))
            {
                return false;
            }

            var bounds = Box.bounds;

            //X
            var distL = pos.x - bounds.min.x;
            var distR = bounds.max.x - pos.x;

            if (distL < size.x)
                pos.x = bounds.min.x + size.x;
            else if (distR < size.x)
                pos.x = bounds.max.x - size.x;

            //Y
            var distB = pos.y - bounds.min.y;
            var distT = bounds.max.y - pos.y;

            if (distB < size.y)
                pos.y = bounds.min.y + size.y;
            else if (distT < size.y)
                pos.y = bounds.max.y - size.y;

            return true;
        }
    }
}