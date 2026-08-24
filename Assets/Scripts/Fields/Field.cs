using UnityEngine;

namespace EasyField.Fields
{
    public abstract class Field : MonoBehaviour, IField
    {        
        [SerializeField]
        protected SpriteRenderer _imgFrame;
        [SerializeField]
        protected SpriteRenderer _imgBackground;        

        [Space]
        [SerializeField]
        protected BoxCollider2D _collider;
        [SerializeField]
        protected Transform _nodesContainer;
        [SerializeField]
        protected Transform _linksContainer;

        protected Vector2 _framePadding;

        public BoxCollider2D Box => _collider;
        public Transform NodesContainer => _nodesContainer;
        public Transform LinksContainer => _linksContainer;

        public abstract Vector2 ScaleFactor { get; }


        private void Awake()
        {
            var border = _imgFrame.sprite.border;
            _framePadding = new Vector2(border.x + border.w, border.y + border.w) / _imgFrame.sprite.pixelsPerUnit;
        }

        protected void UpdateGraphics(Vector2 size)
        {
            _imgBackground.size = size;
            _imgFrame.size = size + _framePadding;
        }
    }
}