using UnityEngine;

public class Clouds : MonoBehaviour{
    [System.Serializable] class Cloud
    {
        public MeshRenderer meshRenderer = null;
        public float speed = 0f;
        [HideInInspector] public Vector2 offset;
        [HideInInspector] public Material mat;
    }

    [SerializeField] private Cloud[] allClounds;

    private int count;

    private void Start(){
        count = allClounds.Length;
        for (int i = 0; i < count; i++){
            allClounds[i].offset = Vector2.zero;
            allClounds[i].mat = allClounds[i].meshRenderer.material;
        }
    }

    private void Update()
    {
        for (int i = 0; i < count; i++)
        {
            allClounds[i].offset.x += allClounds[i].speed * Time.deltaTime;
            allClounds[i].mat.mainTextureOffset = allClounds[i].offset;
        }
    }

}
