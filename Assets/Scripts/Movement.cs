using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _nextPosition;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private Animator _animator;

    private void Start()
    {
        float widthCamera = Camera.main.orthographicSize * Camera.main.aspect;
        Vector3 centerCamera = Camera.main.transform.position;
        minX = centerCamera.x - widthCamera + 0.3f;
        maxX = centerCamera.x + widthCamera - 0.3f;
        minY = centerCamera.y - Camera.main.orthographicSize +0.3f;
        maxY = centerCamera.y + Camera.main.orthographicSize - 0.3f;
        _animator = GetComponent<Animator>();
        _nextPosition = SetNextWaypoint();
    }
    
    enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        Count
    }

    private Vector3 SetNextWaypoint()
    {
        int direction = Random.Range(0, (int)Direction.Count);
        float positionX = transform.position.x;
        float positionY = transform.position.y;
        _animator.Rebind();

        switch (direction)
        {
            case (int)Direction.Up:
                positionY = Random.Range(transform.position.y, maxY);
                _animator.SetBool("Up", true);
                break;
            case (int)Direction.Down:
                positionY = Random.Range(minY, transform.position.y);
                _animator.SetBool("Down", true);
                break;
            case (int)Direction.Left:
                positionX = Random.Range(minX, transform.position.x);
                _animator.SetBool("Left", true);
                break;
            case (int)Direction.Right:
                positionX = Random.Range(transform.position.x, maxX);
                _animator.SetBool("Right", true);
                break;
        }

        return new Vector3(positionX, positionY, transform.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _speed * Time.deltaTime);

        if (transform.position == _nextPosition)
        {
            _nextPosition = SetNextWaypoint();
        }
    }
}
