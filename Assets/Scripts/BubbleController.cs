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
    public BubbleType BubbleType { get => bubbleType; set => bubbleType = value; }

    [SerializeField] private List<int> bubblesLeft = new();
    [SerializeField] private GameObject bubbleObject;
    [SerializeField] private GameObject moleObject;

    private int currentTypeIndex = -1;

    private MoleController moleController;

    [SerializeField] private List<Color> bubbleColor;
    private bool canExplode;

    //Behaviour
    private Vector2 mousePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleType = BubbleType.ForwardBubble;

        currentTypeIndex = 0;
        bubbleObject.GetComponent<Renderer>().material.color = bubbleColor[currentTypeIndex];

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

    public void ChangeBubble(BubbleType newBubbleType)
    {
        switch (newBubbleType)
        {
            case BubbleType.ForwardBubble:
                currentTypeIndex = 0;

                break;
            case BubbleType.StopBubble:
                currentTypeIndex = 1;
                break;
            case BubbleType.FastBubble:
                currentTypeIndex = 2;
                break;
        }

        bubbleObject.GetComponent<Renderer>().material.color = bubbleColor[currentTypeIndex];
        bubbleType = newBubbleType;
    }
}
