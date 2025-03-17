using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [Header("Physique")]
    public float walkingSpeed;
    public float sprintingSpeed;
    public float maxFallingSpeed;
    public float gravityForce;
    public float jumpForce;

    private float verticalSpeed;
    private Vector3 direction;

    [Header("Autorisations")]
    public bool canMoveRight;
    public bool canMoveLeft;
    public bool canJump;
    public bool canCharge;
    private bool isSprinting;
    public bool canSprint;
    public bool canStopSprinting;

    [Header("Corners")]
    public GameObject TopLeft;
    public GameObject BottomLeft;
    public GameObject TopRight;
    public GameObject BottomRight;

    private Vector2 TopLeftPosition;
    private Vector2 BottomLeftPosition;
    private Vector2 TopRightPosition;
    private Vector2 BottomRightPosition;

    public bool leftGroundCheck;
    public bool rightGroundCheck;
    public bool leftBottomWallCheck;
    public bool leftTopWallCheck;
    public bool rightBottomWallCheck;
    public bool rightTopWallCheck;
    public bool leftCeilingCheck;
    public bool rightCeilingCheck;

    private bool groundCheck;
    private bool leftWallCheck;
    private bool rightWallCheck;
    private bool ceilingCheck;


    public float groundCheckDistance = 0.1f;
    public LayerMask terrain;

    void Start()
    {
        verticalSpeed = 0;
        EnableGameplay();
    }

    void Update()
    {
        transform.position += Vector3.up * verticalSpeed * Time.deltaTime;

        RayCastTest();
        Gameplay();
    }

    public void Gameplay()
    {
        isSprintingVerif();

        isMoving();

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

    public void JumpFallMess()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (groundCheck == false)
            {
                Fall();
            }
        }
        else
        {
            //Stops the player's fall. Preventing it to fall indefinitly at the same speed.
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
            //Verify if Falling isn't already activated
            if (Input.GetKey(KeyCode.Z) == false)
            {
                //Uses Fall to slow down the vertical speed of the character to 0.
                if (verticalSpeed > 0)
                {
                    Fall();
                }
            }
        }
    }

    public void isMoving()
    {
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
    }

    private void MoveRight()
    {
        direction = Vector3.right;
        if (rightWallCheck == false)
        {
            Move();
        }
    }

    private void MoveLeft()
    {
        direction = Vector3.left;
        if (leftWallCheck == false)
        {
            Move();
        }
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

    public void isSprintingVerif()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (canSprint)
            {
                isSprinting = true;
            }
        }
        else
        {
            if (canStopSprinting)
            {
                isSprinting = false;
            }
        }
    }

    public void DisableGameplay()
    {
        canMoveLeft = false;
        canMoveRight = false;
        canJump = false;
        canCharge = false;
        canSprint = false;
        canStopSprinting = false;
    }

    public void EnableGameplay()
    {
        canMoveLeft = true;
        canMoveRight = true;
        canJump = true;
        canCharge = true;
        canSprint = true;
        canStopSprinting = true;
    }

    public void RayCastTest()
    {
        TopLeftPosition = TopLeft.transform.position;
        BottomLeftPosition = BottomLeft.transform.position;
        TopRightPosition = TopRight.transform.position;
        BottomRightPosition = BottomRight.transform.position;

        Debug.Log("RayCast");

        leftGroundCheck = Physics2D.Raycast(BottomLeftPosition, Vector2.down, groundCheckDistance, terrain);
        rightGroundCheck = Physics2D.Raycast(BottomRightPosition, Vector2.down, groundCheckDistance, terrain);
        leftBottomWallCheck = Physics2D.Raycast(BottomLeftPosition, Vector2.left, groundCheckDistance, terrain);
        leftTopWallCheck = Physics2D.Raycast(TopLeftPosition, Vector2.left, groundCheckDistance, terrain);
        rightBottomWallCheck = Physics2D.Raycast(BottomRightPosition, Vector2.right, groundCheckDistance, terrain);
        rightTopWallCheck = Physics2D.Raycast(TopRightPosition, Vector2.right, groundCheckDistance, terrain);
        leftCeilingCheck = Physics2D.Raycast(TopLeftPosition, Vector2.up, groundCheckDistance, terrain);
        rightCeilingCheck = Physics2D.Raycast(TopRightPosition, Vector2.up, groundCheckDistance, terrain);

        if (leftGroundCheck && rightGroundCheck)
        {
            groundCheck = true;
        }
        else
        {
            groundCheck = false;
        }
        if (leftBottomWallCheck && leftTopWallCheck)
        {
            leftWallCheck = true;
        }
        else
        {
            leftWallCheck = false;
        }
        if (rightBottomWallCheck && rightTopWallCheck)
        {
            rightWallCheck = true;
        }
        else
        {
            rightWallCheck = false;
        }
        if (leftCeilingCheck && rightCeilingCheck)
        {
            ceilingCheck = true;
        }
        else
        {
            ceilingCheck = false;
        }
    }
}
