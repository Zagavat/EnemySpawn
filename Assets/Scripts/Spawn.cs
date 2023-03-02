using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _spawnPeriod;
    [SerializeField] private GameObject _spawnPrefab;

    private Transform[] _points;
    private int _maxBotsCount = 10;
    private int _botsOnMap;

    private void Start()
    {
        _points = new Transform[_spawn.childCount];

        for (int i = 0; i < _spawn.childCount; i++)
        {
            _points[i] = _spawn.GetChild(i).transform;
        }

        var spawnInJob = StartCoroutine(SpawnNext());
    }

    private IEnumerator SpawnNext()
    {
        var waitForSpawnPeriod = new WaitForSeconds(_spawnPeriod);

        while(_botsOnMap < _maxBotsCount)
        {
            GameObject _nextBot = Instantiate(_spawnPrefab, _points[Random.Range(0, _points.Length)].position, Quaternion.identity);
            _botsOnMap++;
            yield return waitForSpawnPeriod;
        }
    }
}
