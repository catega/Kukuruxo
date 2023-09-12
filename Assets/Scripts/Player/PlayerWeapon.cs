using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool autoMode = true;
    private Collider2D[] detection;
    private Vector3 target;

    void Update() {
        if ( Input.GetKeyDown(KeyCode.C) ) autoMode = !autoMode;
    }

    void FixedUpdate()
    {

        if ( autoMode ) {
            detection = Physics2D.OverlapCircleAll( transform.position, detectionRadius, layerMask );

            if ( detection.Length > 0 ) {
                target = getClosestEnemy(detection);
            }
        } else {
            Camera camera = Camera.main;

            Vector3 mousePosition = Input.mousePosition;
            target = camera.ScreenToWorldPoint(mousePosition) - new Vector3(0, 0, camera.transform.position.z);
        }

        transform.up = target - transform.position;
    }

    Vector3 getClosestEnemy(Collider2D[] detections) {
        Collider2D closest = detections[0];
        float minDistance = (detections[0].transform.position - transform.position).sqrMagnitude;
        foreach (var item in detections)
        {
            float currentDistance = (item.transform.position - transform.position).sqrMagnitude;

            if ( minDistance > currentDistance ) {
                minDistance = currentDistance;
                closest = item;
            };
        }

        return closest.transform.position;
    }

    private void OnDrawGizmos() {
        Gizmos.color = detection.Length > 0 ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);    
    }
}
