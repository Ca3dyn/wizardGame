using UnityEngine;

public class characterController : MonoBehaviour
{
    public float moveSpeed = 5.0f;   // Character movement speed
    public float jumpForce = 5.0f;   // Character jump force

    private Rigidbody rb;           // Character rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the character's rigidbody component
    }

    void FixedUpdate()
    {
        // Move the character using the keyboard arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * moveSpeed);

        // Jump the character using the space bar key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}