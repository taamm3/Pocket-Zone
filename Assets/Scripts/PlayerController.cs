using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FixedJoystick Joystick;
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    private Vector2 _move;


    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _move.x = Joystick.Horizontal;
        _move.y = Joystick.Vertical;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _move * _speed * Time.fixedDeltaTime);
    }
}