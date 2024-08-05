using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyPlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 1;
    public float jumpForce = 1;

    private float moveDir = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveDir;
        rb.velocity = velocity;
    }

    protected void OnMove(InputValue value) {
        moveDir = value.Get<float>() * speed;
    }

    protected void OnJump(InputValue value) {
        Vector2 velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;
    }
}
