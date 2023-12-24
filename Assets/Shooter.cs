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

    private float _time;

    private void Start()
    {
        _concetration = GetComponent<Concetration>();
    }

    private void Update()
    {
        _attackCooldownTimeInSeconds = _baseAttackSpeed / (float)Plug.Concetration * 10;
        // Debug.Log(_attackCooldownTimeInSeconds);
        // Debug.Log(_time);
        _time += Time.deltaTime;
        if (_time >= _attackCooldownTimeInSeconds)
        {
            Shoot();
            _time = 0.0f;
        }

    }

    // private void ChangeAttackSpeed()
    // {
    //     _attackCooldownTimeInSeconds = _baseAttackSpeed / ((float)_concetration.Value * 10.0f);
    //     if (_time >= _attackCooldownTimeInSeconds)
    //     {
    //         _time = 0;
    //         Shoot();
    //     }
    //     _time += Time.deltaTime;
    // }

    // private void FixedUpdate()
    // {
    //     lock (locker)
    //     {

    //         // Debug.Log(_baseAttackSpeed);
    //         // Debug.Log((float)_concetration.Value * 10.0f);
    //     }
    //     // _attackCooldownTimeInSeconds = (float)BrainBitSignalReader.Relaxation * _baseAttackSpeed;
    // }
    private void Shoot()
    {
        if (Player.Instance.GetComponent<TargetDetector>().CurrentTarget == null)
        {
            return;
        }
        Shot?.Invoke();
        Debug.Log("Should invoke");
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        PlayerProjectile pproj = projectile.GetComponent<PlayerProjectile>();
        pproj.Init();
        pproj.SetForward(Player.Instance.GetComponent<TargetDetector>().CurrentTarget.position - transform.position);
        // projectile.GetComponent<PlayerProjectile>().SetForward(Player.Instance.GetComponent<TargetDetector>().CurrentTarget.position - transform.position); 
    }
}
