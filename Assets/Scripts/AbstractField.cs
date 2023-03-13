using Algorithm;
using System;
using System.Collections;
using UnityEngine;

namespace Demo
{
    public abstract class AbstractField : MonoBehaviour //interface IField
    {
        public abstract void Initialize();
        public FieldMode Mode { get; protected set; }
        public INode StartNode { get; protected set; }
        public INode FinishNode { get; protected set; }

        public event Action<FieldMode> ModeChanged;


        public void SetMode(FieldMode mode)
        {
            Mode = mode;
            ModeChanged?.Invoke(mode);
        }

        public void SetStartNode(INode node)
        {
            StartNode = node;
        }
        public void SetFinishNode(INode node)
        {
            FinishNode = node;
        }
    }
}