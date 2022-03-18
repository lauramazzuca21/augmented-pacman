using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private float _speed = 10.0f;


    private Direction _previousDirection = Direction.RIGHT;
    private Direction _currentDirection = Direction.RIGHT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //FixedUpdate gets called before Update
    void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Mathf.Abs(_joystick.Horizontal) == .0f && Mathf.Abs(_joystick.Vertical) == .0f)
            return;

        _previousDirection = _currentDirection;
        ////If we're moving the joystick more horizontally
        if (Mathf.Abs(_joystick.Horizontal) > Mathf.Abs(_joystick.Vertical))
        {
            _currentDirection = _joystick.Horizontal > 0.0f ? Direction.RIGHT : Direction.LEFT;
        }
        else
        {
            _currentDirection = _joystick.Vertical > 0.0f ? Direction.UP : Direction.DOWN;
        }

        if (_previousDirection != _currentDirection)
        {
            float angle;
            Constants.RotationVector.TryGetValue(new KeyValuePair<Direction, Direction>(_previousDirection, _currentDirection), out angle);
            transform.Rotate(0.0f, angle, 0.0f);
        }

        Vector3 newPos;
        if (_currentDirection == Direction.DOWN || _currentDirection == Direction.UP)
            newPos = new Vector3(0.0f, _joystick.Vertical * _speed * Time.deltaTime, 0.0f);
        else
            newPos = new Vector3(_joystick.Horizontal * _speed * Time.deltaTime, 0.0f, 0.0f);

        transform.position = new Vector3(transform.position.x, transform.position.y, 10.0f) + newPos;
    }
}
