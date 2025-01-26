using TMPro;
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

    private TMP_Text forwardBubbleQuantityText;
    private TMP_Text stopBubbleQuantityText;
    private TMP_Text fastBubbleQuantityText;

    public bool canClickForwardBubble;
    public bool canClickStopBubble;
    public bool canClickFastBubble;

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

        forwardBubbleQuantityText = cardForwardBubble.GetComponentInChildren<TMP_Text>();
        stopBubbleQuantityText = cardStopBubble.GetComponentInChildren<TMP_Text>();
        fastBubbleQuantityText = cardFastBubble.GetComponentInChildren<TMP_Text>();

        bubbleController = bubble.GetComponent<BubbleController>();

        cardForwardButton.onClick.AddListener(OnCardForwardClicked);
        cardStopButton.onClick.AddListener(OnCardStopClicked);
        cardFastButton.onClick.AddListener(OnCardFastClicked);
    }

    private void Update()
    {
        forwardBubbleQuantityText.text = bubbleController.bubblesLeft[0].ToString();
        stopBubbleQuantityText.text = bubbleController.bubblesLeft[1].ToString();
        fastBubbleQuantityText.text = bubbleController.bubblesLeft[2].ToString();
    }

    private void OnCardForwardClicked()
    {
        if (canClickForwardBubble)
        {
            bubbleController.ChangeBubble(BubbleType.ForwardBubble);
            
            AudioManager.Instance.PlayOnce(3, TYPEOFAUDIO.SFX);
        }

    }

    private void OnCardStopClicked()
    {
        if (canClickStopBubble)
        {
            bubbleController.ChangeBubble(BubbleType.StopBubble);
            
            AudioManager.Instance.PlayOnce(3, TYPEOFAUDIO.SFX);
        }
    }

    private void OnCardFastClicked()
    {
        if (canClickFastBubble)
        {
            bubbleController.ChangeBubble(BubbleType.FastBubble);
            
            AudioManager.Instance.PlayOnce(3, TYPEOFAUDIO.SFX);
        }
    }
}
