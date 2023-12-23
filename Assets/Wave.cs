using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
[System.Serializable]
public class Wave : LevelStage
{
    public override void Execute(TargetDetector targetDetector, EnemySpawner enemySpawner)
    {
        GameObject[] enemyGameObjects = new GameObject[Enemies.Count];
        for (int i = 0; i < Enemies.Count; i++)
        {
            enemyGameObjects[i] = Enemies[i].Prefab;
        }
        GameObject[] gameObjects = enemySpawner.SpawnEnemies(enemyGameObjects);
        targetDetector.AddEnemies(gameObjects);
    }
}