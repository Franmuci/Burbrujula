using UnityEngine;

public class MoleController : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 5.0f;
    [SerializeField] private float fastSpeed = 10.0f;

    private Vector3 clickPosition;
    private float currentSpeed;
    private bool isMoving;
    private bool isRotating;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RotateMole();
        MoveMole();
    }

    public void RespondToClick(BubbleType currentBubbleType)
    {
        switch (currentBubbleType)
        {
            case BubbleType.ForwardBubble:
                MoveTowardsBubble(normalSpeed); 
                break;
            case BubbleType.StopBubble:
                StopMoving();
                break;
            case BubbleType.FastBubble:
                MoveTowardsBubble(fastSpeed);
                break;
        }
    }

    private void MoveTowardsBubble(float speed)
    {
        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.LookAt(new Vector3(0, 0, 0));

        //Vector3 rotation = (Vector2)mousePosition - (Vector2)transform.position;
        //float rotz = Mathf.Atan2(rotation.y * -3, rotation.x * -3) * Mathf.Rad2Deg;
        //rotz += 180;
        //gameObject.GetComponent<Rigidbody2D>().rotation = rotz;

        isRotating = true;
        currentSpeed = speed;

        isMoving = true;


        //Quaternion targetRotation = new Quaternion(0.0f,0.0f, Quaternion.LookRotation(mousePosition - transform.position).z, 0.0f); 
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1.0f * Time.deltaTime);


        print("I am moving towards bubble");

    }


    private void StopMoving()
    {
        isMoving = false;
    }


    private void RotateMole()
    {
        if (isRotating)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = (new Vector3(mousePosition.x, mousePosition.y, 0) - transform.position).normalized;

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
            transform.position = transform.position + transform.up * Time.deltaTime * currentSpeed;
        }
    }

}


//rotate que espera a que movement.