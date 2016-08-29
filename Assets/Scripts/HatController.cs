using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb2D;
    private float maxWidth;
    private Renderer hatRenderer;
    private bool canControl;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        hatRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        canControl = false;
        FindMaxWidth();
    }

	void FixedUpdate () // for manipulating any rigidbody on any gameobject
    {
        if (canControl)
        {
            MoveWithinMaxWidth();
        }
    }

    public void toggleHatControl(bool toggle)
    {
        canControl = toggle;
    }

    void FindMaxWidth()
    {
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float hatWidth = hatRenderer.bounds.extents.x;
        maxWidth = targetWidth.x - hatWidth;
    }

    void MoveWithinMaxWidth()
    {
        Vector3 rawPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3(rawPos.x, -3f, 0f);
        float targetWidth = Mathf.Clamp(targetPos.x, -maxWidth, maxWidth);
        targetPos = new Vector3(targetWidth, targetPos.y, targetPos.z);
        rb2D.MovePosition(targetPos);
    }
}
