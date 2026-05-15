using Core.Links.Providers;
using System;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellsGridFieldGenerator
    {
        private readonly RuntimeLinksProvider<CellNode> _linksProvider;
        private readonly CellViewFactory _viewsFactory;
        private readonly CellNodeFactory _nodesFactory;
        private readonly CellsConfig _config;

        public CellsGridFieldGenerator(RuntimeLinksProvider<CellNode> linksProvider, CellViewFactory viewFactory, CellNodeFactory nodeFactory, CellsConfig config)
        {
            _linksProvider = linksProvider;
            _viewsFactory = viewFactory;
            _nodesFactory = nodeFactory;
            _config = config;
        }

        public void PopulateField(CellsGridField field, Transform container, Vector2 scale, Grid grid, Action<CellNode, CellType> callback)
        {
            var size = field.CellsNumber;
            var views = new CellView[size.x, size.y];
            var nodes = new CellNode[size.x, size.y];

            _viewsFactory.SetConfiguration(scale, container);
            _nodesFactory.SetConfiguration(callback);

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var worldPos = grid.CellToWorld(new Vector3Int(i, j, 0));
                    var index = new Vector2Int(i, j);

                    var view = _viewsFactory.Create(worldPos, index);
                    var node = _nodesFactory.Create(view.GetCenterCoords() / scale, index, _config.DefaultCellType);

                    views[i, j] = view;
                    nodes[i, j] = node;
                }
            }

            //todo unsubscribe if ever needed
            field.CellNodeTypeChanged += (node, type) =>
            {
                var view = field.GetViewForNode(node);
                view.UpdateSprite(type.Sprite);
            };

            _linksProvider.InitGrid(nodes);
            field.SetFieldData(nodes, views);
        }
    }
}