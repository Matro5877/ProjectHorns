using UnityEngine;
using UnityEngine.InputSystem;

public class EntityController : MonoBehaviour
{
    public InputActionAsset inputAsset;
    private float _axis;
    public float speed;
    public float sprintSpeed;
    private bool _isUsingSecondary = false;

    private void OnEnable()
    {
        inputAsset.FindAction("Gameplay/Jump").started += HandleJump;
        inputAsset.FindAction("Gameplay/Jump").canceled += HandleJump;

        inputAsset.FindAction("Gameplay/Move").started += HandleMove;
        inputAsset.FindAction("Gameplay/Move").canceled += HandleMove;

        inputAsset.FindAction("Gameplay/Secondary").started += HandleSecondary;
        inputAsset.FindAction("Gameplay/Secondary").canceled += HandleSecondary;

        inputAsset.Enable();
    }

    private void OnDisable()
    {
        inputAsset.FindAction("Gameplay/Jump").started -= HandleJump;
        inputAsset.FindAction("Gameplay/Jump").canceled -= HandleJump;

        inputAsset.FindAction("Gameplay/Move").started -= HandleMove;
        inputAsset.FindAction("Gameplay/Move").canceled -= HandleMove;

        inputAsset.FindAction("Gameplay/Secondary").started -= HandleSecondary;
        inputAsset.FindAction("Gameplay/Secondary").canceled -= HandleSecondary;

        inputAsset.Disable();
    }

    private void Update()
    {
        if (_isUsingSecondary)
        {
            transform.position += Vector3.right * _axis * sprintSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * _axis * speed * Time.deltaTime;
        }
    }

    private void HandleJump(InputAction.CallbackContext ctx)
    {
        Debug.Log($"JUMP: Phase = {ctx.phase}");
    }

    private void HandleMove(InputAction.CallbackContext ctx)
    {
        _axis = ctx.ReadValue<float>();
        Debug.Log($"MOVE: Phase = {ctx.phase}, Axis = {_axis}");
    }

    private void HandleSecondary(InputAction.CallbackContext ctx)
    {
        _isUsingSecondary = !ctx.canceled;
        Debug.Log($"SECONDARY: Phase = {ctx.phase}");
    }
}
