using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
[System.Serializable]
public class Wave : LevelStage
{
    public override void Execute(TargetDetector targetDetector, EnemySpawner enemySpawner)
    {
        GameObject[] enemyGameObjects = enemySpawner.SpawnEnemies(Enemies.ToArray());
        targetDetector.AddEnemies(enemyGameObjects);
    }
}