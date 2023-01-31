using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 50f;
    [SerializeField] Transform playerBody;
    private float xRotation;


    void Update()
    {

        float mouseX = SimpleInput.GetAxis("LookX") * mouseSensitivity * Time.deltaTime;
        float mouseY = SimpleInput.GetAxis("LookY") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }


}
