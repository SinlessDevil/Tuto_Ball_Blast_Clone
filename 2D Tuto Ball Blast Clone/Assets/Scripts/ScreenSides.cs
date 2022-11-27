using UnityEngine;

public class ScreenSides : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _leftWallCollider;
    [SerializeField] private BoxCollider2D _rightWallCollider;

    private void Start()
    {
        float screenWight = Game.Instance.screenWidth;

        _leftWallCollider.transform.position = new Vector3(-screenWight - _leftWallCollider.size.x / 2f, 0f, 0f);
        _rightWallCollider.transform.position = new Vector3(screenWight + _rightWallCollider.size.x / 2f, 0f, 0f);

        //disable this script: we're no Longer need if : this.eabled = false
        //or destroy if:
        Destroy(this);

    }

}
