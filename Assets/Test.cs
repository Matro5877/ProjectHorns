using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{

    public float walkingSpeed;
    public float sprintingSpeed;
    public float maxFallingSpeed;
    public float gravityForce;
    public float jumpForce;

    private float verticalSpeed;
    private Vector3 direction;

    public bool canMoveRight;
    public bool canMoveLeft;
    public bool canJump;
    public bool canCharge;
    public bool isSprinting;

    void Start()
    {
        verticalSpeed = 0;
        EnableGameplay();
    }

    void Update()
    {
        transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        Gameplay();
    }

    public void JumpFallMess()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Fall();
        }
        else
        {
            if (verticalSpeed < 0)
            {
                verticalSpeed = 0;
            }
        }

        if (Input.GetKey(KeyCode.X))
        {
            Jump();
        }
        else
        {
            if (Input.GetKey(KeyCode.Z) == false)
            {
                if (verticalSpeed > 0)
                {
                Fall();
                }
            }
        }
    }

    public void Gameplay()
    {

        if (Input.GetKey(KeyCode.C))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (canMoveRight)
            {
                MoveRight();
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (canMoveLeft)
            {
                MoveLeft();
            }
        }

        JumpFallMess();
    }

    public void Jump()
    {
        verticalSpeed = jumpForce;
    }

    public void Fall()
    {
        Debug.Log("isFalling");

        verticalSpeed -= gravityForce * Time.deltaTime;
        if (verticalSpeed < maxFallingSpeed)
        {
            verticalSpeed = maxFallingSpeed;
        }
    }

    public void DisableGameplay()
    {
        canMoveLeft = false;
        canMoveRight = false;
        canJump = false;
        canCharge = false;
    }

    public void EnableGameplay()
    {
        canMoveLeft = true;
        canMoveRight = true;
        canJump = true;
        canCharge = true;
    }

    private void MoveRight()
    {
        direction = Vector3.right;
        Move();
    }

    private void MoveLeft()
    {
        direction = Vector3.left;
        Move();
    }

    private void Move()
    {
        if (isSprinting)
        {
            transform.position += direction * sprintingSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += direction * walkingSpeed * Time.deltaTime;
        }
    }
}
