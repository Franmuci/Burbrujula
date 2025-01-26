using UnityEngine;

public class GenerarBuruburu : MonoBehaviour
{


    public GameObject transitionsContainer;

    private SceneTransition transition;

    public static GenerarBuruburu Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        transition = transitionsContainer.GetComponentInChildren<SceneTransition>();
    }

    public void GenerarBuruBuru()
    {
        
        transition.BubbleTransition();
    }
}
