using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private float speed = 10.0f;


    private Direction _previousDirection = Direction.RIGHT;
    private Direction _currentDirection = Direction.RIGHT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Mathf.Abs(joystick.Horizontal) == .0f && Mathf.Abs(joystick.Vertical) == .0f)
            return;

        _previousDirection = _currentDirection;
        ////If we're moving the joystick more horizontally
        if (Mathf.Abs(joystick.Horizontal) > Mathf.Abs(joystick.Vertical))
        {
            _currentDirection = joystick.Horizontal > 0.0f ? Direction.RIGHT : Direction.LEFT;
        }
        else
        {
            _currentDirection = joystick.Vertical > 0.0f ? Direction.UP : Direction.DOWN;
        }

        if (_previousDirection != _currentDirection)
        {
            float angle;
            Constants.RotationVector.TryGetValue(new KeyValuePair<Direction, Direction>(_previousDirection, _currentDirection), out angle);
            transform.Rotate(0.0f, angle, 0.0f );
        }

        Vector3 newPos;
        if (_currentDirection == Direction.DOWN || _currentDirection == Direction.UP)
            newPos = new Vector3(0.0f, 0.0f, joystick.Vertical * speed * Time.deltaTime);
        else
            newPos = new Vector3(joystick.Horizontal * speed * Time.deltaTime, 0.0f, 0.0f);

        transform.position = transform.position + newPos;
        //Debug.Log(transform.position.z);
    }
}
