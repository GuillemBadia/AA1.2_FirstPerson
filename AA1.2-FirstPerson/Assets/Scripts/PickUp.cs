
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    public Vector3 direction;
    public float distance;

    Vector3 hitPosition;

    public LayerMask Pickup;

    GameObject objetoCogido;
    Rigidbody rbObjeto;

    public float distanciaOriginal;
    public float DistanciaMin;
    public float DistanciaMax;

    public float fuerzaMovimiento = 20f;
    public float amortiguacion = 5f;

    void Update()
    {

        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);

        if (objetoCogido == null && Mouse.current.leftButton.wasPressedThisFrame && Physics.Raycast(transform.position, transform.forward, out hit, distance, Pickup))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);

            hitPosition = hit.point;

            objetoCogido = hit.collider.gameObject;
            rbObjeto = objetoCogido.GetComponent<Rigidbody>();

            distanciaOriginal = hit.distance;
            rbObjeto.isKinematic = false;
            rbObjeto.useGravity = false;

        }

        if (objetoCogido != null)
        {
            distanciaOriginal += Mouse.current.scroll.ReadValue().y * 0.5f;
            distanciaOriginal = Mathf.Clamp(distanciaOriginal, DistanciaMin, DistanciaMax);

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                SoltarObjeto();
            }
        }
    }

    void FixedUpdate()
    {
        if (objetoCogido == null || rbObjeto == null) return;

        Vector3 posicionObjetivo = transform.position + transform.forward * distanciaOriginal;

        Vector3 diferencia = posicionObjetivo - rbObjeto.position;

        Vector3 fuerza = diferencia * fuerzaMovimiento - rbObjeto.linearVelocity * amortiguacion;

        rbObjeto.AddForce(fuerza, ForceMode.Acceleration);
    }

    void SoltarObjeto()
    {
        rbObjeto.useGravity = true;

        objetoCogido = null;
        rbObjeto = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(hitPosition, 0.1f);
    }
}
