using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells.UI
{
    public class UICellsPalette : MonoBehaviour
    {
        [SerializeField]
        private List<UICellsPaletteItem> _allItems;

        public IReadOnlyList<UICellsPaletteItem> AllItems => _allItems;

        private CellsConfig _cellsConfig;


        [Inject]
        public void Construct(CellsConfig cellsConfig)
        {
            _cellsConfig = cellsConfig;
        }

        private void Start()
        {
            foreach (var cellType in _cellsConfig.CellTypes)
            {
                //todo init
            }
        }
    }
}