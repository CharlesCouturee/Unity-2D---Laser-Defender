using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 rawInput;
    Vector2 minBound;
    Vector2 maxBound;

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();  
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0f));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1f, 1f));
    }

    private void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPose = new Vector2();
        newPose.x = Mathf.Clamp(transform.position.x + delta.x, minBound.x + paddingLeft, maxBound.x - paddingRight);
        newPose.y = Mathf.Clamp(transform.position.y + delta.y, minBound.y + paddingBottom, maxBound.y - paddingTop);
        transform.position = newPose;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    { 
        if (shooter)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
