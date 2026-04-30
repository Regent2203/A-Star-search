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


        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle();
        }

        public CellType DefaultCellType => _defaultCellType;

        public float GetMinimumCellTypeWeight()
        {
            return _cellTypes.Min(cellType => cellType.Weight);
        }
    }
}