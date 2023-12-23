using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform == Player.Instance.transform)
        {
            Player.Instance.GetComponent<Health>().Reduce(transform.GetComponent<Enemy>().Damage);
        }
    }
}
