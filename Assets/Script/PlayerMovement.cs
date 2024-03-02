using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float dashCd;
    bool isDash;
    bool canDash;
    Vector2 moveDirection;
    void Start()
    {
        canDash = true;
    }
    void Update()
    {
        ProcessInputs();
    }
    
    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        if (isDash)
        {
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void Move()
    {
        if (isDash)
        {
            return;
        }
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDash = true;
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration); 
        isDash = false;
        yield return new WaitForSeconds(dashCd);
        canDash = true;
    }
}
