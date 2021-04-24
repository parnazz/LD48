using UnityEngine;

public interface IEnemyFactory
{
    void Create(GameObject enemyPrefab, Vector3 at);
}
