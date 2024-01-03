using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public bool inputMethod;
    Rigidbody rb;

    // 4th dimension coordinate
    public float w;
    public float dw;
    public FourthDimension d;
    public float radius_4d = 1f;

    // 4d physics
    private bool isJumping = false;
    private float jumpTime;
    private float jumpDuration = 0.5f; // Duration for bigger jumps
    private float jumpForce = 2f;
    private float w_gravity = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    public void swapInputMethod()
    {
        inputMethod = !inputMethod;
    }

    void Update()
    {

        // 4d player render (a little unoptimized)
        // Function makes the scale of the player drop off in a manner similar to a circle
        float dist = Mathf.Min(Mathf.Abs(d.current_4d - w), radius_4d);
        float scale_w = Mathf.Sqrt(1 - Mathf.Pow(dist / radius_4d, 2));
        transform.localScale = new Vector3(scale_w,scale_w,scale_w);


        if (Time.timeScale != 0f)
        {
            // 3d physics
            float xInput = Input.GetAxis("Horizontal");
            float zInput = Input.GetAxis("Vertical");
            float yInput = Input.GetAxis("Z Vertical");

            if (inputMethod)
            {
                Vector3 moveDirection = new Vector3(xInput, yInput, zInput);
                rb.velocity = moveDirection * moveSpeed;
            }
            else
            {

                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
                float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

                transform.Rotate(new Vector3(-mouseY, mouseX, 0f), Space.Self);

                Vector3 moveDirection = transform.forward * zInput * moveSpeed;
                Vector3 strafeDirection = transform.right * xInput * moveSpeed;
                Vector3 verticalStrafeDireciton = transform.up * yInput * moveSpeed;

                rb.velocity = moveDirection + strafeDirection + verticalStrafeDireciton;
            }

            //4d physics
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping && canJump())
            {
                isJumping = true;
                jumpTime = Time.time;
                dw = getJumpForce();
            }

            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                if (Time.time - jumpTime > jumpDuration)
                {
                    // Jump button can be held for higher jumps
                    isJumping = false;
                }
                dw = getJumpForce();
            }

            if (Input.GetKeyUp(KeyCode.Space) && isJumping)
            {
                // If the jump button is released before the threshold, finish the short jump
                isJumping = false;
            }

            w += dw * Time.deltaTime;

            // Temporary for no floor
            if (w <= 0)
            {
                dw = 0;
                w = 0;
            } else
            {
                dw -= w_gravity * Time.deltaTime;
            }

        }
    }

    private float getJumpForce() // Potentially add Jump Multiplier
    {
        return jumpForce;
    }

    private bool canJump()
    {
        // Allows for theoretical frame perfect jumps at the exact peak of the jump but at that point go for it
        return dw == 0 && isJumping == false;
    }

}
