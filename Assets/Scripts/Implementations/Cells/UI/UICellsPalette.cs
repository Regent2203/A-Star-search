using ThisProject.Inputs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Implementations.Cells.UI
{
    public class UICellsPalette : MonoBehaviour
    {
        [SerializeField] 
        private UICellsPaletteItem _itemPrefab;
        [SerializeField] 
        private Transform _container;

        private readonly List<UICellsPaletteItem> _items = new List<UICellsPaletteItem>();

        private IInstantiator _instantiator;
        private IInputService _inputService;
        private CellsConfig _cellsConfig;

        public IReadOnlyList<UICellsPaletteItem> Items => _items;
        public event Action<CellType, PointerEventData.InputButton> ItemClicked;


        [Inject]
        public void Construct(IInstantiator instantiator, IInputService inputService, CellsConfig cellsConfig)
        {
            _instantiator = instantiator;
            _inputService = inputService;
            _cellsConfig = cellsConfig;
        }

        private void Start()
        {
            foreach (var cellType in _cellsConfig.CellTypes)
            {
                var item = _instantiator.InstantiatePrefabForComponent<UICellsPaletteItem>(_itemPrefab, _container);
                item.Init(cellType, OnItemClicked);
                _items.Add(item);
            }
        }

        private void Update()
        {
            foreach (var item in _items)
            {
                if (Input.GetKeyDown(item.CellType.PaletteHotkey))
                {
                    var button = _inputService.IsMarkingMode
                        ? PointerEventData.InputButton.Right
                        : PointerEventData.InputButton.Left;

                    ItemClicked?.Invoke(item.CellType, button);
                }
            }
        }

        private void OnItemClicked(CellType cellType, PointerEventData.InputButton button)
        {
            ItemClicked?.Invoke(cellType, button);
        }
    }
}