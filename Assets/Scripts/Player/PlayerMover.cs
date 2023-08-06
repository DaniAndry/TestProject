using Unity.VisualScripting;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public static bool PointerDown = true;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Transform _weapon;

    private float _moveSpeed = 6f;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveX = _joystick.Horizontal;
        float moveY = _joystick.Vertical;
        float currentSpeed;

        if (PointerDown)
        {
            currentSpeed = 0;
        }
        else
        {
            currentSpeed = _moveSpeed;
        }

        Vector2 movement = new Vector2(moveX, moveY);
        _rigidbody.velocity = movement.normalized * currentSpeed;

        if (moveX != 0f || moveY != 0f)
        {
            Flip(moveX);
        }
    }

    private void Flip(float moveX)
    {
        if (moveX < 0f)
        {
            Vector3 weaponLocalPosition = _weapon.localPosition;
            weaponLocalPosition.x = Mathf.Abs(weaponLocalPosition.x) * -1;
            _weapon.localPosition = weaponLocalPosition;

            Vector3 weaponLocalRotation = _weapon.localRotation.eulerAngles;
            weaponLocalRotation.y = 180f;
            _weapon.localRotation = Quaternion.Euler(weaponLocalRotation);
        }
        else if (moveX > 0f)
        {
            Vector3 weaponLocalPosition = _weapon.localPosition;
            weaponLocalPosition.x = Mathf.Abs(weaponLocalPosition.x);
            _weapon.localPosition = weaponLocalPosition;

            Vector3 weaponLocalRotation = _weapon.localRotation.eulerAngles;
            weaponLocalRotation.y = 0f;
            _weapon.localRotation = Quaternion.Euler(weaponLocalRotation);
        }
    }

}
