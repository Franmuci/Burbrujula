using UnityEngine;

public class MainMenuFran1 : MonoBehaviour
{

    private void Start()
    {
        Invoke(nameof(Daleninio),1.0f);
    }

    private void Daleninio()
    {
        TutorialBeh.Instance.Tutorial();

    }


}
