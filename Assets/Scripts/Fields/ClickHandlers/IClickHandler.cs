using UnityEngine.EventSystems;

namespace ThisProject.Fields.ClickHandlers
{
    public interface IClickHandler
    {
        public void ProcessClick(PointerEventData eventData);
    }
}
