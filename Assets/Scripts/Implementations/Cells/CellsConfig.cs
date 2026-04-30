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
        private CellType[] _cellTypes;
        [SerializeField]
        private CellType _defaultCellType;

        private Dictionary<CellId, CellType> _cellTypesDict;


        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle();

            _cellTypesDict = _cellTypes.ToDictionary(t => t.Id);
        }

        public CellType DefaultCellType => _defaultCellType;

        public CellType GetCellType(CellId id)
        {
            return _cellTypesDict.TryGetValue(id, out var cell) ? cell : null;
        }

        public float GetMinimumCellTypeWeight()
        {
            return _cellTypes.Min(cellType => cellType.Weight);
        }
    }
}