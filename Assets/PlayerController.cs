using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector2 moveDir;
    Vector2 mouseDelta;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] float mouseSens;
    [SerializeField] float rotationSpeed = 90;
    float xRotation;
    bool inspectingObject;
    public Transform selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            inspectingObject = !inspectingObject;
            if(!inspectingObject) { selectedObject.rotation = Quaternion.Euler(0, 0, 0); }
        }

        if (inspectingObject) RotateObject();
        else
        {
            RotateCamera();
            moveDir = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 worldMove = transform.TransformDirection(new(moveDir.x, 0, moveDir.y));
        rb.velocity = worldMove * moveSpeed;
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

    void RotateObject()
    {
        selectedObject.Rotate(GetRotationVector());


        Vector3 GetRotationVector()
        {
            return rotationSpeed * 
            new Vector3(
                Input.GetAxis("RotateX"),
                Input.GetAxis("RotateY"),
                Input.GetAxis("RotateZ")
            ) * Time.deltaTime;
        }
    }
}
