using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        transform.position += _speed * Vector3.Normalize(Player.Instance.transform.position - transform.position);
    }
}
