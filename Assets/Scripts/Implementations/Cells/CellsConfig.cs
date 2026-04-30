using System.Linq;
using UnityEngine;

namespace Core.Implementations.Cells
{
    [CreateAssetMenu(fileName = "CellsConfig", menuName = "Core/CellsConfig")]
    public class CellsConfig : ScriptableObject
    {
        [SerializeField]
        private CellType[] _cellTypes;
        [SerializeField]
        private CellType _defaultCellType;


        public CellType DefaultCellType => _defaultCellType;

        public float GetMinimumCellTypeWeight()
        {
            return _cellTypes.Min(cellType => cellType.Weight);
        }
    }
}