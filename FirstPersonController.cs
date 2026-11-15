using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;

    public Transform playerCamera;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Mouse.current.delta.x.ReadValue()
                       * mouseSensitivity * Time.deltaTime;

        float mouseY = Mouse.current.delta.y.ReadValue()
                       * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

        // Keyboard movement
        float x = 0;
        float z = 0;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) x = -1;
            if (Keyboard.current.dKey.isPressed) x = 1;
            if (Keyboard.current.wKey.isPressed) z = 1;
            if (Keyboard.current.sKey.isPressed) z = -1;
        }

        Vector3 movement =
            transform.right * x + transform.forward * z;

        transform.position +=
            movement.normalized * speed * Time.deltaTime;

        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
