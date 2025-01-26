using UnityEngine;

public class MainMenuFran : MonoBehaviour
{
    public void SwitchScene()
    {
        LevelManagerTransition.Instance.LoadScene("LevelScene","CircleWipe");
        //LevelManagerTransition.Instance.Tutorial();

    }
}
