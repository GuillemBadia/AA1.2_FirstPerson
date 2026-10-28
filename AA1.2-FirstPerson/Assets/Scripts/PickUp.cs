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

    

    void Update()
    {

        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, Pickup))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green);

            hitPosition = hit.point;

         
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                objetoCogido = hit.collider.gameObject;
                rbObjeto = objetoCogido.GetComponent<Rigidbody>();

                distanciaOriginal = hit.distance;

                rbObjeto.isKinematic = true;
                
            }
        }

        if (objetoCogido != null)
        {
            objetoCogido.transform.position = transform.position + transform.forward * distanciaOriginal;


            distanciaOriginal += Mouse.current.scroll.ReadValue().y;
            if (distanciaOriginal < DistanciaMin)
            {
                distanciaOriginal = DistanciaMin;
            }
            if (distanciaOriginal > DistanciaMax)
            {
                distanciaOriginal = DistanciaMax;
            }



            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                objetoCogido = null;


                rbObjeto.isKinematic = false;
                
                rbObjeto = null;


            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(hitPosition, 0.1f);
    }
}