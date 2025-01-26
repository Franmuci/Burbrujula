using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private List<int> thisQuantityOfBubbles = new();
    private BubbleController bubbleController;
    private void Start()
    {
        bubbleController = GameObject.Find("Worm").GetComponent<BubbleController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Mole")
        {
            MoleController moleController = collision.gameObject.GetComponent<MoleController>();

            if (moleController != null && bubbleController != null)
            {
                moleController.LastCheckpointPosition = transform.position;
                moleController.lastCheckpoint = transform.gameObject;
                ChangeBubbleQuantity();
            }

        }


    }

    public void ChangeBubbleQuantity()
    {
        bubbleController.bubblesLeft.Clear();
        foreach(int num in thisQuantityOfBubbles)
        {
            bubbleController.bubblesLeft.Add(num);
        }
    }
}
