using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class BubbleUp : SceneTransition
{
    public List<Image> circle;

    public override IEnumerator AnimateTransitionIn()
    {

        foreach (var buruburu in circle)
        {
            buruburu.rectTransform.anchoredPosition = new Vector2(buruburu.transform.position.x, -400f);
            var tweener = buruburu.rectTransform.DOAnchorPosY(0f, 1f);
            yield return tweener.WaitForCompletion();
        }
        
    }

    public override IEnumerator AnimateTransitionOut()
    {
        foreach (var buruburu in circle)
        {
            var tweener = buruburu.rectTransform.DOAnchorPosY(400f, 1f);
            yield return tweener.WaitForCompletion();
        }
            
    }

    public override void BubbleTransition()
    {
        foreach (var buruburu in circle)
        {
            print(buruburu.rectTransform.anchoredPosition);
            buruburu.rectTransform.anchoredPosition = new Vector2(buruburu.rectTransform.anchoredPosition.x, -400f);
            var tweener = buruburu.rectTransform.DOAnchorPosY(400f, Random.Range(1.3f,2.2f));
        }
    }
}