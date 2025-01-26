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

    private Vector2 lastCheckpointPosition = new Vector2(-20,-0.5f); // cambiar a codigo de checkpoint

    private bool isMoving;
    private bool isRotating;

    [SerializeField] private GameObject raycastOrigin;
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
        if (!CardIgnorer.isHoveringCard)
        {
            switch (currentBubbleType)
            {
                case BubbleType.ForwardBubble:
                    currentAnimationSpeed = animationNormalSpeed;
                    MoveTowardsBubble(normalSpeed);
                    break;
                case BubbleType.StopBubble:
                    StopMoving();
                    break;
                case BubbleType.FastBubble:
                    currentAnimationSpeed = animationFastSpeed;
                    MoveTowardsBubble(fastSpeed);
                    break;
            }
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
    }


    private void RotateMole()
    {
        if (isRotating)
        {
            //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = (new Vector3(clickPosition.x, clickPosition.y, 0) - transform.position).normalized;

            transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * 5);

            if (Vector3.Distance(transform.up, direction) < 0.01f)
            {
                isRotating = false;
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
        }
    }

    private bool IsWalled()
    {
        Debug.DrawRay(raycastOrigin.transform.position, transform.up * 0.1f, Color.red);
        return Physics2D.Raycast(raycastOrigin.transform.position, transform.up, 0.1f, wallLayer);
    }

    public void FallDownTheAbism()
    {
        isMoving = false;
        moleAnimator.SetBool("isFalling", true);
        moleAnimator.speed = 1;

        StartCoroutine(StopFalling());
    }

    private IEnumerator StopFalling()
    {
        yield return new WaitForSeconds(1.5f);
        
        transform.position = lastCheckpointPosition;
        moleAnimator.SetBool("isFalling", false);
    }

    public void GetHurt()
    {
        isMoving = false;
        moleAnimator.SetBool("isHurting", true);
        moleAnimator.speed = 1;

        StartCoroutine(StopHurting());
    }

    private IEnumerator StopHurting()
    {
        yield return new WaitForSeconds(1.5f);

        transform.position = lastCheckpointPosition;
        moleAnimator.SetBool("isHurting", false);
    }

}