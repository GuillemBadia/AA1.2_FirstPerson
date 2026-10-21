using UnityEngine;
using UnityEngine.InputSystem;

public class FPSMovement : MonoBehaviour
{
    InputSystem_Actions inputs;
    public float speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputs = new InputSystem_Actions();
        inputs.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = inputs.Player.Move.ReadValue<Vector2>();
        dir.z = dir.y;
        dir.y = 0;

        dir = transform.TransformDirection(dir);

        transform.position += dir * speed * Time.deltaTime;
    }
}
