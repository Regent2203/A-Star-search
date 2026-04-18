using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Nodes;
using Nodes.Cells;
using Links;
using Zenject;
using Core.SearchAlgorithms;
using Nodes.Cells.CellStates;

namespace Fields
{
    public class CellGridField : AbstractGridField<Cell>
    {
        [SerializeField]
        private Cell _cellViewPrefab;

        protected override IView _nodePrefab => _cellViewPrefab;

        private IInstantiator _instantiator;


        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        protected override void Awake()
        {
            base.Awake();

            //recreates links each time after we finished setting obstacles
            /*
            ModeChangedPrevious += (prevMode) =>
            {
                if (prevMode == DrawMode.SelectObstacles)
                    CreateLinksForNodes();
            };*/

            CreateNodes();
        }

        private void CreateNodes()
        {
            for (int i = 0; i < _gridNodes.GetLength(0); i++)
            {
                for (int j = 0; j < _gridNodes.GetLength(1); j++)
                {
                    var node = _instantiator.InstantiatePrefabForComponent<Cell>(
                        _cellViewPrefab,
                        transform.position + new Vector3(_cellSize.x * i, _cellSize.y * j, 0) + new Vector3(_grid.cellGap.x * i, _grid.cellGap.y * j),
                        Quaternion.identity, transform);
                    node.Init(new Vector2Int(i, j), _scaleFactor);
                    _gridNodes[i, j] = node;
                }
            }

            CreateLinksForNodes();
        }

        private void CreateLinksForNodes() //note: no diagonal, change if needed
        {
            const float WEIGHT = 1.0f;

            for (int i = 0; i < _gridNodes.GetLength(0); i++)
            {
                for (int j = 0; j < _gridNodes.GetLength(1); j++)
                {
                    _gridNodes[i, j].Links.Clear();

                    TryCreateLink(i - 1, j, _gridNodes[i, j], WEIGHT);
                    TryCreateLink(i, j + 1, _gridNodes[i, j], WEIGHT);
                    TryCreateLink(i + 1, j, _gridNodes[i, j], WEIGHT);
                    TryCreateLink(i, j - 1, _gridNodes[i, j], WEIGHT);
                }
            }
            

            void TryCreateLink(int i, int j, INode node, float weight)
            {
                if (_gridNodes.IndexExists(i) && _gridNodes.IndexExists(j))
                {
                    if (_gridNodes[i, j].IsObstacle)
                        return;

                    var link = new Link(node, _gridNodes[i, j], weight);
                    node.Links.Add(link);
                }
            }
        }

        public override float EstimateCost(INode node1, INode node2)
        {
            var p1 = node1.GetCenterCoords();
            var p2 = node2.GetCenterCoords();

            return Mathf.Abs(p2.x - p1.x) / _scaleFactor.x + Math.Abs(p2.y - p1.y) / _scaleFactor.y;
        }

        public override void ShowPath(bool show, IList<INode> path)
        {
            if (path is null)
                return;

            int from = 1;
            int to = path.Count - 1;

            for (int i = from; i < to; i++)
            {
                path[i].DrawPath(show);
            }
        }
    }
}