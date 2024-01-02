using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public PlayerController pc;
    public float cameraDistance = 10.0f;

    private Vector3 staticlocation = new Vector3(0,6,-15);
    private Vector3 staticlook = new Vector3(0,5,0);

    // Use this for initialization
    void Start()
    {
        transform.position = staticlocation;
        transform.LookAt(staticlook);
    }

    void LateUpdate()
    {
        if (!pc.inputMethod)
        {
            transform.position = player.transform.position - player.transform.forward * cameraDistance;
            transform.LookAt(player.transform.position, player.transform.up);
        }
    }

    public void StaticCamera() 
    {
        transform.position = staticlocation;
        transform.LookAt(staticlook);
    }
}