using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardIgnorer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public static bool isHoveringCard;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoveringCard = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHoveringCard = false;
    }
}
