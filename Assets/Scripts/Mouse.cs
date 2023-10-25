using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float moveDistance = 5.0f;
    public float idleTimeMin = 1.0f;
    public float idleTimeMax = 3.0f;

    private bool isMoving = true;
    private float idleTimer = 0.0f;
    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private float leftBound;
    private float rightBound;
    
    private Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {  
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
        originalPosition = transform.position;
        SetRandomIdleTime();
        CalculateNewTarget();
        
        float objectWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        leftBound = -moveDistance + objectWidth / 2;
        rightBound = moveDistance - objectWidth / 2;
    }

    void Update()
    {
        if (isMoving)
        {
            anim.SetBool("walking", true);
            if (transform.position.x > targetPosition.x)
            {
                sprite.flipX = false;
            }
            else if (transform.position.x < targetPosition.x)
            {
                sprite.flipX = true;
            }
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                SetRandomIdleTime();
            }
        }
        else
        {
            anim.SetBool("walking", false);
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                isMoving = true;
                CalculateNewTarget();
                SetRandomIdleTime();
            }
        }
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBound, rightBound), transform.position.y, transform.position.z);
    }

    void SetRandomIdleTime()
    {
        idleTimer = Random.Range(idleTimeMin, idleTimeMax);
    }

    void CalculateNewTarget()
    {
        float randomX = Random.Range(leftBound, rightBound);
        targetPosition = new Vector3(randomX, transform.position.y, transform.position.z);
    }

    // Сброс позиции мыши
    public void ResetMousePosition()
    {
        transform.position = originalPosition;
        CalculateNewTarget();
    }
}
