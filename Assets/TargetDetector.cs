using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    List<Transform> _targets;

    private void Awake()
    {
        _targets = new List<Transform>();
    }

    public void AddEnemies(GameObject[] enemyGameObjects)
    {
        foreach (GameObject enemy in enemyGameObjects)
        {
            _targets.Add(enemy.transform);
        }
        _targets.Sort(delegate(Transform x, Transform y) { return Vector3.Distance(x.position, Player.Instance.transform.position).CompareTo(Vector3.Distance(y.position, Player.Instance.transform.position)); });
    }

    public Transform GetTarget()
    {
        Transform target = _targets[0];
        _targets.RemoveAt(0);
        return target;
    }
}
