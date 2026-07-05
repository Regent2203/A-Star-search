using UnityEngine;

namespace ThisProject.Fields
{
    public abstract class Field : MonoBehaviour, IField
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;
        [SerializeField]
        protected BoxCollider2D _collider;
        [SerializeField]
        protected Transform _nodesContainer;
        [SerializeField]
        protected Transform _linksContainer;

        public BoxCollider2D Box => _collider;
        public Transform NodesContainer => _nodesContainer;
        public Transform LinksContainer => _linksContainer;

        public abstract Vector2 ScaleFactor { get; }
    }
}