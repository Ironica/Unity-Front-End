using UnityEngine;
using UnityEngine.EventSystems;

namespace Resources.Scripts
{
    public class PanelBlockMouseActionsBehavoir: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Global.mouseActivated = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Global.mouseActivated = true;
        }
    }
}