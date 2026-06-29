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
    }
}