using UnityEngine;
using UnityEngine.InputSystem;

public class basicMovement : MonoBehaviour
{
    InputAction  moveAction;
    InputAction  jumpAction;

    public float jForce = 0.05f;
    public float mForce = 0.05f;

    public float gravity = 0.1f;
    public float fallFactor = 0.01f;

    private bool inAir = false;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    { 
        Vector2 moveVal = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(moveVal.x, 0, moveVal.y) * mForce;

        if (transform.position.y <= 0)
        {
            inAir = false;
            gravity = 0.1f;
        }

        if (!inAir)
        {
            if (jumpAction.IsPressed())
            {
                transform.position += new Vector3(0, 1.0f, 0) * jForce;
                inAir = true;
            }
        }
        else
        {
            transform.position -= new Vector3(0, gravity, 0);
            gravity += fallFactor;
        }

    }
}
