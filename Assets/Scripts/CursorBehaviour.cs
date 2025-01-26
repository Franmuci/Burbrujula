using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{

    [SerializeField] Vector3 hotspot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Cursor.visible = false;
       Cursor.lockState = CursorLockMode.None;

    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.position = Input.mousePosition + hotspot;
    }
}
