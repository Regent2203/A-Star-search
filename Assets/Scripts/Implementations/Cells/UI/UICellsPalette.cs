using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Zenject;

namespace Core.Implementations.Cells.UI
{
    public class UICellsPalette : MonoBehaviour
    {
        [SerializeField] 
        private UICellsPaletteItem _itemPrefab;
        [SerializeField] 
        private Transform _container;

        private List<UICellsPaletteItem> _allItems = new List<UICellsPaletteItem>();

        public IReadOnlyList<UICellsPaletteItem> AllItems => _allItems;

        private IInstantiator _instantiator;
        private CellsConfig _cellsConfig;


        [Inject]
        public void Construct(IInstantiator instantiator, CellsConfig cellsConfig)
        {
            _instantiator = instantiator;
            _cellsConfig = cellsConfig;
        }

        private void Start()
        {
            foreach (var cellType in _cellsConfig.CellTypes)
            {
                var item = _instantiator.InstantiatePrefabForComponent<UICellsPaletteItem>(_itemPrefab, _container);
                item.Init(cellType);
                _allItems.Add(item);
            }
        }
    }
}