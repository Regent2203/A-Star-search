using ThisProject.Links.Providers;
using System;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    public class CellsGridFieldGenerator
    {
        private CellsGridField _field;
        private Vector2 _scaleFactor;

        private readonly CellViewFactory _viewsFactory;
        private readonly CellNodeFactory _nodesFactory;
        private readonly CellsConfig _config;

        public CellsGridFieldGenerator(CellViewFactory viewFactory, CellNodeFactory nodeFactory, CellsConfig config)
        {
            _viewsFactory = viewFactory;
            _nodesFactory = nodeFactory;
            _config = config;
        }

        public void SetConfiguration(CellsGridField field, Transform container, Vector2 scaleFactor,
            Action<Vector2> nodePositionChangedCallback, Action<CellNode, CellType> nodeTypeChangedCallback)
        {
            _field = field;
            _scaleFactor = scaleFactor;

            _viewsFactory.SetConfiguration(container);
            _nodesFactory.SetConfiguration(nodePositionChangedCallback, nodeTypeChangedCallback);
        }

        public void PopulateField()
        {
            var size = _field.CellsNumber;
            var views = new CellView[size.x, size.y];
            var nodes = new CellNode[size.x, size.y];

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var worldPos = _field.Grid.CellToWorld(new Vector3Int(i, j, 0));
                    var index = new Vector2Int(i, j);

                    var view = _viewsFactory.Create(index, worldPos, _scaleFactor);
                    var node = _nodesFactory.Create(index, view.GetCenterCoords() / _scaleFactor, _config.DefaultCellType);

                    views[i, j] = view;
                    nodes[i, j] = node;
                }
            }

            _field.SetFieldData(nodes, views);
        }
    }
}