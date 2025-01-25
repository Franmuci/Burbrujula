using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class ButtonSelect : MonoBehaviour
{
    public Image Burbuja;
    [SerializeField] GameObject currentButton;
    [SerializeField] List<GameObject> otherButtons;

    private void Awake()
    {
        Burbuja.transform.position = new Vector3(Burbuja.transform.position.x, currentButton.transform.position.y, 0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentButton = EventSystem.current.currentSelectedGameObject;
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("Buttons"))
        {
            otherButtons.Add(button);
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        currentButton = EventSystem.current.currentSelectedGameObject;
        Burbuja.transform.position = new Vector3(Burbuja.transform.position.x, currentButton.transform.position.y, 0);
        Debug.Log(currentButton.gameObject);

        for (int i = 0; i < otherButtons.Count; i++)
        {
            if (otherButtons[i] != currentButton) 
            {
                otherButtons[i].gameObject.GetComponentInChildren<TMP_Text>().color = Color.black;
            }
            else
            {
                otherButtons[i].gameObject.GetComponentInChildren<TMP_Text>().color = Color.white;

            }
        }
        
    }
}
