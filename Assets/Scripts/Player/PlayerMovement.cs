using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float skinWidth = .01f;
    [SerializeField] private LayerMask layerMask;
    private CircleCollider2D playerCollider;
    private Vector2 direction;
    private RaycastHit2D hit;

    void Start() {
        playerCollider = GetComponent<CircleCollider2D>();
    }

    void Update() {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        direction = new Vector2(x, y).normalized;

        Vector2 movement = direction * speed * Time.deltaTime;

        Vector2 finalPos = Vector2.zero;
        Vector2 newVelocity = movement; //! Para ir recalculando la distancia del vector

        while ( newVelocity != Vector2.zero ) {

            float rayLength = newVelocity.magnitude + skinWidth;

            hit = Physics2D.CircleCast( transform.position + (Vector3)finalPos, playerCollider.radius, newVelocity.normalized, rayLength, layerMask );
            if ( hit ) {

                Debug.Log($"Original newMovement: {newVelocity}");
                Debug.Log($"Hit distance:{hit.distance}");
                finalPos += newVelocity.normalized * (hit.distance - skinWidth); //! Distancia hasta la colisión
                Debug.Log($"finalPos: {finalPos}");
                // Debug.DrawRay(transform.position, finalPos, Color.black, 1f);
                newVelocity -= newVelocity.normalized * (hit.distance - skinWidth); //! Lo que sobra de la colisión
                Debug.Log($"Modificada movement: {newVelocity}");

                if ( Vector2.Dot( direction, hit.normal ) >= -0.9 ) {

                    // Debug.DrawRay(transform.position, hit.normal, Color.yellow, 1f);
                    // Debug.DrawRay(transform.position, newVelocity.normalized, Color.red, 1f);
                    float align = Vector2.Dot(hit.normal, newVelocity.normalized);
                    Debug.Log($"Align: {align}");
                    Vector2 reposition = newVelocity.normalized - hit.normal * align;
                    // Debug.DrawRay(transform.position, reposition.normalized, Color.green, 1f);
                    newVelocity = reposition.normalized * newVelocity.magnitude;
                    // Debug.Break();

                }
                else {
                    newVelocity = Vector2.zero;
                }

            } else {
                finalPos += newVelocity;
                newVelocity = Vector2.zero;
            }

        }

        transform.Translate(finalPos);

    }
}
