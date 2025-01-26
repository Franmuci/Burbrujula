using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [SerializeField] private GameObject cardForwardBubble;
    [SerializeField] private GameObject cardStopBubble;
    [SerializeField] private GameObject cardFastBubble;

    private Button cardForwardButton;
    private Button cardStopButton;
    private Button cardFastButton;

    private bool canClickForwardBubble;
    private bool canClickStopBubble;
    private bool canClickFastBubble;

    [SerializeField] private GameObject bubble;
    private BubbleController bubbleController;


    private void Start()
    {
        Instance = this;

        canClickFastBubble = true;
        canClickForwardBubble = true;
        canClickStopBubble = true;

        cardForwardButton = cardForwardBubble.GetComponent<Button>();
        cardStopButton = cardStopBubble.GetComponent<Button>();
        cardFastButton = cardFastBubble.GetComponent<Button>();

        bubbleController = bubble.GetComponent<BubbleController>();

        cardForwardButton.onClick.AddListener(OnCardForwardClicked);
        cardStopButton.onClick.AddListener(OnCardStopClicked);
        cardFastButton.onClick.AddListener(OnCardFastClicked);
    }

    private void OnCardForwardClicked()
    {
        if (canClickForwardBubble)
        {
            print("aiuda");
            bubbleController.ChangeBubble(BubbleType.ForwardBubble);
        }
    }

    private void OnCardStopClicked()
    {
        if (canClickStopBubble)
        {
            print("socorro");
            bubbleController.ChangeBubble(BubbleType.StopBubble);
        }
    }

    private void OnCardFastClicked()
    {
        if (canClickFastBubble)
        {
            print("aiudaaaaa");
            bubbleController.ChangeBubble(BubbleType.FastBubble);
        }
    }
}
