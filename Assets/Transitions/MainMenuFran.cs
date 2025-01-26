using UnityEngine;

public class MainMenuFran : MonoBehaviour
{
    public void SwitchScene()
    {
        LevelManagerTransition.Instance.GenerarBuruBuru("BubbleUp");
        LevelManagerTransition.Instance.Tutorial();

    }
}
