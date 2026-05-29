using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.Cells
{
    public class CellNodeFactory
    {
        private readonly IInstantiator _instantiator;


        public CellNodeFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public CellNode Create(Vector2Int index, Vector2 nodePosition, CellType cellType)
        {
            var cellNode = _instantiator.Instantiate<CellNode>(new object[] { index, nodePosition, cellType });

            return cellNode;
        }
    }
}