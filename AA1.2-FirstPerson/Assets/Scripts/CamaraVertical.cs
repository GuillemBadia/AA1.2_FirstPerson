using UnityEngine;
using UnityEngine.InputSystem;

public class CamaraVertical : MonoBehaviour
{
    public float rotationSpeed = 0.01f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        transform.localEulerAngles += new Vector3(-Mouse.current.delta.value.y, 0, 0) * rotationSpeed;
    }

}
