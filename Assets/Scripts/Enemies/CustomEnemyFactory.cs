using UnityEngine;
using Zenject;

public class CustomEnemyFactory : IEnemyFactory
{
    private DiContainer diContainer;

    public CustomEnemyFactory(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }

    public void Create(GameObject enemyPrefab, Vector3 at)
    {
        diContainer.InstantiatePrefab(enemyPrefab, at, Quaternion.identity, null);
    }
}
