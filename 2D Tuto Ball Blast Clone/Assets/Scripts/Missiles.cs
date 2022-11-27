using System.Collections.Generic;
using UnityEngine;

public class Missiles : MonoBehaviour
{
    private Queue<GameObject> _missilesQueus;

    [Header("Missiles Variabls")]
    [SerializeField] private GameObject _missilePrefab;
    [SerializeField] private int _missilesCount;
    [Space(20)]

    [Header("Other Variabls")]
    [SerializeField] private float _delay = 0.3f;
    [SerializeField] private float _speed = 0.3f;

    private GameObject _object;
    private float _time = 0f;

    #region Singleton class: Missiles

    public static Missiles Instance;

    public void Awake(){
        Instance = this;
    }

    #endregion

    private void Start(){
        PrepareMissiles();
    }

    private void Update(){
        _time += Time.deltaTime;
        if(_time >= _delay){
            _time = 0f;
            _object = SpawnMissile(transform.position);
            _object.GetComponent<Rigidbody2D>().velocity = Vector2.up * _speed;
        }
    }

    private void PrepareMissiles(){
        _missilesQueus = new Queue<GameObject>();
        for (int i = 0; i < _missilesCount; i++){
            _object = Instantiate(_missilePrefab, transform.position, Quaternion.identity, transform);
            _object.SetActive(false);
            _missilesQueus.Enqueue(_object);
        }
    }

    public GameObject SpawnMissile (Vector2 position){
        if(_missilesQueus.Count > 0){
            _object = _missilesQueus.Dequeue();
            _object.transform.position = position;
            _object.SetActive(true);
            return _object;
        }

        return null;
    }

    public void DestroyMisiile (GameObject missile){
        _missilesQueus.Enqueue(missile);
        missile.SetActive(false);
    }

    //missile collision with top collider
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag.Equals("missile")){
            DestroyMisiile(other.gameObject);
        }
    }
}
