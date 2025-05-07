using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Collections.AllocatorManager;

public class Chara : MonoBehaviour
{
    [Header("Paramètres Physique")]
    public float walkingSpeed;
    public float sprintingSpeed;
    public float sneakingSpeed;
    public float dashingSpeed;
    public float aerialSpeed;
    public float maxAerialSpeed;
    public float maxFallingSpeed;
    public float gravityForce;
    public float lowerGravityForce;
    public float jumpForce;
    public float bumperForce;
    public float airResistance;
    public float dashDuration;


    [Header("Physique")]
    private float verticalSpeed;
    private float horizontalSpeed;
    private Vector3 direction;
    private Vector3 visibleDirection;
    public int maxJumps;
    private int jumpCount;
    private Vector2 playerPos2D;
    private bool hasToStopWhenTouchingGround;
    private bool lateJump;
    public float lateJumpDistance;
    private bool jumpSource;
    private float delayedHorizontalSpeed;
    private float fallHorizontalSpeed;


    [Header("Autorisations")]
    public bool canMoveRight;
    public bool canMoveLeft;
    public bool canJump;
    public bool canCharge;
    private bool isSprinting;
    private bool isWalking;
    private bool isSneaking;
    public bool isDashing;
    public bool canSprint;
    public bool canStopSprinting;
    public bool canBumper;
    public bool canGetHit;
    public bool canDash;

    [Header("Corners")]
    public GameObject TopLeft;
    public GameObject BottomLeft;
    public GameObject TopRight;
    public GameObject BottomRight;

    private Vector2 TopLeftPosition;
    private Vector2 BottomLeftPosition;
    private Vector2 TopRightPosition;
    private Vector2 BottomRightPosition;

    public float wallOffset; 

    [Header("RayCasts")]
    public RaycastHit2D leftGroundCheck;
    public RaycastHit2D rightGroundCheck;
    public RaycastHit2D leftBottomWallCheck;
    public RaycastHit2D leftTopWallCheck;
    public RaycastHit2D rightBottomWallCheck;
    public RaycastHit2D rightTopWallCheck;
    public RaycastHit2D leftCeilingCheck;
    public RaycastHit2D rightCeilingCheck;
    public RaycastHit2D lateJumpLeftGroundCheck;
    public RaycastHit2D lateJumpRightGroundCheck;

    [Header("Profondeurs RayCasts")]
    private float leftGroundDepth;
    private float rightGroundDepth;
    private float groundDepth;
    private float leftBottomWallDepth;
    private float leftTopWallDepth;
    private float leftWallDepth;
    private float rightBottomWallDepth;
    private float rightTopWallDepth;
    private float rightWallDepth;
    private float leftCeilingDepth;
    private float rightCeilingDepth;
    private float ceilingDepth;
    private float depth;

    [Header("Raccourçis RayCasts")]
    private float topRightWallDepth;
    private float bottomRightWallDepth;
    private float topLeftWallDepth;
    private float bottomLeftWallDepth;

    private bool groundCheck;
    private bool leftWallCheck;
    private bool rightWallCheck;
    private bool ceilingCheck;

    private bool delayedGroundCheck;

    [Header("Paramètres Collisions")]
    public float groundCheckDistance = 0.1f;
    public float ceilingCheckDistance = 0.05f;
    public float wallCheckDistance = 0.1f;

    [Header("Collisions")]
    public LayerMask terrain;
    public LayerMask solidOnly;
    public Collider2D feet;
    public Collider2D core;
    public GameObject rDash;
    public GameObject lDash;

    [Header("Collisions")]
    public float verticalHitForce;
    public float horizontalHitForce;
    
