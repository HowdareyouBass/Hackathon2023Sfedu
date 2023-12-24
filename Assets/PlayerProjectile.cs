using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _speed = 3;
    [SerializeField] private GameObject _effectPrefab;

    private Rigidbody2D _rb;

    // Specify to direct your projectile  
    public Vector2 forward;

    public void SetForward(Vector2 vector)
    {
        forward = vector;
        forward.Normalize();
        _rb.velocity = forward * _speed;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Health>(out Health health) && collider.transform != Player.Instance.transform)
        {
            health.Reduce(_damage);
            Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}