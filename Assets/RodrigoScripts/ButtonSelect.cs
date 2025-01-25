using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    public Image burbuja;
    public static Image burbujaPaFuera;
    [SerializeField] GameObject currentButton;
    [SerializeField] List<GameObject> otherButtons;

    [SerializeField] List<GameObject> allCanvas;


    private void Awake()
    {
        burbujaPaFuera = burbuja;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentButton.GetComponentInChildren<TMP_Text>().color = Color.white;
        otherButtons.Clear();
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("Buttons"))
        {
            otherButtons.Add(button);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentButton != EventSystem.current.currentSelectedGameObject)
        {
            currentButton = EventSystem.current.currentSelectedGameObject != null ? EventSystem.current.currentSelectedGameObject : currentButton;
            burbuja.transform.position = new Vector3(burbuja.transform.position.x, currentButton.transform.position.y, 0);




            for (int i = 0; i < otherButtons.Count; i++)
            {
                if (otherButtons[i] != currentButton)
                {
                    otherButtons[i].GetComponentInChildren<TMP_Text>().color = Color.black;
                }
                else
                {
                    otherButtons[i].GetComponentInChildren<TMP_Text>().color = Color.white;

                }
            }
        }
        

    }


    


    public void CanvasChange(GameObject canvas)
    {
        foreach (GameObject can in allCanvas)
        {
            if (can == canvas)
            {
                can.SetActive(true);
               
            }
            else
            {
                can.SetActive(false);
            }
        }

        otherButtons.Clear();
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("Buttons"))
        {
            otherButtons.Add(button);
            button.GetComponentInChildren<TMP_Text>().color = Color.black;
        }

        EventSystem.current.firstSelectedGameObject = otherButtons[0];
        EventSystem.current.SetSelectedGameObject(otherButtons[0]);
        currentButton = otherButtons[0];
        currentButton.GetComponentInChildren<TMP_Text>().color = Color.white;
        burbuja.transform.position = new Vector3(burbuja.transform.position.x, currentButton.transform.position.y, 0);


    }

    public void Quit()
    {
        Application.Quit();
    }
}
