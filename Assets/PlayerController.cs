using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null )
        {
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float yInput = Input.GetAxis("Z Vertical");

        //Vector3 moveDirection = new Vector3(xInput, yInput, zInput);
        //rb.velocity = moveDirection * moveSpeed;

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.Rotate(new Vector3(-mouseY, mouseX, 0f), Space.Self);

        // Apply rotations to the player
        //Quaternion targetRotation = Quaternion.Euler(-pitch, yaw, 0f);
        //transform.rotation = targetRotation;


        // Movement based on player's orientation
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = transform.forward * verticalInput * moveSpeed;
        Vector3 strafeDirection = transform.right * horizontalInput * moveSpeed;

        rb.velocity = moveDirection + strafeDirection;

    }

}
