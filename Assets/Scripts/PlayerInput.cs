using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IPaddleMover
{
    [SerializeField] private InputAction _moveInput;
    [SerializeField] private PaddleMover _paddle;
    public float MoveDirection { get; private set; }

    void Awake()
    {
        _moveInput.Enable();

        _moveInput.performed += ctx => MoveDirection = ctx.ReadValue<float>();
        _moveInput.canceled += ctx => MoveDirection = ctx.ReadValue<float>();

        _paddle.Mover = this;
    }

}

public interface IPaddleMover
{
    public float MoveDirection { get; }
}
