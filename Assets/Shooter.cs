using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public event Action Shot;

    [SerializeField] private float _baseAttackSpeed;
    [SerializeField] private GameObject _projectilePrefab;

    private readonly object locker = new object();

    private float _attackCooldownTimeInSeconds;
    private Concetration _concetration;
    private Coroutine _shootingRoutine;

    private void Start()
    {
        _concetration = GetComponent<Concetration>();
    }

    private void OnEnable()
    {
        _shootingRoutine = StartCoroutine(ShootingRoutine());
    }
    private void OnDisable()
    {
        StopCoroutine(_shootingRoutine);
    }

    private void FixedUpdate()
    {
        lock (locker)
        {
            _attackCooldownTimeInSeconds = _baseAttackSpeed / Mathf.Max(0.2f, ((float)_concetration.Value * 10.0f));
        //    Debug.Log(_baseAttackSpeed);
           // Debug.Log((float)_concetration.Value * 10.0f);
        }
        // _attackCooldownTimeInSeconds = (float)BrainBitSignalReader.Relaxation * _baseAttackSpeed;
    }
    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            lock (locker)
            {
                //Debug.Log("SHOT:" + _attackCooldownTimeInSeconds.ToString());
                Shoot();
                yield return new WaitForSeconds(_attackCooldownTimeInSeconds);
                //yield return null;
            }

        }
    }
    private void Shoot()
    {
        if (Player.Instance.GetComponent<TargetDetector>().CurrentTarget == null)
        {
            return;
        }
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        PlayerProjectile pproj = projectile.GetComponent<PlayerProjectile>();
        pproj.Init();
        pproj.SetForward(Player.Instance.GetComponent<TargetDetector>().CurrentTarget.position - transform.position);
        Shot?.Invoke();
        // projectile.GetComponent<PlayerProjectile>().SetForward(Player.Instance.GetComponent<TargetDetector>().CurrentTarget.position - transform.position); 
    }
}
