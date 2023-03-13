using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Algorithm;
using Cells;
using Links;

namespace Demo
{
    public class GridField : AbstractField
    {
        [SerializeField]
        private Grid _grid = default;
        [SerializeField]
        private CellView _nodePrefab = default;
        [SerializeField]
        private Vector2Int _size = new Vector2Int(10, 10);
        [SerializeField]
        private bool _doCentering = true;
        [SerializeField]
        private float _timeDelay = 0.1f;

        private CellView[,] _gridNodes;
        private List<CellView> _allNodes;


        void Awake()
        {
            ModeChanged += Run;
            ModeChanged += ClearPath;
        }

        public override void Initialize()
        {
            _gridNodes = new CellView[_size.x, _size.y];
            _allNodes = new List<CellView>(_size.x * _size.y);

            var cellSize = _grid.cellSize;
            var newScale = cellSize / _nodePrefab.GetSize();
                        
            CreateNodesForGrid();
            DoCentering();


            void CreateNodesForGrid()
            {
                for (int i = 0; i < _gridNodes.GetLength(0); i++)
                {
                    for (int j = 0; j < _gridNodes.GetLength(1); j++)
                    {
                        var node = GameObject.Instantiate(_nodePrefab, new Vector3(cellSize.x * i, cellSize.y * j, 0), Quaternion.identity, this.transform);

                        node.Init(this, new Vector2Int(i, j));
                        node.SetScale(newScale);
                        node.name = $"Cell {i},{j}";

                        _gridNodes[i, j] = node;
                        _allNodes.Add(node);
                    }
                }
            }

            void DoCentering()
            {
                if (_doCentering)
                    this.transform.position -= 0.5f * Vector3.Scale(cellSize, new Vector3(_size.x, _size.y, 0));

            }
        }

        private void CreateLinksForNodes() //note: no diagonal, change if needed
        {
            const float WEIGHT = 1.0f;
            ILink link;

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
                    if (_gridNodes[i, j].CellState == CellState.Blocked)
                        return;

                    link = new Link(node, _gridNodes[i, j], weight);
                    node.Links.Add(link);
                }
            }
        }

        private void Run(FieldMode mode)
        {
            //todo
            if (mode == FieldMode.Launch)
                //StartCoroutine(CalculateWay());
                CalculateWay();
        }

        private void ClearPath(FieldMode mode)
        {
            if (mode != FieldMode.Launch)
                foreach (var cellView in _allNodes)
                    if (cellView.CellState == CellState.Way)
                        cellView.ChangeState(CellState.Normal);
        }

        public static float EstimateCost(INode node1, INode node2)
        {
            var p1 = node1.GetCenter();
            var p2 = node2.GetCenter();

            //return Vector2.SqrMagnitude(p2 - p1);
            return Mathf.Abs(p2.x - p1.x) + Math.Abs(p2.y - p1.y);
        }

        public void CalculateWay()
        {
            CreateLinksForNodes();
            Debug.Log("Path calculating started...");

            var cameFrom = new Dictionary<INode, INode>();
            var costSoFar = new Dictionary<INode, float>();

            var needToCheck = new PriorityQueue<INode>();
            needToCheck.Enqueue(StartNode, 0);

            cameFrom[StartNode] = StartNode;
            costSoFar[StartNode] = 0;

            while (needToCheck.Count > 0)
            {
                //yield return new WaitForSeconds(_timeDelay);

                var current = needToCheck.Dequeue();

                if (current == FinishNode)
                {
                    break;
                }

                foreach (var link in current.Links)
                {
                    var newCost = costSoFar[current] + link.Cost;
                    if (!costSoFar.ContainsKey(link.To) || newCost < costSoFar[link.To])
                    {
                        costSoFar[link.To] = newCost;
                        var priority = newCost + EstimateCost(link.To, FinishNode);
                        needToCheck.Enqueue(link.To, priority);
                        cameFrom[link.To] = current;
                    }
                }
            }
            Debug.Log("Path calculating finished...");

            //foreach (var n in cameFrom)            
            //    Debug.Log($"{n.Key} {n.Value}");
            

            var node = FinishNode;
            while (true)
            {                
                Debug.Log($"{node}");

                var cell = node as CellView;
                if (cell.CellState != CellState.Start && cell.CellState != CellState.Finish)
                    cell.ChangeState(CellState.Way);

                if (!cameFrom.ContainsKey(node) || node == cameFrom[node])
                    break;

                node = cameFrom[node];                
            }            
        }
    }
}