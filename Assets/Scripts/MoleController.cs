using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 5.0f;
    [SerializeField] private float fastSpeed = 10.0f;

    [SerializeField] private float animationNormalSpeed = 1.0f;
    [SerializeField] private float animationFastSpeed = 2.0f;

    private float currentSpeed;
    private float currentAnimationSpeed;

    private Vector3 clickPosition;

    public GameObject lastCheckpoint;

    private Vector2 lastCheckpointPosition = new Vector2(-20,-0.5f);
    public Vector2 LastCheckpointPosition { get => lastCheckpointPosition; set => lastCheckpointPosition = value; }

    private bool isMoving;
    private bool isRotating;

    [SerializeField] private GameObject raycastOrigin;
    [SerializeField] private GameObject raycastOrigin2;
    [SerializeField] private GameObject raycastOrigin3;
    private LayerMask wallLayer;

    private Animator moleAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wallLayer = LayerMask.GetMask("Wall");

        moleAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateMole();
        MoveMole();

        if (IsWalled())
        {
            StopMoving();
        }
    }

    public void RespondToClick(BubbleType currentBubbleType)
    {
        if (!CardIgnorer.isHoveringCard && !TutorialBeh.isInTutorial)
        {
            switch (currentBubbleType)
            {
                case BubbleType.ForwardBubble:
                    if (GameUIManager.Instance.canClickForwardBubble)
                    {
                        currentAnimationSpeed = animationNormalSpeed;
                        MoveTowardsBubble(normalSpeed);
                    }
                    break;
                case BubbleType.StopBubble:
                    if (GameUIManager.Instance.canClickStopBubble)
                    {
                        StopMoving();
                    }
                    break;
                case BubbleType.FastBubble:
                    if (GameUIManager.Instance.canClickFastBubble)
                    {
                        currentAnimationSpeed = animationFastSpeed;
                        MoveTowardsBubble(fastSpeed);
                    }
                    break;
            }

            StartCoroutine(AudioManager.Instance.PlayOnceAwaited(4, 0.22f, TYPEOFAUDIO.SFX));
        }
        
    }

    private void MoveTowardsBubble(float speed)
    {
        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isRotating = true;
        currentSpeed = speed;

        isMoving = true;
    }


    private void StopMoving()
    {
        isMoving = false;
        moleAnimator.SetBool("isMoving", false);
        moleAnimator.speed = 1;

        AudioManager.Instance.StopAudio(2);
    }


    private void RotateMole()
    {
        if (isRotating)
        {
            //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = (new Vector3(clickPosition.x, clickPosition.y, 0) - transform.position).normalized;

            transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * 5);

            AudioManager.Instance.PlayLoop(2, TYPEOFAUDIO.SFX);

            if (Vector3.Distance(transform.up, direction) < 0.01f)
            {
                isRotating = false;
                AudioManager.Instance.StopAudio(2);
            }
        }
    }

    private void MoveMole()
    {
        if (isMoving && !isRotating)
        {
            moleAnimator.SetBool("isMoving", true);
            moleAnimator.speed = currentAnimationSpeed;

            transform.position = transform.position + transform.up * Time.deltaTime * currentSpeed;
            
            AudioManager.Instance.PlayLoop(2, TYPEOFAUDIO.SFX);
        }
    }

    private bool IsWalled()
    {
        Debug.DrawRay(raycastOrigin.transform.position, transform.up * 0.1f, Color.red);
        Debug.DrawRay(raycastOrigin2.transform.position, transform.up * 0.1f, Color.yellow);
        Debug.DrawRay(raycastOrigin3.transform.position, transform.up * 0.1f, Color.green);

        bool isRaycasting1 = Physics2D.Raycast(raycastOrigin.transform.position, transform.up, 0.2f, wallLayer);
        bool isRaycasting2 = Physics2D.Raycast(raycastOrigin2.transform.position, transform.up, 0.2f, wallLayer);
        bool isRaycasting3 = Physics2D.Raycast(raycastOrigin3.transform.position, transform.up, 0.2f, wallLayer);

        return isRaycasting1 || isRaycasting2 || isRaycasting3;
    }

    public void FallDownTheAbism()
    {
        isMoving = false;
        moleAnimator.SetBool("isFalling", true);
        moleAnimator.speed = 1;

        AudioManager.Instance.PlayOnce(6, TYPEOFAUDIO.SFX);

        StartCoroutine(StopFalling());
    }

    private IEnumerator StopFalling()
    {
        yield return new WaitForSeconds(1.5f);
        
        transform.position = lastCheckpointPosition;
        moleAnimator.SetBool("isFalling", false);

        ManageBubbles();
    }

    public void GetHurt()
    {
        isMoving = false;
        moleAnimator.SetBool("isHurting", true);
        moleAnimator.speed = 1;
        
        AudioManager.Instance.PlayOnce(5, TYPEOFAUDIO.SFX);

        StartCoroutine(StopHurting());
    }

    private IEnumerator StopHurting()
    {
        yield return new WaitForSeconds(1.5f);

        transform.position = lastCheckpointPosition;
        moleAnimator.SetBool("isHurting", false);

        ManageBubbles();
    }

    private void ManageBubbles()
    {
        if (lastCheckpoint != null)
        {
            lastCheckpoint.GetComponent<CheckpointController>().ChangeBubbleQuantity();
        }
        else
        {
            BubbleController bubbleController = GameObject.Find("Worm").GetComponent<BubbleController>();
            bubbleController.bubblesLeft.Clear();
            bubbleController.bubblesLeft.Add(8);
            bubbleController.bubblesLeft.Add(4);
            bubbleController.bubblesLeft.Add(4);
            GameUIManager.Instance.canClickStopBubble = true;
            GameUIManager.Instance.canClickForwardBubble = true;
            GameUIManager.Instance.canClickFastBubble = true;
        }
    }
}