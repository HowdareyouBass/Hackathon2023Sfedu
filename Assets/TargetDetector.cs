using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class TargetDetector : MonoBehaviour
{
    public Transform CurrentTarget { get; private set; } = null;

    public event Action TargetChanged;

    private List<Transform> _targets;
    private Health _playerHealth;

    private bool _isFirstTarget = true;

    private void Awake()
    {
        _targets = new List<Transform>();
        _playerHealth = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _playerHealth.Hit += GetNextTarget;
        GlobalEvents.AnyEnemyKilled += GetNextTarget;
    }
    private void OnDisable()
    {
        _playerHealth.Hit -= GetNextTarget;
        GlobalEvents.AnyEnemyKilled -= GetNextTarget;
    }

    public void AddEnemies(GameObject[] enemyGameObjects)
    {

        foreach (GameObject enemy in enemyGameObjects)
        {
            _targets.Add(enemy.transform);
        }
        _targets.Sort(delegate (Transform x, Transform y) { return Vector3.Distance(x.position, Player.Instance.transform.position).CompareTo(Vector3.Distance(y.position, Player.Instance.transform.position)); });
        if (_isFirstTarget)
        {
            _isFirstTarget = false;
            GetNextTarget();
        }
    }

    public Transform GetTarget()
    {
        Transform target = _targets[0];
        _targets.RemoveAt(0);
        return target;
    }

    private void GetNextTarget()
    {
        if (_targets.Count == 0)
        {
            return;
        }
        CurrentTarget = GetTarget();
        TargetChanged?.Invoke();
    }
}