    [Header("Animation")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [Header("Contrôles")]
    public bool rightControl;
    public bool leftControl;
    public bool rightControlUp;
    public bool leftControlUp;
    public bool downControl;
    public bool jumpControl;
    public bool sprintControl;
    public bool dashControlUp;
    public bool interractControl;
    public bool fallControl;

    void Start()
    {
        verticalSpeed = 0;
        EnableGameplay();
    }

    void Update()
    {
        if (groundCheck)
        {
            fallHorizontalSpeed = horizontalSpeed;
        }

        //Debug.Log($"verticalSpeed : {verticalSpeed}");
        //Debug.Log($"rightGroundCheck.point.y: {rightGroundCheck.point.y}");
        //Debug.Log($"BottomRightPosition.y: {BottomRightPosition.y}");
        //Debug.Log($"horizontalSpeed: {horizontalSpeed}");

        transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        transform.position += Vector3.right * horizontalSpeed * Time.deltaTime;

        //Used to have the 2D coordinates of the Character.
        playerPos2D = new Vector2(transform.position.x, transform.position.y);

        Gameplay();
        CharaAnimation();
    }

    public void Gameplay()
    {
        Controls();

        RayCastTest();
        
        isSprintingVerif();

        isMoving();

        JumpFallController();

        AirResistance();

        CeilingThing();

        Dance();

        DashController();

        //Chaos();
    }

    public void Controls()
    {
        rightControl = (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6) || Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxis("HorizontalMenu") > 0);
        leftControl = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4) || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxis("HorizontalMenu") < 0);
        rightControlUp = (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Keypad6) || Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxis("HorizontalMenu") == 0);
        leftControlUp = (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.Keypad4) || Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxis("HorizontalMenu") == 0);
        downControl = (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Keypad5) || Input.GetKey(KeyCode.LeftControl) || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxis("VerticalMenu") > 0);
        jumpControl = (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Numlock) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.JoystickButton5));
        sprintControl = (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey(KeyCode.F3) || Input.GetKey(KeyCode.JoystickButton2) || Input.GetKey(KeyCode.RightShift) || Input.GetAxis("ZR") > 0) || Input.GetAxis("ZL") > 0;
        dashControlUp = (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.F6) || Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.RightControl));
        interractControl = (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.JoystickButton1));
        fallControl = (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Keypad0) || Input.GetKey(KeyCode.Keypad8) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Numlock) || Input.GetKey(KeyCode.JoystickButton0) || Input.GetKey(KeyCode.JoystickButton4) || Input.GetKey(KeyCode.JoystickButton5));
    }

    public void Timer(int duration)
    {
        
    }

    public void ResetJumpCount()
    {
        jumpCount = maxJumps;
    }

    public void JumpFallController()
    {
        if (groundCheck == false)
        {
            if (delayedGroundCheck)
            {
                //Debug.Log("DelayedGroundCheck");
                if (horizontalSpeed > 0)
                {
                    maxAerialSpeed = fallHorizontalSpeed;
                    //Debug.Log($"horizonalSpeed: {horizontalSpeed}");
                }
                else if (horizontalSpeed < 0)
                {
                    maxAerialSpeed = -fallHorizontalSpeed;
                    //Debug.Log($"horizonalSpeed: {horizontalSpeed}");
                }
                else
                {
                    maxAerialSpeed = walkingSpeed;
                    //Debug.Log("Else");
                }
            }

            Fall();
            delayedGroundCheck = false;
            delayedHorizontalSpeed = horizontalSpeed;
        }
        else
        {
            //Stops the player's fall. Preventing it to fall indefinitly at the same speed.
            if (verticalSpeed < 0)
            {
                verticalSpeed = 0;
                ResetJumpCount();

                if (hasToStopWhenTouchingGround)
                {
                    if (!isSprinting)
                    {
                        animator.SetBool("anim_isSprinting", false);
                    }
                    else if (!isWalking)
                    {
                        animator.SetBool("anim_isWalking", false);
                    }
                    //horizontalSpeed = 0;
                    hasToStopWhenTouchingGround = false;
                }
            }
            delayedGroundCheck = true;
            
        }
        // hi hi je suis caché
        if (jumpControl || lateJump)
        {
            if (jumpCount > 0)
            {
                Jump(jumpForce);
            }
        }

        if (jumpControl && (lateJumpLeftGroundCheck || lateJumpRightGroundCheck) && verticalSpeed < 0)
        {
            lateJump = true;
        }
    }
    
    public void Jump(float force)
    {
        lateJump = false;
        //horizontalSpeed = horizontalSpeed / 1.5f;
        verticalSpeed = force;
        jumpCount -= 1;
        if (jumpSource)
        {
            maxAerialSpeed = sprintingSpeed;

            jumpSource = false;
        }
        else
        {
            if (horizontalSpeed > 0)
            {
                maxAerialSpeed = horizontalSpeed;
            }
            else if (horizontalSpeed < 0)
            {
                maxAerialSpeed = -horizontalSpeed;
            }
            else
            {
                maxAerialSpeed = walkingSpeed;
            }
        }
    }

    public void Fall()
    {
        //Debug.Log("isFalling");

        if (fallControl && verticalSpeed > 0)
        {
            verticalSpeed -= lowerGravityForce * Time.deltaTime;
            hasToStopWhenTouchingGround = true;
            
            
            //Debug.Log("Lowwww");
        }
        else
        {
            verticalSpeed -= gravityForce * Time.deltaTime;
            hasToStopWhenTouchingGround = true;
            animator.SetBool("anim_isFloating", false);
            //Debug.Log("Not Lowwww");
        }
        if (fallControl && !groundCheck)
        {
            if (verticalSpeed < (jumpForce / 3))
            {
                animator.SetBool("anim_isFloating", true);
            }
        }
        else
        {
            animator.SetBool("anim_isFloating", false);
        }
        
        if (verticalSpeed < maxFallingSpeed)
        {
            verticalSpeed = maxFallingSpeed;
        }
    }

    public void isMoving()
    {
        if (rightControl)
        {
            if (canMoveRight)
            {
                visibleDirection = Vector3.right;
                MoveRight();
            }
        }
        else if (leftControl)
        {
            if (canMoveLeft)
            {
                visibleDirection = Vector3.left;
                MoveLeft();
            }
        }
        else if ((leftControlUp || rightControlUp) && groundCheck && canStopSprinting)
        {
            animator.SetBool("anim_isWalking", false);
            animator.SetBool("anim_isSprinting", false);
            horizontalSpeed = 0;
            canDash = true;
        }
    }

    private void MoveRight()
    {
        direction = Vector2.right;
        if (rightWallCheck == false)
        {
            Move(direction);
        }
    }

    private void MoveLeft()
    {
        direction = Vector2.left;
        if (leftWallCheck == false)
        {
            Move(direction);
        }
    }

    private void Move(Vector2 movingDirection)
    {
        if (groundCheck && verticalSpeed <= 0)
        {
            if (isDashing)
            {
                horizontalSpeed = dashingSpeed * movingDirection.x;
            }
            else if (isSprinting)
            {
                //transform.position += movingDirection * sprintingSpeed * Time.deltaTime;
                horizontalSpeed = sprintingSpeed * movingDirection.x;
                animator.SetBool("anim_isSprinting",true);
                animator.SetBool("anim_isWalking", false);
            }
            else if (isWalking && !isSprinting)
            {
                //transform.position += movingDirection * walkingSpeed * Time.deltaTime;
                horizontalSpeed = walkingSpeed * movingDirection.x;
                animator.SetBool("anim_isWalking", true);
                animator.SetBool("anim_isSprinting", false);
            }
            else if (isSneaking && !isSprinting)
            {
                horizontalSpeed = sneakingSpeed * movingDirection.x;
                animator.SetBool("anim_isWalking", true);
                animator.SetBool("anim_isSprinting", false);
            }
        }
        else if (!groundCheck)
        {
            horizontalSpeed += aerialSpeed * movingDirection.x * Time.deltaTime;

            if (horizontalSpeed > maxAerialSpeed)
            {
                horizontalSpeed = maxAerialSpeed;
            }
            if (horizontalSpeed < - maxAerialSpeed)
            {
                horizontalSpeed = - maxAerialSpeed;
            }
            
            /*if ((- maxAerialSpeed < horizontalSpeed) && (horizontalSpeed < maxAerialSpeed))
            {
            horizontalSpeed += aerialSpeed * movingDirection.x * Time.deltaTime;
            }
            else
            {
                horizontalSpeed -= aerialSpeed * movingDirection.x * Time.deltaTime;
            }*/
        }
        
    }

    private void AirResistance()
    {
        if ((/*(groundCheck == true && verticalSpeed <= 0) || */groundCheck == false) && rightControl == false && leftControl == false && !isDashing)
        {
            Debug.Log("Air Resistance");
            if (horizontalSpeed < 0)
            {
                horizontalSpeed += airResistance * Time.deltaTime;
            }
            if (horizontalSpeed > 0)
            {
                horizontalSpeed -= airResistance * Time.deltaTime;
            }
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
        if (sprintControl && canSprint)
        {
            isSprinting = true;
            isWalking = false;
            isSneaking = false;
        }
        else
        {
            if (canStopSprinting)
            {
                isSprinting = false;
                isWalking = true;
            }
        }
        if (dashControlUp)
        {
            StartCoroutine(DashTimer());
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
        //TopLeftPosition = TopLeft.transform.position;
        TopLeftPosition = new Vector2(TopLeft.transform.position.x + groundCheckDistance + wallOffset, TopLeft.transform.position.y - ceilingCheckDistance);
        //BottomLeftPosition = BottomLeft.transform.position
        BottomLeftPosition = new Vector2(BottomLeft.transform.position.x + groundCheckDistance + wallOffset, BottomLeft.transform.position.y + groundCheckDistance);
        //TopRightPosition = TopRight.transform.position;
        TopRightPosition = new Vector2(TopRight.transform.position.x - groundCheckDistance - wallOffset, TopRight.transform.position.y - ceilingCheckDistance);
        //BottomRightPosition = BottomRight.transform.position;
        BottomRightPosition = new Vector2(BottomRight.transform.position.x - groundCheckDistance - wallOffset, BottomLeft.transform.position.y + groundCheckDistance);

        //Debug.Log("RayCast");

        leftGroundCheck = Physics2D.Raycast(BottomLeftPosition, Vector2.down, groundCheckDistance, terrain);
        rightGroundCheck = Physics2D.Raycast(BottomRightPosition, Vector2.down, groundCheckDistance, terrain);
        leftBottomWallCheck = Physics2D.Raycast(BottomLeftPosition, Vector2.left, wallCheckDistance, solidOnly);
        leftTopWallCheck = Physics2D.Raycast(TopLeftPosition, Vector2.left, wallCheckDistance, solidOnly);
        rightBottomWallCheck = Physics2D.Raycast(BottomRightPosition, Vector2.right, wallCheckDistance, solidOnly);
        rightTopWallCheck = Physics2D.Raycast(TopRightPosition, Vector2.right, wallCheckDistance, solidOnly);
        leftCeilingCheck = Physics2D.Raycast(TopLeftPosition, Vector2.up, ceilingCheckDistance, solidOnly);
        rightCeilingCheck = Physics2D.Raycast(TopRightPosition, Vector2.up, ceilingCheckDistance, solidOnly);

        lateJumpLeftGroundCheck = Physics2D.Raycast(BottomLeftPosition, Vector2.down, lateJumpDistance, terrain);
        lateJumpRightGroundCheck = Physics2D.Raycast(BottomRightPosition, Vector2.down, lateJumpDistance, terrain);

        Debug.DrawRay(BottomRightPosition, Vector2.down * lateJumpDistance, Color.blue);
        Debug.DrawRay(BottomLeftPosition, Vector2.down * lateJumpDistance, Color.blue);

        Debug.DrawRay(BottomRightPosition, Vector2.down * groundCheckDistance, Color.red);
        Debug.DrawRay(BottomLeftPosition, Vector2.down * groundCheckDistance, Color.red);
        Debug.DrawRay(BottomRightPosition, Vector2.right * wallCheckDistance, Color.red);
        Debug.DrawRay(TopRightPosition, Vector2.right * wallCheckDistance, Color.red);
        Debug.DrawRay(BottomLeftPosition, Vector2.left * wallCheckDistance, Color.red);
        Debug.DrawRay(TopLeftPosition, Vector2.left * wallCheckDistance, Color.red);
        Debug.DrawRay(TopRightPosition, Vector2.up * ceilingCheckDistance, Color.red);
        Debug.DrawRay(TopLeftPosition, Vector2.up * ceilingCheckDistance, Color.red);

        /*rightGroundDepth = BottomRightPosition.y - rightGroundCheck.point.y;
        leftGroundDepth = BottomLeftPosition.y - leftGroundCheck.point.y;
        //rightCeilingDepth = TopRightPosition.y - rightCeilingCheck.point.y;
        //leftCeilingDepth = TopLeftPosition.y - leftCeilingCheck.point.y; 

        leftBottomWallDepth = leftBottomWallCheck.point.x - BottomLeftPosition.x;
        leftTopWallDepth = leftTopWallCheck.point.x - TopLeftPosition.x;
        rightBottomWallDepth = rightBottomWallCheck.point.x - BottomRightPosition.x;
        rightTopWallDepth = rightTopWallCheck.point.x - TopRightPosition.x;*/


        CollisionsDoubleCheck();

        AntiStuck();
    }

    public void jumpOnJumpables()
    {
        if (verticalSpeed < -0.1)
        {
            Debug.Log("Jumpable");
            jumpSource = true;
            Jump(bumperForce);
        }
    }

    public void getHitBySomething(Vector2 enemyPos2D)
    {
        Debug.Log("Joueur touche Enemy");
        Jump(verticalHitForce);
        if (playerPos2D.x > enemyPos2D.x)
        {
            Debug.Log("Hit from the right");

            horizontalSpeed = horizontalHitForce;
        } 
        else
        {
            Debug.Log("Hit from the left");

            horizontalSpeed = - horizontalHitForce;
        }
    }

    public void CollisionsDoubleCheck()
    {
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

    public void AntiStuck()
    {
        GroundAntiStuck();
        RightWallAntiStuck();
        LeftWallAntiStuck();
        CeilingAntiStuck();
    }

    public void GroundAntiStuck()
    {
        rightGroundDepth = groundCheckDistance - rightGroundCheck.distance;
        //Debug.Log($"rightGroundCheck.distance: {rightGroundCheck.distance}");

        leftGroundDepth = groundCheckDistance - leftGroundCheck.distance;
        //Debug.Log($"leftGroundCheck.distance: {leftGroundCheck.distance}");

        if (rightGroundCheck)
        {
            groundDepth = rightGroundDepth;
        }
        else if (leftGroundCheck)
        {
            groundDepth = leftGroundDepth;
        }
        
        if (groundCheck && verticalSpeed < 0)
        {
            VerticalAntiStuckExe(groundDepth);
        }
    }

    public void RightWallAntiStuck()
    {
        topRightWallDepth = - (wallCheckDistance - rightTopWallCheck.distance);
        bottomRightWallDepth = - (wallCheckDistance - rightBottomWallCheck.distance);

        if (rightTopWallCheck)
        {
            rightWallDepth = topRightWallDepth;
        }
        else if (rightBottomWallCheck)
        {
            rightWallDepth = bottomRightWallDepth;
        }

        if (rightWallCheck)
        {
            //Debug.Log($"rightWallDepth: {rightWallDepth}");
            HorizontalAntiStuckExe(rightWallDepth / 2);
        }
    }
    public void LeftWallAntiStuck()
    {
        topLeftWallDepth = wallCheckDistance - leftTopWallCheck.distance;
        bottomLeftWallDepth = wallCheckDistance - leftBottomWallCheck.distance;

        if (leftTopWallCheck)
        {
            leftWallDepth = topLeftWallDepth;
        }
        else if (leftBottomWallCheck)
        {
            leftWallDepth = bottomLeftWallDepth;
        }

        if (leftWallCheck)
        {
            //Debug.Log($"leftWallDepth: {leftWallDepth}");
            HorizontalAntiStuckExe(leftWallDepth / 2);
        }
    }

    public void CeilingAntiStuck()
    {
        rightCeilingDepth = - (ceilingCheckDistance - rightCeilingCheck.distance);
        //Debug.Log($"rightCeilingCheck.distance: {rightCeilingCheck.distance}");

        leftCeilingDepth = - (ceilingCheckDistance - leftCeilingCheck.distance);
        //Debug.Log($"leftCeilingCheck.distance: {leftCeilingCheck.distance}");

        if (rightCeilingCheck)
        {
            ceilingDepth = rightCeilingDepth;
        }
        else if (leftCeilingCheck)
        {
            ceilingDepth = leftCeilingDepth;
        }

        if (ceilingCheck && groundCheck == false && verticalSpeed > 0)
        {
            verticalSpeed = 0;
            VerticalAntiStuckExe(ceilingDepth);
        }
    }

    public void VerticalAntiStuckExe(float verticalDepth)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + verticalDepth, transform.position.z);
    }

    public void HorizontalAntiStuckExe(float horizontalDepth)
    {
        transform.position = new Vector3(transform.position.x + horizontalDepth, transform.position.y, transform.position.z);
    }

    public void Chaos()
    {
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log("CHAOS");
        }
    }

    public void CharaAnimation()
    {
        animator.SetBool("anim_isDashing", isDashing);
        animator.SetBool("anim_groundCheck", groundCheck && verticalSpeed <= 0);
        if (sprintControl)
        {
            animator.SetBool("anim_isSprinting", horizontalSpeed != 0);
        }
        else
        {
            animator.SetBool("anim_isWalking", horizontalSpeed != 0); 
        }
        
        spriteRenderer.flipX = direction.x < 0;
    }
    // Hop je saute
    public void Dance()
    {
        if (downControl)
        {
            animator.SetBool("anim_isDancing", true);
        }
        else
        {
            animator.SetBool("anim_isDancing", false);
        }
    }

    public void DashEnd()
    {
        isDashing = false;
        canStopSprinting = true;
        canDash = true;
    }

    public void DashController()
    {
        if (!isDashing)
        {

        }
    }

    IEnumerator DashTimer()
    {   
        Debug.Log("Fortnite 2");
        animator.SetBool("anim_dashEnd", false);
        isDashing = true;
        canStopSprinting = false;
        canDash = false;
        Move(direction);

        yield return new WaitForSeconds(dashDuration);

        animator.SetBool("anim_dashEnd", true);
    }
}
