using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class LevelStage : ScriptableObject
{
    public float DelayBeforeExecutionInSeconds;
    public List<EnemySO> Enemies;

    public abstract void Execute(TargetDetector targetDetector, EnemySpawner enemySpawner);
}