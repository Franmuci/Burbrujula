using UnityEngine;

public class TutorialBeh : MonoBehaviour
{


    public static bool isInTutorial;
    public static TutorialBeh Instance;




    public GameObject panel1GameObject;
    public GameObject panel2GameObject;
    public GameObject panel3GameObject;
    public GameObject panel4GameObject;
    public GameObject panel5GameObject;


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


    public void Tutorial()
    {
        Time.timeScale = 0.0f;
        panel1GameObject.SetActive(true);
        isInTutorial = true;

    }

    public void Tutorial2()
    {
        panel1GameObject.SetActive(false);
        panel2GameObject.SetActive(true);
    }

    public void Tutorial3()
    {
        panel2GameObject.SetActive(false);
        panel3GameObject.SetActive(true);
    }

    public void Tutorial4()
    {
        panel3GameObject.SetActive(false);
        panel4GameObject.SetActive(true);
    }
    public void Tutorial5()
    {
        panel4GameObject.SetActive(false);
        panel5GameObject.SetActive(true);
    }

    public void TutorialFinal()
    {
        panel5GameObject.SetActive(false);
        Time.timeScale = 1.0f;
        isInTutorial = false;
    }

}
