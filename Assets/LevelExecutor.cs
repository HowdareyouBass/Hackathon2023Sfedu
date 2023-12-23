using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LevelExecutor : MonoBehaviour
{
    [SerializeField] private List<LevelSO> _levels;

    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void Start()
    {
        StartCoroutine(ExecuteLevels());
    }

    public IEnumerator ExecuteLevels()
    {
        foreach(LevelSO level in _levels)
        {
            foreach(LevelStage levelStage in level.LevelStages)
            {
                yield return new WaitForSeconds(levelStage.DelayBeforeExecutionInSeconds);
                levelStage.Execute(_targetDetector, _enemySpawner);
            }
        }
    }
}
