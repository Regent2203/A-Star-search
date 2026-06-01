using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    public class CellsFieldGenerator
    {
        private CellsField _field;
        private Vector2 _scaleFactor;

        private readonly CellViewFactory _viewsFactory;
        private readonly CellNodeFactory _nodesFactory;

        public CellsFieldGenerator(CellViewFactory viewFactory, CellNodeFactory nodeFactory)
        {
            _viewsFactory = viewFactory;
            _nodesFactory = nodeFactory;
        }

        public void SetConfiguration(CellsField field, Transform container, Vector2 scaleFactor)
        {
            _field = field;
            _scaleFactor = scaleFactor;

            _viewsFactory.SetConfiguration(container);
        }

        public void PopulateField(Vector2Int size, CellType cellType)
        {
            var views = new CellView[size.x, size.y];
            var nodes = new CellNode[size.x, size.y];

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    var index = new Vector2Int(x, y);

                    var localX = x - (size.x / 2f);
                    var localY = y - (size.y / 2f);
                    var localPos = new Vector3(localX * _field.Grid.cellSize.x, localY * _field.Grid.cellSize.y, 0);

                    var viewPos = _field.Grid.transform.TransformPoint(localPos);
                    var view = _viewsFactory.Create(index, viewPos, _scaleFactor);

                    var nodePos = index;
                    var node = _nodesFactory.Create(index, nodePos, cellType);

                    views[x, y] = view;
                    nodes[x, y] = node;
                }
            }

            _field.SetFieldData(nodes, views, size);
        }
    }
}