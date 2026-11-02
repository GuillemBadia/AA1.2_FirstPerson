
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSMovement : MonoBehaviour
{
    InputSystem_Actions inputs;
    Rigidbody rb;

    public float speed = 1;

    public Vector3 direction;
    public float distance;

    public float fuerzaSalto = 7f;

    Vector3 hitPosition;
    bool Grounded;

    void Start()
    {
        inputs = new InputSystem_Actions();
        inputs.Enable();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 movimientoInput = inputs.Player.Move.ReadValue<Vector2>();

        Vector3 dir = new Vector3(movimientoInput.x, 0, movimientoInput.y);

        dir = Vector3.ClampMagnitude(dir, 1f);

        Vector3 dirLocal = dir;

        dir = transform.TransformDirection(dir);

        RaycastHit hit;

        Debug.DrawRay(transform.position, -transform.up * distance, Color.red);

        Grounded = Physics.Raycast(transform.position, -transform.up, out hit, distance);

        float multiplicadorVelocidad = 1f;

 
        if (dirLocal.z < 0)
        {
            multiplicadorVelocidad = 0.5f;
        }
        else if (dirLocal.z == 0 && dirLocal.x != 0)
        {
            multiplicadorVelocidad = 0.75f;
        }
        else if (dirLocal.z > 0)
        {
            multiplicadorVelocidad = 1f;
        }

        if (!Grounded)
        {
            multiplicadorVelocidad *= 0.5f;
        }


        if (Grounded && dirLocal.z > 0 && Keyboard.current != null &&
            (Keyboard.current.leftShiftKey.isPressed || Keyboard.current.rightShiftKey.isPressed))
        {
            multiplicadorVelocidad *= 2f;
        }


        if (Grounded)
        {
            Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.green);
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = transform.position + transform.up * distance;
        }

        if (dir != Vector3.zero)
        {
            Vector3 movimiento = dir * speed * multiplicadorVelocidad * Time.deltaTime;

            rb.MovePosition(rb.position + movimiento);
        }


        if (Grounded && inputs.Player.Jump.WasPressedThisFrame())
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawSphere(hitPosition, 0.1f);
    }
}
