using UnityEngine;

public class GoBackController : MonoBehaviour
{
    [SerializeField] GameObject padre;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("WallGoBack") )
        {

            padre.GetComponent<SpriteRenderer>().enabled = false;
            padre.GetComponent<Collider2D>().enabled = false;
            Invoke(nameof(Respawn),gameObject.GetComponentInParent<EnemyController>().secondsRespawn);
            
        }
    }

    private void Respawn()
    {
        gameObject.GetComponentInParent<EnemyController>().PutEarthwormInStartingPosition();
        padre.GetComponent<SpriteRenderer>().enabled = true;
        padre.GetComponent<Collider2D>().enabled = true;

    }
}
