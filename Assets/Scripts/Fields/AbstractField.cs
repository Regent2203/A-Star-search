using System;
using UnityEngine;
using Nodes;
using System.Collections.Generic;

namespace Fields
{
    public abstract class AbstractField : MonoBehaviour
    {
        protected INode _startNode;
        protected INode _finishNode;
        protected IList<INode> _path;

        public INode StartNode => _startNode;
        public INode FinishNode => _finishNode;

        public event Action StartFinishReady;


        public void SetStartNode(INode node)
        {
            var oldNode = _startNode;
            _startNode = null;
            oldNode?.ResetState();
            _startNode = node;
        }
        public void SetFinishNode(INode node)
        {
            var oldNode = _finishNode;
            _finishNode = null;
            oldNode?.ResetState();
            _finishNode = node;
        }

        public abstract float EstimateCost(INode node1, INode node2);

        public void SetPath(IList<INode> path)
        {
            _path = path; 
        }

        public void ShowPath(bool show)
        {
            if (_path == null)
                return;

            int from = 1;
            int to = _path.Count - 1;

            for (int i = from; i < to; i++)
            {
                _path[i].DrawPath(show);
            }
        }

        protected void CheckStartFinishReady()
        {
            if (_startNode != null && _finishNode != null)
                StartFinishReady?.Invoke();
        }
    }
}