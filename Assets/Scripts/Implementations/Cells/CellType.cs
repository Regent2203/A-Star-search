using UnityEngine;
using UnityEngine.Serialization;

namespace ThisProject.Implementations.Cells
{
    public enum CellId 
    {
        Unassigned = 0,
        Normal = 1,
        Obstacle = 2,
        Dirt = 3,
        Sand = 4, 
        Swamp = 5,
    }

    [CreateAssetMenu(fileName = "CellType", menuName = "Core/CellType")]
    public class CellType : ScriptableObject
    {
        [SerializeField]
        private CellId _id;
        [SerializeField]
        private string _name;
        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private float _moveCost = float.PositiveInfinity;
        [SerializeField]
        private KeyCode _paletteHotkey;

        public CellId Id => _id;
        public string Name => _name;
        public Sprite Sprite => _sprite;
        public float MoveCost => _moveCost;
        public KeyCode PaletteHotkey => _paletteHotkey;
    }
}