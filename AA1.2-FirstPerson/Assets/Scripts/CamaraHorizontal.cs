using UnityEngine;
using UnityEngine.InputSystem;

public class CamaraHorizontal : MonoBehaviour
{
    public float rotationSpeed = 0.01f;


    void Update()
    {

        transform.localEulerAngles += new Vector3(0, Mouse.current.delta.value.x, 0) * rotationSpeed;
    }

}
