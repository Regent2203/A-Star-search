using Core.Implementations.Cells;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellsGridGenerator
    {
        private readonly CellViewFactory _viewsFactory;
        private readonly CellNodeFactory _nodesFactory;
        private readonly CellsConfig _config;

        public CellsGridGenerator(CellViewFactory viewFactory, CellNodeFactory nodeFactory, CellsConfig config)
        {
            _viewsFactory = viewFactory;
            _nodesFactory = nodeFactory;
            _config = config;
        }

        public void PopulateField(CellsGridField field, Transform container, Vector2 scale, Grid grid)
        {
            var size = field.CellsNumber;
            var nodes = new CellNode[size.x, size.y];
            var views = new CellView[size.x, size.y];

            _viewsFactory.SetConfiguration(scale, container);

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var worldPos = grid.CellToWorld(new Vector3Int(i, j, 0));
                    var index = new Vector2Int(i, j);

                    var view = _viewsFactory.Create(worldPos, index);
                    var node = _nodesFactory.Create(view.GetCenterCoords() / scale, index, _config.DefaultCellType);

                    //todo unsubscribe if ever needed
                    node.CellTypeChanged += (type) => view.UpdateSprite(type.Sprite);
                    node.CellTypeChanged += (_) => field.NotifyNodeChanged(node);

                    nodes[i, j] = node;
                    views[i, j] = view;
                }
            }

            field.SetData(nodes, views);
        }
    }
}