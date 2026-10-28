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
    bool enSuelo;


    void Start()
    {
        inputs = new InputSystem_Actions();
        inputs.Enable();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
         Vector3 dir = inputs.Player.Move.ReadValue<Vector2>();


        dir.z = dir.y;
        dir.y = 0;


        if (dir.x < 0)
        {

        }


        dir = transform.TransformDirection(dir);

        RaycastHit hit;

        Debug.DrawRay(transform.position, -transform.up * distance, Color.red);

        enSuelo = Physics.Raycast(transform.position, -transform.up, out hit, distance);

        if (enSuelo)
        {
            Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.green);
            hitPosition = hit.point;

            if (dir != Vector3.zero)
            {
                Debug.Log(dir);


                Vector3 movimiento = dir * speed * Time.deltaTime;
                rb.MovePosition(rb.position + movimiento);
            }

            if (inputs.Player.Jump.WasPressedThisFrame())
            {
                Jump();
            }
        }
        else
        {
            hitPosition = transform.position - -transform.up * distance;
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