using Core.Implementations;
using Core.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public class VisualLinksCreator
    {
        private VisualLink<INode> _visualLinkPrefab;

        private enum Mode { None, CreateLink, RemoveLink }

        private KeyCode _linkingKey;
        private Mode _mode;

        private INode _firstNode;


        public void TryUseNode(INode node)
        {

        }

        public void UseFirstNode(INode node, PointerEventData.InputButton btn)
        {
            if (!Input.GetKeyDown(_linkingKey)) 
                return;

            if (Input.GetMouseButtonDown(0)) //lmb
            {
                _mode = Mode.CreateLink;
                _firstNode = node;
            }
            else if (Input.GetMouseButtonDown(1)) //rmb
            {
                _mode = Mode.RemoveLink;
                _firstNode= node;
            }
        }

        public void UseSecondNode(INode node)
        {
            if (!Input.GetKeyDown(_linkingKey))
                return;

            if (Input.GetMouseButtonDown(0)) //lmb
            {
                if (_mode == Mode.CreateLink)
                {
                    _firstNode = node;
                    //create
                }
            }
            else if (Input.GetMouseButtonDown(1)) //rmb
            {
                if (_mode == Mode.RemoveLink)
                {
                    //_firstNode = node;
                    //remove
                }
            }
            //todo add key cancel
            Cancel();
        }

        public void Cancel()
        {
            _mode = Mode.None; 
            _firstNode = null;
        }
    }
}
