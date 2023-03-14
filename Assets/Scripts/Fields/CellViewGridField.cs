using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Nodes;
using Nodes.Cells;
using Links;

namespace Fields
{
    /// <summary>
    /// Uses CellView in grid's cells.
    /// </summary>
    public class CellViewGridField : AbstractGridField
    {
        [SerializeField]
        private CellView _cellViewPrefab = default;

        protected override IView _nodePrefab => _cellViewPrefab;


        protected override void Awake()
        {
            base.Awake();

            //recreates links each time after we finished setting obstacles
            ModeChangedPrevious += (prevMode) =>
            {
                if (prevMode == FieldMode.SelectObstacles)
                    CreateLinksForNodes();
            };
        }

        public override void Initialize()
        {
            base.Initialize();

            CreateNodes();
        }

        public override float EstimateCost(INode node1, INode node2)
        {
            var p1 = node1.GetCenter();
            var p2 = node2.GetCenter();

            return Mathf.Abs(p2.x - p1.x) / _scaleMod.x + Math.Abs(p2.y - p1.y) / _scaleMod.y;
        }

        private void CreateNodes()
        {
            for (int i = 0; i < _gridNodes.GetLength(0); i++)
            {
                for (int j = 0; j < _gridNodes.GetLength(1); j++)
                {
                    var node = GameObject.Instantiate(_cellViewPrefab, this.transform.position + new Vector3(_cellSize.x * i, _cellSize.y * j, 0), Quaternion.identity, this.transform);

                    node.Init(this, new Vector2Int(i, j));
                    node.SetScale(_scaleMod);
                    node.name = $"Cell {i},{j}";

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
    }
}