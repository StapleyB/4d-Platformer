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

        }
    }

}
