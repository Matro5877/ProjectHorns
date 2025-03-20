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
    public float bumperForce;

    private float verticalSpeed;
    private Vector3 direction;
    private Vector3 visibleDirection;
    public int maxJumps;
    private int jumpCount;

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

    [Header("RayCasts")]
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

    [Header("Collisions")]
    public float groundCheckDistance = 0.1f;
    public LayerMask terrain;
    public LayerMask solidOnly;
    public Collider2D feet;
    public Collider2D core;
    private float originalScale;

    void Start()
    {
        originalScale = transform.localScale.x;
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

        CeilingThing();
    }

    public void ResetJumpCount()
    {
        jumpCount = maxJumps;
    }
    public void Jump()
    {
        if (jumpCount > 0)
        {
            verticalSpeed = jumpForce;
            jumpCount -= 1;
        }
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
        if (groundCheck == false)
        {
            Fall();
        }
        else
        {
            //Stops the player's fall. Preventing it to fall indefinitly at the same speed.
            if (verticalSpeed < 0)
            {
                verticalSpeed = 0;
                ResetJumpCount();
            }
        }

        if (Input.GetKey(KeyCode.X))
        {
            Jump();
        }
    }

    public void isMoving()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (canMoveRight)
            {
                visibleDirection = Vector3.right;
                MoveRight();
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (canMoveLeft)
            {
                visibleDirection = Vector3.left;
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

    private void CeilingThing()
    {
        if (ceilingCheck && verticalSpeed > 0)
        {
            verticalSpeed = 0;
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

    public void VisibleDirection()
    {
        if (visibleDirection == Vector3.right)
        {
            transform.localScale = new Vector3(-originalScale, transform.localScale.y, transform.localScale.z);
        }
        if (visibleDirection == Vector3.left)
        {
            transform.localScale = new Vector3(originalScale, transform.localScale.y, transform.localScale.z);
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
        leftBottomWallCheck = Physics2D.Raycast(BottomLeftPosition, Vector2.left, groundCheckDistance, solidOnly);
        leftTopWallCheck = Physics2D.Raycast(TopLeftPosition, Vector2.left, groundCheckDistance, solidOnly);
        rightBottomWallCheck = Physics2D.Raycast(BottomRightPosition, Vector2.right, groundCheckDistance, solidOnly);
        rightTopWallCheck = Physics2D.Raycast(TopRightPosition, Vector2.right, groundCheckDistance, solidOnly);
        leftCeilingCheck = Physics2D.Raycast(TopLeftPosition, Vector2.up, groundCheckDistance, solidOnly);
        rightCeilingCheck = Physics2D.Raycast(TopRightPosition, Vector2.up, groundCheckDistance, solidOnly);

        if (leftGroundCheck || rightGroundCheck)
        {
            groundCheck = true;
        }
        else
        {
            groundCheck = false;
        }
        if (leftBottomWallCheck || leftTopWallCheck)
        {
            leftWallCheck = true;
        }
        else
        {
            leftWallCheck = false;
        }
        if (rightBottomWallCheck || rightTopWallCheck)
        {
            rightWallCheck = true;
        }
        else
        {
            rightWallCheck = false;
        }
        if (leftCeilingCheck || rightCeilingCheck)
        {
            ceilingCheck = true;
        }
        else
        {
            ceilingCheck = false;
        }
    }

    public void jumpOnJumpables()
    {
        if (verticalSpeed < 0)
        {
            Debug.Log("Jumpable");
            verticalSpeed = bumperForce;
        }
    }

    public void getHitBySomething()
    {

    }

}
