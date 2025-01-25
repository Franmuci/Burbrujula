using System.Collections;
using UnityEngine;

public enum EnemyType
{
    Spikes, Earthworm, Abism
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;

    private void Update()
    {
        if (enemyType.Equals(EnemyType.Earthworm)){
            MoveEarthworm();
        }
    }

    private void MoveEarthworm() { 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Mole")
        {
            if (enemyType.Equals(EnemyType.Abism))
            {
                MoleController moleController = collision.gameObject.GetComponent<MoleController>();

                if(moleController != null)
                {
                    StartCoroutine(MakeMoleFall(moleController));
                }
            }
        }
    }

    private IEnumerator MakeMoleFall(MoleController moleController)
    {
        yield return new WaitForSeconds(0.1f);

        moleController.FallDownTheAbism();
    }

}
