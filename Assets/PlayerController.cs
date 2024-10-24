using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector2 moveDir;
    Vector2 mouseDelta;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] float mouseSens;
    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        RotateCamera();
    }

    private void FixedUpdate()
    {
        Vector3 worldMove = transform.TransformDirection(new (moveDir.x, 0, moveDir.y));
        
        rb.velocity = worldMove * moveSpeed;
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        // Rotate the camera up/down based on the Y movement
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Clamp to prevent full rotation

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body left/right based on X movement
        transform.Rotate(Vector3.up * mouseX);
    }
}
