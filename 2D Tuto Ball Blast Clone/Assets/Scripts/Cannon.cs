using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Camera _cam;
    private Rigidbody2D _rb;

    [SerializeField] private HingeJoint2D[] _wheels;
    private JointMotor2D _motor;

    [SerializeField] private float _cannonSpeed;
    private bool _isMoving = false;

    private Vector2 _pos;
    private float _screenBounds;
    private float _velocityX;
    [SerializeField] private float _offset;

    private void Start(){
        _cam = Camera.main;

        _rb = GetComponent<Rigidbody2D>();
        _pos = _rb.position;

        _motor = _wheels[0].motor;
        _motor = _wheels[1].motor;

        _screenBounds = Game.Instance.screenWidth - 0.56f;
    }

    private void Update()
    {
        //Check player input (hand or mouse drag)
        _isMoving = Input.GetMouseButton(0);

        if (_isMoving){
            _pos.x = _cam.ScreenToWorldPoint(Input.mousePosition).x;
        }
    }

    private void FixedUpdate()
    {
        //Move the cannon
        if (_isMoving){
            _rb.MovePosition(Vector2.Lerp(_rb.position, _pos, _cannonSpeed * Time.fixedDeltaTime));

            _velocityX = (_pos.x - _rb.position.x) * _offset; 
        }
        else{
            _rb.velocity = Vector2.zero;

            _velocityX = 0f;
        }

        //Rotate wheels
      //  _velocityX = _rb.GetPointVelocity(_rb.position).x;
        if (Mathf.Abs(_velocityX) > 0.0f && Mathf.Abs(_rb.position.x) < _screenBounds){
            _motor.motorSpeed = _velocityX * 150f;
            MotorActivate(true);
        }else{
            _motor.motorSpeed = 0f;
            MotorActivate(false);
        }
    }

    private void MotorActivate(bool isActive){
        _wheels[0].useMotor = isActive;
        _wheels[1].useMotor = isActive;

        _wheels[0].motor = _motor;
        _wheels[1].motor = _motor;
    }

}
