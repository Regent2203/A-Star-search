using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    [CreateAssetMenu(fileName = "CellsConfig", menuName = "Core/CellsConfig")]
    public class CellsConfig : ScriptableObjectInstaller<CellsConfig>
    {
        [SerializeField]
        private List<CellType> _cellTypes;
        [SerializeField]
        private CellType _defaultCellType;

        public IReadOnlyList<CellType> CellTypes => _cellTypes;
        public CellType DefaultCellType => _defaultCellType;


        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle();
        }

        public float GetMinimumCellTypeWeight()
        {
            return _cellTypes.Min(cellType => cellType.MoveCost);
        }
    }
}