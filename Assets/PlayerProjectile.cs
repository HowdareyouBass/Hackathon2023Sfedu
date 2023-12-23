using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _speed = 3;

    private Rigidbody2D _rb;

    // Specify to direct your projectile  
    public Vector3 forward;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector3.Normalize(forward) * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Health>(out Health health) && collider.transform != Player.Instance.transform)
        {
            health.Reduce(_damage);
            Destroy(gameObject);
        }
    }
}