using UnityEngine;

public class Platform : MonoBehaviour
{

    public float w1;
    public float w2;

    public FourthDimension d;

    private BoxCollider collision;
    private Renderer objectRenderer;
    private Material myMaterial;

    // Config
    private float gradientLengthW = 1f;
    private float maxScaleSize = 1.2f; // potentially change to static growth size instead of percentage

    private Color belowColor = new Color(0f, 1f, 0f);
    private Color aboveColor = new Color(1f, 0f, 0f);
    private Color renderedColor = new Color(1f, 1f, 1f);

    public float debug;

    void Start()
    {
        collision = GetComponent<BoxCollider>();
        objectRenderer = GetComponent<Renderer>();
        myMaterial = objectRenderer.material;
        collision.enabled = false;
        belowColor.a = 0f;
        objectRenderer.material.color = belowColor;
        objectRenderer.material.SetFloat("_Mode", 3);
        objectRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    
    void Update()
    {
        renderBasedOnShape();
    }

    private void renderBasedOnShape() // Change function to change shape of object in 4d
    {
        if (d.current_4d < w1 - gradientLengthW) { transform.localScale = Vector3.zero; }
        else if (d.current_4d > w2 + gradientLengthW) { transform.localScale = Vector3.zero; }
        else if (d.current_4d < w1)
        {
            belowColor.a = 1 - (w1 - d.current_4d) / (gradientLengthW);
            objectRenderer.material.color = belowColor;
            collision.enabled = false;
            transform.localScale = Vector3.one * Mathf.Lerp(1, maxScaleSize, (w1 - d.current_4d) / gradientLengthW);
            
        }
        else if (d.current_4d > w2)
        {
            aboveColor.a = 1 - (d.current_4d - w2) / (gradientLengthW);
            objectRenderer.material.color = aboveColor;
            collision.enabled = false;
            transform.localScale = Vector3.one * Mathf.Lerp(1, maxScaleSize, (d.current_4d - w2) / gradientLengthW);
        }
        else
        {
            collision.enabled = true;
            transform.localScale = Vector3.one;
            objectRenderer.material.color = renderedColor;
            objectRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }

}
