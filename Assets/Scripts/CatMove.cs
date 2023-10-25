using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask hangingGround;

    private float dirX;
    [SerializeField] private float walkspeed = 7f;
    [SerializeField] private float jumpforce = 3f;

    private enum MovementState { stay, walk, jump, hang }
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHanging() == false)
        {
            dirX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(dirX * walkspeed, rb.velocity.y);
        } 
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKey("up") && (IsGrounded() || IsHanging()))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            
        }
        
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MovementState state;
        
        if (dirX > 0f)
        {
            state = MovementState.walk;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.walk;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.stay;
        }
        
        if (rb.velocity.y > .1f || rb.velocity.y < -.1f)
        {
            state = MovementState.jump;
        }
        
        if (IsHanging() == true)
        {
            state = MovementState.hang;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    
    private bool IsHanging()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, hangingGround);
    }
    
}
