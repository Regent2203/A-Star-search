using UnityEngine.EventSystems;

namespace Core.Fields
{
    public interface IClickHandler
    {
        public void ProcessClick(PointerEventData eventData);
    }
}
