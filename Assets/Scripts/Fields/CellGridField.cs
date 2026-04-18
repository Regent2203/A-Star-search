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
using System.IO;

namespace Fields
{
    public class CellGridField : AbstractGridField<Cell>
    {
        [SerializeField]
        private Cell _cellViewPrefab;

        protected override IView _nodePrefab => _cellViewPrefab;
        private CellGridFieldEditor _fieldEditor;
        private IInstantiator _instantiator;


        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;

            _fieldEditor = _instantiator.Instantiate<CellGridFieldEditor>();
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


            //clears path when we start changing setup
            /*
            ModeChangedPrevious += (prevMode) =>
            {
                if (prevMode == DrawMode.Launch)
                    ShowPath(false, _path, true);
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
                        transform.position + new Vector3(_grid.cellSize.x * i, _grid.cellSize.y * j, 0) + new Vector3(_grid.cellGap.x * i, _grid.cellGap.y * j),
                        Quaternion.identity, transform);

                    node.Init(new Vector2Int(i, j), _scaleFactor);
                    node.CellClicked += _fieldEditor.ChangeCell;
                    node.CellStateChanged += OnCellChanged;

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

        private void OnCellChanged(Cell cell, CellState state)
        {
            if (state is CellStateWay || cell.CellState is CellStateWay) //todo: fix this
                return;

            if (state is CellStateStart)
                SetStartNode(cell);
            else if (state is CellStateFinish)
                SetFinishNode(cell);
            else
            {
                if (_startNode == cell)
                    SetStartNode(null);
                if (_finishNode == cell)
                    SetFinishNode(null);
            }

            //ShowPath(false);
            CheckStartFinishReady();
        }

        public override float EstimateCost(INode node1, INode node2)
        {
            var p1 = node1.GetCenterCoords();
            var p2 = node2.GetCenterCoords();

            return Mathf.Abs(p2.x - p1.x) / _scaleFactor.x + Math.Abs(p2.y - p1.y) / _scaleFactor.y;
        }
    }
}