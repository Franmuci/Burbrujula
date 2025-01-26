using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image burbuja;

    private void Start()
    {
        burbuja = ButtonSelect.burbujaPaFuera;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<TMP_Text>().color = Color.white;
        burbuja.transform.position = new Vector3(burbuja.transform.position.x, gameObject.transform.position.y, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<TMP_Text>().color = Color.black;
    }

}
