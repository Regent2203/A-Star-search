using UnityEngine;

namespace ThisProject.Fields
{
    public class SpatialField: Field
    {
        [SerializeField]
        protected Vector2 _scaleFactor = Vector2.one;

        public override Vector2 ScaleFactor => _scaleFactor;
    }
}