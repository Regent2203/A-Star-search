using Core.Implementations.VisualLinks;
using Core.Links.Providers;
using Core.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core
{
    public class VisualLinksCreator<T> : MonoBehaviour where T: class, INode
    {
        private VisualLink<INode> _prefab;

        private enum Mode { None, CreateLink, RemoveLink }

        private KeyCode _linkingKey;
        private Mode _mode;
        private INode _firstNode;

        private StoredLinksProvider<T> _linksProvider;


        [Inject]
        public void Construct(StoredLinksProvider<T> linksProvider)
        {
            _linksProvider = linksProvider;
        }
        
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
