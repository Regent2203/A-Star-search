using Core.Inputs;
using Core.Nodes;
using UnityEngine.EventSystems;

namespace Core.Signals
{
    public readonly struct NodeClickedSignal<T> where T : class, INode
    {
        public T Node { get; }
        public PointerEventData.InputButton Button { get; }
        public InputSnapshot Input { get; }


        public NodeClickedSignal(T node, PointerEventData.InputButton btn, InputSnapshot input)
        {
            Node = node;
            Button = btn;
            Input = input;
        }
    }
}