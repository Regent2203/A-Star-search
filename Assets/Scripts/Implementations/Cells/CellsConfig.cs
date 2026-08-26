using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace EasyField.Implementations.Cells
{
    [CreateAssetMenu(fileName = "CellsConfig", menuName = "EasyField/CellsConfig")]
    public class CellsConfig : ScriptableObjectInstaller<CellsConfig>
    {
        [SerializeField]
        private List<CellType> _cellTypes;
        [SerializeField]
        private CellType _defaultCellType;

        private float _minCellTypeWeight;

        public IReadOnlyDictionary<CellTypeId, CellType> CellTypes;
        public CellType DefaultCellType => _defaultCellType;


        public override void InstallBindings()
        {
            CellTypes = _cellTypes.ToDictionary(cellType => cellType.Id);
            _minCellTypeWeight = _cellTypes.Min(cellType => cellType.MoveCost);

            Container.BindInstance(this).AsSingle();
        }

        public float GetMinimumCellTypeWeight()
        {
            return _minCellTypeWeight;
        }
    }
}