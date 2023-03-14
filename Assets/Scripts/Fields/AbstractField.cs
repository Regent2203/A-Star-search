using System;
using UnityEngine;
using Nodes;
using System.Collections;
using System.Collections.Generic;

namespace Fields
{
    public abstract class AbstractField : MonoBehaviour //interface IField
    {
        public abstract void Initialize();
        public FieldMode Mode { get; protected set; }
        public INode StartNode { get; protected set; }
        public INode FinishNode { get; protected set; }

        public event Action<FieldMode> ModeChangedPrevious;
        public event Action<FieldMode> ModeChangedCurrent;

        protected IList<INode> _path;


        public void SetMode(FieldMode mode)
        {
            ModeChangedPrevious?.Invoke(Mode);
            Mode = mode;
            ModeChangedCurrent?.Invoke(Mode);
        }

        public void SetStartNode(INode node)
        {
            StartNode?.ResetState();
            StartNode = node;
        }
        public void SetFinishNode(INode node)
        {
            FinishNode?.ResetState();
            FinishNode = node;
        }

        public abstract float EstimateCost(INode node1, INode node2);

        public void ShowPath(bool show, IList<INode> path, bool ignoreStartFinish = true)
        {
            _path = path;

            if (path is null)
                return;

            int from = 0;
            int to = path.Count - 1;

            for (int i = from; i <= to; i++)
            {
                if (ignoreStartFinish)
                {
                    if (i == from || i == to)
                        continue;
                }

                if (show)
                    path[i].DrawPath();
                else
                    path[i].ResetState();
            }
        }
    }
}