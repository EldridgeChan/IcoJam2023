using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D arrowRig;

    private int damage = 0;

    private void Start()
    {
        if (!arrowRig) { arrowRig = GetComponent<Rigidbody2D>(); }
        arrowFly();
        Destroy(gameObject, GameManager.Instance.GameDesignScriptObj.ArrowMaxFlyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerPawn") || collision.CompareTag("EnemyPawn"))
        {
            collision.GetComponent<PawnBehaviour>().attacked(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }

    private void arrowFly()
    {
        Vector2 flyDir = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
        arrowRig.velocity = flyDir * GameManager.Instance.GameDesignScriptObj.ArrowSpeed;
    }

    public void init(int damage)
    {
        this.damage = damage;
    }
}
