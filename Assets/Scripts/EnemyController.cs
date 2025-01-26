using System.Collections;
using UnityEngine;

public enum EnemyType
{
    Spikes, Earthworm, Abism
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float speed = 3.0f;
    public float secondsRespawn = 2.0f;
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (enemyType.Equals(EnemyType.Earthworm)){
            MoveEarthworm();
        }
    }

    private void MoveEarthworm() {
        transform.position = transform.position + transform.up * speed * Time.deltaTime;
    }

    public void PutEarthwormInStartingPosition()
    {
        transform.position = startingPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Mole")
        {
            MoleController moleController = collision.gameObject.GetComponent<MoleController>();

            if (moleController != null) {
                if (enemyType.Equals(EnemyType.Abism))
                {
                    StartCoroutine(MakeMoleFall(moleController));
                } else
                {
                    moleController.GetHurt();
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
