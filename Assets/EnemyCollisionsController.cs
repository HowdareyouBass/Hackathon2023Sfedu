using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyCollisionController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform == Player.Instance.transform)
        {
            Player.Instance.GetComponent<Health>().Reduce(transform.GetComponent<Enemy>().Damage);
            Destroy(gameObject, 0.1f);
        }
    }
}
