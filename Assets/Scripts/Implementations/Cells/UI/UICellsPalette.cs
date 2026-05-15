using Core.Inputs;
using Core.Signals;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells.UI
{
    public class UICellsPalette : MonoBehaviour
    {
        [SerializeField] 
        private UICellsPaletteItem _itemPrefab;
        [SerializeField] 
        private Transform _container;

        private List<UICellsPaletteItem> _items = new List<UICellsPaletteItem>();

        public IReadOnlyList<UICellsPaletteItem> Items => _items;

        private IInstantiator _instantiator;        
        private SignalBus _signalBus;
        private IInputService _inputService;
        private CellsConfig _cellsConfig;


        [Inject]
        public void Construct(IInstantiator instantiator, SignalBus signalBus, IInputService inputService, CellsConfig cellsConfig)
        {
            _instantiator = instantiator;
            _signalBus = signalBus;
            _inputService = inputService;
            _cellsConfig = cellsConfig;
        }

        private void Start()
        {
            foreach (var cellType in _cellsConfig.CellTypes)
            {
                var item = _instantiator.InstantiatePrefabForComponent<UICellsPaletteItem>(_itemPrefab, _container);
                item.Init(cellType);
                _items.Add(item);
                item.ItemClicked += OnItemClicked;
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

                    _signalBus.Fire(new PaletteItemClickedSignal(item.CellType, button));
                }
            }
        }

        private void OnItemClicked(CellType cellType, PointerEventData.InputButton button)
        {
            _signalBus.Fire(new PaletteItemClickedSignal(cellType, button));
        }

        private void OnDestroy()
        {
            foreach (var item in _items) item.ItemClicked -= OnItemClicked;
        }
    }
}