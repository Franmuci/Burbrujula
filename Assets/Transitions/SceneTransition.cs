using System.Collections;
using UnityEngine;

public abstract class SceneTransition : MonoBehaviour
{
    public abstract IEnumerator AnimateTransitionIn();
    public abstract void BubbleTransition();
    public abstract IEnumerator AnimateTransitionOut();
}