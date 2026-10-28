using UnityEngine;
using UnityEngine.InputSystem;

public class CamaraVertical : MonoBehaviour
{
    public float rotationSpeed = 0.01f;
    public float rotacion;

    public float limiteArriba = 80f;
    public float limiteAbajo = -80f;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rotacion = transform.localEulerAngles.x;

        if (rotacion > 180f)
        {
            rotacion -= 360f;
        }
    }

    void Update()
    {
        rotacion -= Mouse.current.delta.value.y * rotationSpeed;

        rotacion = Mathf.Clamp(rotacion, limiteAbajo, limiteArriba);

        Vector3 rotacionActual = transform.localRotation.eulerAngles;

        transform.localRotation = Quaternion.Euler(
            rotacion,
            rotacionActual.y,
            rotacionActual.z
        );
    }
}