using UnityEngine.EventSystems;

namespace ThisProject.Fields
{
    public interface IClickHandler
    {
        public void ProcessClick(PointerEventData eventData);
    }
}
