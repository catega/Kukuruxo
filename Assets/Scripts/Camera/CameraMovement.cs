using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(0, 4)] private float offset = 0f;
    [Header("Smooth Movement"), Space]
    [SerializeField] private bool smooth = true;
    [SerializeField, Range(0, 10)] private float smoothness = 1f;
    private bool freeMode = false;
    private Vector3 _target;
    private Vector3 cameraGroundPosition;


    void Update()
    {
        cameraGroundPosition = new Vector3(transform.position.x, transform.position.y, 0);

        if ( Input.GetKeyDown(KeyCode.C) ) freeMode = !freeMode;

        if ( !freeMode ) _target = new Vector3(target.position.x, target.position.y, -10);
        else {
            Vector3 middlePoint = Utils.GetMousePosition() + target.position;
            float mouseDistance = (Utils.GetMousePosition() - target.position).magnitude;
            
            //TODO: Poner distancia límite a la que se puede mover la cámara
            // if ( mouseDistance <= 15 ) _target = middlePoint / 2 + new Vector3(0, 0, -10);
            _target = middlePoint / 2 + new Vector3(0, 0, -10);
            moveCamera();
        }

        if ( !freeMode && (cameraGroundPosition - target.position).magnitude > offset ) {
            moveCamera();
        }

    }

    void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere((Utils.GetMousePosition() + target.position) / 2 + new Vector3(0, 0, -10), .5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(cameraGroundPosition, offset);
    }

    private void moveCamera() {
        transform.position = smooth ? 
            Vector3.Lerp(transform.position, _target, (11 - smoothness) * Time.deltaTime)
            : target.position + new Vector3(0, 0, -10);
    }
}
