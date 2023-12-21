using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public bool inputMethod;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null )
        {
            rb.freezeRotation = true;
        }
    }
    
    public void swapInputMethod() {
        inputMethod = !inputMethod;
    }

    void Update()
    {
        if (Time.timeScale != 0f)
        {
            float xInput = Input.GetAxis("Horizontal");
            float zInput = Input.GetAxis("Vertical");
            
            if (inputMethod) {
                float yInput = Input.GetAxis("Z Vertical");
                Vector3 moveDirection = new Vector3(xInput, yInput, zInput);
                rb.velocity = moveDirection * moveSpeed;
            } else {
                
                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
                float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

                transform.Rotate(new Vector3(-mouseY, mouseX, 0f), Space.Self);

                float verticalInput = Input.GetAxis("Vertical");
                float horizontalInput = Input.GetAxis("Horizontal");

                Vector3 moveDirection = transform.forward * verticalInput * moveSpeed;
                Vector3 strafeDirection = transform.right * horizontalInput * moveSpeed;

                rb.velocity = moveDirection + strafeDirection;
            }

        }
    }

}
