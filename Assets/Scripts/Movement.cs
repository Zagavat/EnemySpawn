using System.Collections.Generic;
using UnityEngine;

public static class AnimatorWalk { }

public static class States
{
    public const string Up = nameof(Up);
    public const string Down = nameof(Down);
    public const string Left = nameof(Left);
    public const string Right = nameof(Right);
}

[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _nextPosition;
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    private Animator _animator;

    private void Start()
    {
        float widthCamera = Camera.main.orthographicSize * Camera.main.aspect;
        Vector3 centerCamera = Camera.main.transform.position;
        _minX = centerCamera.x - widthCamera + 0.3f;
        _maxX = centerCamera.x + widthCamera - 0.3f;
        _minY = centerCamera.y - Camera.main.orthographicSize + 0.3f;
        _maxY = centerCamera.y + Camera.main.orthographicSize - 0.3f;
        _animator = GetComponent<Animator>();
        _nextPosition = SetNextWaypoint();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _speed * Time.deltaTime);

        if (transform.position == _nextPosition)
        {
            _nextPosition = SetNextWaypoint();
        }
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
                positionY = Random.Range(transform.position.y, _maxY);
                _animator.SetBool(States.Up, true);
                break;
            case (int)Direction.Down:
                positionY = Random.Range(_minY, transform.position.y);
                _animator.SetBool(States.Down, true);
                break;
            case (int)Direction.Left:
                positionX = Random.Range(_minX, transform.position.x);
                _animator.SetBool(States.Left, true);
                break;
            case (int)Direction.Right:
                positionX = Random.Range(transform.position.x, _maxX);
                _animator.SetBool(States.Right, true);
                break;
        }

        return new Vector3(positionX, positionY, transform.position.z);
    }
}
