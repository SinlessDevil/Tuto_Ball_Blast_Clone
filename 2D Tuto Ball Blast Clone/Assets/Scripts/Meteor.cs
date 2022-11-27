using UnityEngine;
using TMPro;

public class Meteor : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected int _health;

    [SerializeField] protected TMP_Text _textHealth;
    [SerializeField] protected float _jumpForce;

    protected float[] _leftAndRight = new float[2] {-1f, 1f};

    [HideInInspector] public bool isResultOfFission = true;

    protected bool _isShowing;

    const string NAME_TAG_CANNON = "cannon";
    const string NAME_TAG_MISSILE = "missile";
    const string NAME_TAG_WALL = "Wall";
    const string NAME_TAG_GROUD = "ground";

    const string NAME_METHODS_FALLDOWN = "FallDown";

    private void Start()
    {
        UpdateHealthUI();

        _isShowing = true;
        _rb.gravityScale = 0f;

        if (isResultOfFission){
            FallDown();

        }else{
            float direction = _leftAndRight[Random.Range(0, 1)];
            float sreenOffset = Game.Instance.screenWidth * 1.3f;
            transform.position = new Vector2(sreenOffset * direction, transform.position.y);

            _rb.velocity = new Vector2(-direction, 0f);
            //push meteor down after few seconds
            float timefall = Random.Range(sreenOffset - 2.5f, sreenOffset - 1f);
            Invoke(NAME_METHODS_FALLDOWN, timefall);
        }

    }

    private void FallDown()
    {
        _isShowing = false;
        _rb.gravityScale = 1f;
        _rb.AddTorque(Random.Range(-20f, 20f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(NAME_TAG_CANNON)){
            // gameOver
            Debug.Log("GameOver");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        
        if(other.tag.Equals(NAME_TAG_MISSILE)){
            // takedamage
            TakeDamage(2);
            // destroy missile
            Missiles.Instance.DestroyMisiile(other.gameObject);

        }
        
        if (!_isShowing && other.tag.Equals(NAME_TAG_WALL)){
            // touched wall
            float posX = transform.position.x;
            if(posX > 0){
                // touched right wal
                _rb.AddForce(Vector2.left * 150f);
            }else{
                // touched left wall
                _rb.AddForce(Vector2.right * 150f);
            }

            _rb.AddTorque(posX * 4f);
        }
        
        if (other.tag.Equals(NAME_TAG_GROUD)){
            // touched ground
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _rb.AddTorque(-_rb.angularVelocity * 4f);
        }
    }

    public void TakeDamage(int damage)
    {
        if(_health > 1){
            _health -= damage;
        }else{
            Die();
        }
        UpdateHealthUI();
    }

    virtual protected void Die()
    {
        Destroy(gameObject);
    }

    protected void UpdateHealthUI()
    {
        _textHealth.text = _health.ToString();
    }
}
