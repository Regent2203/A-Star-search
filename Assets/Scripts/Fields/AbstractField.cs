using System;
using UnityEngine;
using Nodes;
using System.Collections;
using System.Collections.Generic;

namespace Fields
{
    public abstract class AbstractField : MonoBehaviour
    {
        protected INode _startNode;
        protected INode _finishNode;

        public INode StartNode => _startNode;
        public INode FinishNode => _finishNode;

        /*
        public void SetStartNode(INode node)
        {
            StartNode?.ResetState();
            StartNode = node;
        }
        public void SetFinishNode(INode node)
        {
            FinishNode?.ResetState();
            FinishNode = node;
        }*/

        public abstract float EstimateCost(INode node1, INode node2);
        public abstract void ShowPath(bool show, IList<INode> path);
    }
}