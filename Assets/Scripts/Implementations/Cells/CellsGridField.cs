using Core.Fields.Grids;
using Core.Links;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsGridField : AbstractGridField<CellNode>, IPointerDownHandler
    {
        private CellView[,] _views;
        private CellViewFactory _viewsFactory;
        private CellNodeFactory _nodesFactory;
        private CellsConfig _cellsConfig;
        private IGridNeighboursProvider<CellNode> _neighboursProvider;
        private LinksProvider<CellNode> _linksProvider;

        public event Action<CellView, PointerEventData.InputButton> CellClicked;
        public event Action<CellNode> CellNodeChanged;


        [Inject]
        public void Construct(CellViewFactory viewsFactory, CellNodeFactory nodesFactory, CellsConfig cellsConfig, 
            IGridNeighboursProvider<CellNode> neighboursProvider, LinksProvider<CellNode> linker, CellView cellviewPrefab)
        {
            _viewsFactory = viewsFactory;
            _nodesFactory = nodesFactory;
            _cellsConfig = cellsConfig;
            _neighboursProvider = neighboursProvider;
            _linksProvider = linker;
            _viewPrefab = cellviewPrefab;
        }

        protected override void Init()
        {
            CreateCells(); //todo: change if we want to call CreateCells() manually (after we change grid size or else)
        }

        private void CreateCells()
        {
            _views = new CellView[_nodes.GetLength(0), _nodes.GetLength(1)];
            _viewsFactory.SetConfiguration(_scaleFactor, transform);

            for (int i = 0; i < _nodes.GetLength(0); i++)
            {
                for (int j = 0; j < _nodes.GetLength(1); j++)
                {
                    var pos = _grid.CellToWorld(new Vector3Int(i, j, 0));
                    var index = new Vector2Int(i, j);
                    
                    var cellView = _viewsFactory.Create(pos, index);

                    var center = cellView.GetCenterCoords();
                    var estimatedPosition = new Vector2(center.x / _scaleFactor.x, center.y / _scaleFactor.y);
                    var cellNode = _nodesFactory.Create(estimatedPosition, index, _cellsConfig.DefaultCellType);

                    cellNode.CellTypeChanged += (type) => cellView.UpdateSprite(type.Sprite);
                    cellNode.CellTypeChanged += (_) => CellNodeChanged?.Invoke(cellNode);

                    _nodes[i, j] = cellNode;
                    _views[i, j] = cellView;
                }
            }
        }

        public CellView GetViewByIndex(int i, int j)
        {
            if (_views.IsWithinBounds(i, j))
                return _views[i, j];

            return null;
        }

        public override IEnumerable<ILink<CellNode>> GetLinksForNode(CellNode node)
        {
            var index = node.Index;
            var neighbours = _neighboursProvider.GetNeighbours(index.x, index.y, _nodes);

            return _linksProvider.GetLinks(node, neighbours);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector3 localPos = transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);

            int x = Mathf.FloorToInt(localPos.x / _grid.cellSize.x);
            int y = Mathf.FloorToInt(localPos.y / _grid.cellSize.y);

            if (_views[x, y] != null)
            {
                //Debug.Log($"Cell clicked: {x}, {y}");
                CellClicked?.Invoke(_views[x, y], eventData.button);
            }
        }

        private void OnDrawGizmos()
        {/*
            Gizmos.color = Color.cyan;

            foreach (var node in _nodes)
            {
                foreach (var l in node.GetLinks())
                {
                    Gizmos.DrawLine(l.From.GetCenterCoords(), l.To.GetCenterCoords()); //todo
                }
            }*/
        }
    }
}