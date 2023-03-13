using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorithm;
using Links;

namespace Demo
{
    //todo
    public class NodeView : MonoBehaviour, INode
    {
        private List<ILink> _links = new List<ILink>();

        public List<ILink> Links => _links;

        public Vector3 GetCenter()
        {
            throw new System.NotImplementedException();
        }        
    }
}
