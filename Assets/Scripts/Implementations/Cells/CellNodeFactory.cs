using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellNodeFactory
    {
        private readonly IInstantiator _instantiator;

        public CellNodeFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public CellNode Create(Vector2 position, Vector2Int index, CellType cellType)
        {
            var cellNode = _instantiator.Instantiate<CellNode>(new object[] { position, index, cellType });

            return cellNode;
        }
    }
}