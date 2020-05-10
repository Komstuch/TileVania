using CollabProxy.UI;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    bool isAlive = true;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;

    float startingPlayerGravity;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        startingPlayerGravity = myRigidBody.gravityScale;
    }

    void Update()
    {
        if(!isAlive) { return; }
        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        Die();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //from -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow*runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            bool isTouchingGround = myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
            if (isTouchingGround)
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidBody.velocity += jumpVelocityToAdd;
            }
        }
    }
    private void ClimbLadder()
    {
        bool isTouchingLadder = myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"));

        if (!isTouchingLadder) {
            myAnimator.SetBool("isClimbing", false);
            myRigidBody.gravityScale = startingPlayerGravity;
            return; 
        }
     
        float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
        bool playerHasVerticalSpeed = Mathf.Abs(verticalThrow) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", isTouchingLadder & playerHasVerticalSpeed);

        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, climbSpeed * verticalThrow);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0;
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            this.transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1);
        }
    }

    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            Debug.Log("Ouch!");
            isAlive = false;
            myAnimator.SetBool("isDead", true);

            Vector2 deathKick = new Vector2(-Mathf.Sign(myRigidBody.velocity.x) *10f, 10f);
            myRigidBody.velocity = deathKick;
        }
    }
}
