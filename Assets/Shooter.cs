using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public event Action Shot;

    [SerializeField] private float _baseAttackSpeed;
    [SerializeField] private BrainBitSignalReader _signalReader;
    [SerializeField] private GameObject _projectilePrefab;

    private readonly object locker = new object();

    private float _attackCooldownTimeInSeconds;
    private Coroutine _shootingRoutine;

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
            _attackCooldownTimeInSeconds = _baseAttackSpeed / (float)Plug.Relaxation / 10;
        }
        // _attackCooldownTimeInSeconds = (float)BrainBitSignalReader.Relaxation * _baseAttackSpeed;
    }
    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            lock (locker)
            {
                Debug.Log("SHOT:" + _attackCooldownTimeInSeconds.ToString());
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
