using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Damage = 5;

    private void OnEnable()
    {
        GetComponent<Health>().Death += Die;
    }
    private void OnDisable()
    {
        GetComponent<Health>().Death -= Die;
    }

    private void Die()
    {
        GlobalEvents.AnyEnemyKilled?.Invoke();
        Destroy(gameObject);
    }
}
