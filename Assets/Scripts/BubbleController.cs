using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum BubbleType
{
    ForwardBubble, StopBubble, FastBubble
}

public class BubbleController : MonoBehaviour
{

    //Depending on bubble type
    private BubbleType bubbleType;
    [SerializeField] private List<int> bubblesLeft = new();
    [SerializeField] private GameObject bubbleObject;
    [SerializeField] private GameObject moleObject;

    private MoleController moleController;

    [SerializeField] private List<Color> bubbleColor;
    private bool canExplode;

    //Behaviour
    private Vector2 mousePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleType = BubbleType.ForwardBubble;

        bubbleObject.GetComponent<Renderer>().material.color = bubbleColor[0];

        moleController = moleObject.GetComponent<MoleController>();
    }

    // Update is called once per frame
    void Update()
    {
        //MoveWithMouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;


        //Change mole movement
        if (Input.GetMouseButtonDown(0))
        {
            moleController.RespondToClick(bubbleType);
        }
    }
}
