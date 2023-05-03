using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour, IPaddleMover
{
    [SerializeField] private Rigidbody2D _target;
    [SerializeField] private PaddleMover _mover;

    [SerializeField] private Transform _debugPoint;

    private Plane _plane;

    public float MoveDirection { get; private set; }

    private void Awake()
    {
        _plane = new Plane(Vector3.left, _mover.transform.position);
        _mover.Mover = this;
    }


    private void FixedUpdate()
    {
        var ballDirection = Vector3.Dot(_target.velocity.normalized, Vector3.right);
        MoveDirection = 0;
        if (ballDirection >= 0)
        {
            var ray = new Ray(_target.position, _target.velocity);
            if (!_plane.Raycast(ray, out var targetDistance))
                return;

            var rayPoint = ray.GetPoint(targetDistance);

            _debugPoint.position = rayPoint;

            var direction = rayPoint.y - _mover.transform.position.y;
            MoveDirection = (Mathf.Abs(direction) > 0.25f ? 1 : 0) * Mathf.Sign(direction);
        }
    }
}
