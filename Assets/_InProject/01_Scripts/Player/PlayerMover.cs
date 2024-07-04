using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    private InputAction moveAction;
    private Animator anim;
    private Rigidbody2D rigid2D;
    private Vector2 directionVec;

    public float moveSpeed;

    private bool isTopBorderTriggered;
    private bool isBottomBorderTriggered;
    private bool isLeftBorderTriggered;
    private bool isRightBorderTriggered;

    private void Awake()
    {
        moveAction = FindObjectOfType<PlayerInput>().actions["Move"];
        anim = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        EnableControl();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Border") == true)
        {
            switch (other.name)
            {
                case "Top":
                    isTopBorderTriggered = true;
                    break;
                case "Bottom":
                    isBottomBorderTriggered = true;
                    break;
                case "Left":
                    isLeftBorderTriggered = true;
                    break;
                case "Right":
                    isRightBorderTriggered = true;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player Border") == true)
        {
            switch (other.name)
            {
                case "Top":
                    isTopBorderTriggered = false;
                    break;
                case "Bottom":
                    isBottomBorderTriggered = false;
                    break;
                case "Left":
                    isLeftBorderTriggered = false;
                    break;
                case "Right":
                    isRightBorderTriggered = false;
                    break;
            }
        }
    }

    private void Update()
    {
        Vector2 direction = directionVec;

        if (isTopBorderTriggered == true && direction.y > 0
            || isBottomBorderTriggered == true && direction.y < 0)
            direction.y = 0;

        if (isLeftBorderTriggered == true && direction.x < 0
            || isRightBorderTriggered == true && direction.x > 0)
            direction.x = 0;

        Vector2 calculateMoveVec = direction.normalized * moveSpeed;
        rigid2D.velocity = calculateMoveVec;
    }

    public void EnableControl()
    {
        moveAction.started += OnMoveInputed;
        moveAction.performed += OnMoveInputed;
        moveAction.canceled += OnMoveInputed;
    }

    public void DisableControl()
    {
        moveAction.performed -= OnMoveInputed;
        moveAction.performed -= OnMoveInputed;
        moveAction.canceled -= OnMoveInputed;
    }

    private void OnMoveInputed(InputAction.CallbackContext context)
    {
        directionVec = context.ReadValue<Vector2>();
        anim.SetInteger("Move_X", (int) directionVec.x);
    }
}
