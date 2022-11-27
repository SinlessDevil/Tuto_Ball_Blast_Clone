using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _meteorPrefabs;
    [SerializeField] private int _meteorsCount;
    [SerializeField] float _spawnerDelay;

    private GameObject[] _meteors;


    #region Singleton class: Missiles

    public static MeteorSpawner Instance;

    public void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        PrepareMeteors();
        StartCoroutine(SpawnMeteors());
    }

    private IEnumerator SpawnMeteors()
    {
        for (int i = 0; i < _meteorsCount; i++)
        {
            _meteors[i].SetActive(true);
            yield return new WaitForSeconds(_spawnerDelay);
        }
    }

    private void PrepareMeteors(){
        _meteors = new GameObject[_meteorsCount];
        int prefabsCount = _meteorPrefabs.Length;
        for (int i = 0; i < _meteorsCount; i++){
            _meteors[i] = Instantiate(_meteorPrefabs[Random.Range(0, prefabsCount)], transform);
            _meteors[i].GetComponent<Meteor>().isResultOfFission = false;
            _meteors[i].SetActive(false);
        }
    }
}
