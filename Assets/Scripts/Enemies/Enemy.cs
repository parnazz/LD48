using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    private EnemyStorage _storage;

    [Inject]
    private void Constuct(EnemyStorage storage)
    {
        _storage = storage;
    }

    // Start is called before the first frame update
    void Start()
    {
        _storage.enemies.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Factory : PlaceholderFactory<Enemy>
    {
        public Enemy Create(Vector3 at)
        {
            var enemy = base.Create();
            enemy.transform.position = at;
            return enemy;
        }
    }
}
