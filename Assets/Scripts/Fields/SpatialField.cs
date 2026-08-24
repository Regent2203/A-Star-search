using UnityEngine;

namespace EasyField.Fields
{
    public class SpatialField: Field
    {
        [SerializeField]
        protected Vector2 _scaleFactor = Vector2.one;

        protected Vector2 _size = Vector2.zero;

        public override Vector2 ScaleFactor => _scaleFactor;
        public Vector2 Size => _size;


        public void SetSize(Vector2 size)
        {
            _size = size;

            _collider.size = size;
            _collider.offset = _collider.size / 2;

            UpdateGraphics(size);            
        }
    }
}