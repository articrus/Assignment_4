using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written By: Gianni Coladonato
 * ID: 2414537
 * Last Modified (Date|ID): 09-11-2024, 2414537
 */
//This class is used to determine the type of contacts a rigid body can process
public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.03f;
    public float ceilingDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded = true;
    [SerializeField]
    private bool _isOnWall;
    [SerializeField]
    private bool _isOnCeiling;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    //If the entity is on the ground
    public bool IsGrounded 
    {   get{ return _isGrounded; }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    //If the entity is on the wall, used to prevent entities not falling while hugging a wall
    public bool IsOnWall
    {   get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    //Currently unused, but can be useful when detecting ceiling collisions (above)
    public bool IsOnCeiling
    {   get { return _isOnCeiling; }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //If it is colliding on the ground, then it is true, else it is in the air, thus false
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}