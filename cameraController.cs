using UnityEngine;
public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float height = 2.0f;
    public float rotationDamping = 3.0f;
    public float heightDamping = 2.0f;
    public float zoomSpeed = 2.0f;
    public float maxZoom = 10.0f;
    public float minZoom = 1.0f;
    public float mouseSensitivity = 2.0f;
    public float maxVerticalAngle = 90.0f;
    public float minVerticalAngle = -90.0f;
    public float headOffset = 1.5f;
    public float collisionOffset = 0.2f;

    private float currentZoom = 5.0f;
    private float desiredZoom = 5.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private void LateUpdate()
    {
        if (!target)
            return;

        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, minVerticalAngle, maxVerticalAngle);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);

        //telling how far behind the target the camera should be
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);

        Vector3 position = rotation * negDistance + target.position;
        position += Vector3.up * (height - headOffset); // Add head offset

        currentZoom = Mathf.Lerp(currentZoom, desiredZoom, Time.deltaTime * zoomSpeed);

        position += Vector3.up * height;

        transform.position = position;
        transform.LookAt(target.position + Vector3.up * headOffset); // Look at head

    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        desiredZoom -= scroll * zoomSpeed;

        desiredZoom = Mathf.Clamp(desiredZoom, minZoom, maxZoom);
    }
}
