using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMover : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private float _moveSpeed;

    private IPaddleMover _mover;
    public IPaddleMover Mover
    {
        get => _mover;
        set
        {
            if (_mover != null)
                return;
            _mover = value;
        }
    }

    void Update () => _body.velocity = Vector2.up * (_mover.MoveDirection * _moveSpeed);

    private void Reset ()
    {
        _body = GetComponent<Rigidbody2D>();
        _moveSpeed = 1;
    }
}
