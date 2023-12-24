using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private Vector3 _offset = new Vector3(0.0f, 0.0f, 0.0f);

    private Vector3 _topRightCameraBoundry;
    private Vector3 _bottomLeftCameraBoundry;
    private Vector3 _topRightSpawnBoxBoundry;
    private Vector3 _bottomLeftSpawnBoxBoundry;

    private enum TriBool { True, None, False };

    private void Start()
    {
        _topRightCameraBoundry = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        _bottomLeftCameraBoundry = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        _topRightSpawnBoxBoundry = _topRightCameraBoundry + _offset;
        _bottomLeftSpawnBoxBoundry = _bottomLeftCameraBoundry - _offset;
    }

    public GameObject SpawnEnemy(EnemySO enemy)
    {
        float enemyRadius = enemy.Prefab.transform.localScale.x - 2;

        _topRightCameraBoundry += Vector3.one * enemyRadius;
        _bottomLeftCameraBoundry -= Vector3.one * enemyRadius;

        Vector3 spawnPosition = GetRandomPositionOutsideOfCamera();

        GameObject enemyGO = Instantiate(enemy.Prefab);
        enemyGO.GetComponent<Health>().MaxHealth = enemy.Health;
        enemyGO.GetComponent<Enemy>().Damage = enemy.Damage;
        enemyGO.transform.position = spawnPosition;
        return enemyGO;
    }

    public GameObject[] SpawnEnemies(EnemySO[] enemies)
    {
        GameObject[] result = new GameObject[enemies.Length];
        for(int i = 0; i < enemies.Length; i++)
        {
            result[i] = SpawnEnemy(enemies[i]);
        }
        return result;
    }

    private TriBool GenerateRandomTriBool()
    {
        float randomValue = Random.value;
        if (randomValue < 1.0f/3.0f)
        {
            return TriBool.True;
        }
        if (randomValue > 2.0f/3.0f)
        {
            return TriBool.False;
        }
        return TriBool.None;
    }

    private Vector3 GetRandomPositionOutsideOfCamera()
    {
        Vector3 result = Vector3.zero;

        TriBool spawnBottom = GenerateRandomTriBool();
        TriBool spawnLeft = GenerateRandomTriBool();

        while (spawnBottom == TriBool.None && spawnLeft == TriBool.None)
        {
            spawnBottom = GenerateRandomTriBool();
            spawnLeft = GenerateRandomTriBool();
        }

        if (spawnBottom == TriBool.True)
            result.y = Random.Range(_bottomLeftCameraBoundry.y, _bottomLeftSpawnBoxBoundry.y);
        if (spawnBottom == TriBool.None)
            result.y = Random.Range(_topRightSpawnBoxBoundry.y, _bottomLeftSpawnBoxBoundry.y);
        if (spawnBottom == TriBool.False)
            result.y = Random.Range(_topRightCameraBoundry.y, _topRightSpawnBoxBoundry.y);

        if (spawnLeft == TriBool.True)
            result.x = Random.Range(_bottomLeftCameraBoundry.x, _bottomLeftSpawnBoxBoundry.x);
        if (spawnLeft == TriBool.None)
            result.x = Random.Range(_topRightSpawnBoxBoundry.x, _bottomLeftSpawnBoxBoundry.x);
        if (spawnLeft == TriBool.False)
            result.x = Random.Range(_topRightCameraBoundry.x, _topRightSpawnBoxBoundry.x);

        return result;
    }

}
