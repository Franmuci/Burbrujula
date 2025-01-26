using NUnit.Framework;
using System.Collections;
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

    [SerializeField] public List<int> bubblesLeft = new();
    [SerializeField] private GameObject bubbleObjectPrefab;
    [SerializeField] private GameObject moleObject;
    [SerializeField] private Transform bubblePlaceholder;

    private int currentTypeIndex = -1;

    private MoleController moleController;

    [SerializeField] private List<Color> bubbleColor;
    private bool canExplode;

    //Behaviour
    private Vector2 mousePosition;

    private Animator bubbleAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleType = BubbleType.ForwardBubble;

        currentTypeIndex = 0;

        moleController = moleObject.GetComponent<MoleController>();
    }

    // Update is called once per frame
    void Update()
    {
        //MoveWithMouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;


        //Change mole movement
        if (Input.GetMouseButtonDown(0) && !CardIgnorer.isHoveringCard)
        {
            moleController.RespondToClick(bubbleType);
            StartCoroutine(MakeBubblePop());
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
        bubbleType = newBubbleType;
    }

    public IEnumerator MakeBubblePop()
    {
        if(bubblesLeft[currentTypeIndex] > 0)
        {
            bubblesLeft[currentTypeIndex]--;
        }
            

        bool canClick = false;

            switch (currentTypeIndex)
            {
                case 0:
                    canClick = GameUIManager.Instance.canClickForwardBubble;
                    GameUIManager.Instance.canClickForwardBubble = bubblesLeft[currentTypeIndex] > 0; break;
                case 1:
                    canClick = GameUIManager.Instance.canClickStopBubble;
                    GameUIManager.Instance.canClickStopBubble = bubblesLeft[currentTypeIndex] > 0; break;
                case 2:
                    canClick = GameUIManager.Instance.canClickFastBubble;
                    GameUIManager.Instance.canClickFastBubble = bubblesLeft[currentTypeIndex] > 0; break;
            }

        if (canClick)
        {
            GameObject bubbleObject = Instantiate(bubbleObjectPrefab, bubblePlaceholder);
            bubbleAnimator = bubbleObject.GetComponent<Animator>();
            bubbleObject.GetComponent<Renderer>().material.color = bubbleColor[currentTypeIndex];
            bubbleAnimator.SetBool("isPopping", true);
            yield return new WaitForSeconds(1f);

            bubbleAnimator.SetBool("isPopping", false);
        }

    }
    
}
