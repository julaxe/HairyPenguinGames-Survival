using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    
    [SerializeField] private float tickRate = 10;
    [SerializeField] private int initialSpawnAmount = 1;
    [SerializeField] private int maxAmount = 10;
    [SerializeField] private int spawnPerTick = 1;

    private BoxCollider _rangeBox;
    private List<GameObject> _enemyList;

    private float _timer;
    private bool _initialAmountSpawned = false;

    private void Awake()
    {
        _enemyList = new List<GameObject>();
        _rangeBox = GetComponent<BoxCollider>();
    }

    void Start()
    {
        for (int i = 0; i < maxAmount; i++)
        {
            AddEnemyToList();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameTime.Night)
        {
            if (!_initialAmountSpawned)
            {
                InitialSpawn();
            }
            if (_timer > tickRate)
            {
                for (int i = 0; i < spawnPerTick; i++)
                {
                    if (_enemyList.Exists(x => !x.activeInHierarchy))
                    {
                        ActivateAnEnemy();
                    }
                    else
                    {
                        break;
                    }
                }
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }
        else //if is day -> reset the initial spawn
        {
            _initialAmountSpawned = false;
            
            //disable all the enemies during the day
            if(_enemyList.Exists(x => x.activeInHierarchy))
                _enemyList.ForEach(x => x.SetActive(false));
        }
    }

    private void ActivateAnEnemy()
    {
        //look for the first inactive
        var temp = _enemyList.Find(x => !x.activeInHierarchy);
        if (!temp) return;
            
        temp.SetActive(true);
        RandomizePosition(temp);
    }
    private void AddEnemyToList()
    {
        var temp = Instantiate(enemy, transform);
        _enemyList.Add(temp);
        temp.SetActive(false);
    }

    private void RandomizePosition(GameObject obj)
    {
        float randomX = Random.Range(_rangeBox.bounds.min.x, _rangeBox.bounds.max.x);
        float randomY = Random.Range(_rangeBox.bounds.min.y, _rangeBox.bounds.max.y);
        float randomZ = Random.Range(_rangeBox.bounds.min.z, _rangeBox.bounds.max.z);

        obj.transform.position = new Vector3(randomX, randomY, randomZ);
    }

    private void InitialSpawn()
    {
        for (int i = 0; i < initialSpawnAmount; i++)
        {
            ActivateAnEnemy();
        }

        _initialAmountSpawned = true;
    }
    
}
