using UnityEngine;

public class MeteorFissionabl : Meteor
{
    [SerializeField] private GameObject[] _splitsPrefabs;

    override protected void Die(){
        SplitMeteor();

        Destroy(gameObject);
    }

    private void SplitMeteor(){
        GameObject objects;
        for (int i = 0; i < 2; i++){
            objects = Instantiate(_splitsPrefabs[i], transform.position, Quaternion.identity, MeteorSpawner.Instance.transform);
            objects.GetComponent<Rigidbody2D>().velocity = new Vector2(_leftAndRight[i], 5f);
        }
    }
}
