using UnityEngine;

namespace Core.Implementations.Cells
{
    [CreateAssetMenu(fileName = "CellType", menuName = "Core/CellType")]
    public class CellType : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private float _weight = float.PositiveInfinity;

        public string Name => _name;
        public Sprite Sprite => _sprite;
        public float Weight => _weight;
    }
}
