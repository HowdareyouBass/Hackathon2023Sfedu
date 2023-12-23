using System.Collections;
using System.Collections.Generic;
using NeuroSDK;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _baseAttackSpeed;
    [SerializeField] private BrainBitSignalReader _signalReader;
    [SerializeField] private GameObject _projectilePrefab;

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
        _attackCooldownTimeInSeconds = _signalReader.Alpha * _baseAttackSpeed;
    }
    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(_attackCooldownTimeInSeconds);
        }
    }
    private void Shoot()
    {
        if (Player.Instance.GetComponent<TargetDetector>().CurrentTarget == null)
            return;
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.FromToRotation(transform.position, Player.Instance.GetComponent<TargetDetector>().CurrentTarget.position));
        Debug.Log(Player.Instance.GetComponent<TargetDetector>().CurrentTarget.position - transform.position);
        projectile.GetComponent<PlayerProjectile>().forward = Player.Instance.GetComponent<TargetDetector>().CurrentTarget.position - transform.position; 
    }
}
