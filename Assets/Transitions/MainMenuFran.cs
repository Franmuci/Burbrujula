using UnityEngine;

public class MainMenuFran : MonoBehaviour
{
    public void SwitchScene()
    {
        LevelManagerTransition.Instance.LoadScene("Level1", "CircleWipe");
    }
}
