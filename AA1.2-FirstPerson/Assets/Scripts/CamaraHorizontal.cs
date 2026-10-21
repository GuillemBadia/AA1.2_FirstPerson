using UnityEngine;
using UnityEngine.InputSystem;

public class CamaraHorizontal : MonoBehaviour
{
    public float rotationSpeed = 0.01f;


    void Update()
    {

        Debug.Log(Mouse.current.delta.value);
        Debug.Log(Mouse.current.position.value);


        transform.localEulerAngles += new Vector3(0, Mouse.current.delta.value.x, 0) * rotationSpeed;
    }

}
