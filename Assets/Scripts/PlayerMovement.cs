using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float collistionWidth = .1f;
    [SerializeField] private LayerMask collisionLayers;
    private new BoxCollider2D collider;
    private RaycastHit2D hit;
    private Vector2 movement;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontalMovement, verticalMovement).normalized;
        
        hit = Physics2D.BoxCast(transform.position, collider.size, 0, new Vector2(0, movement.y), Mathf.Abs(movement.y * speed * Time.deltaTime), collisionLayers);
        if ( hit.collider == null ) transform.Translate(0, movement.y * speed * Time.deltaTime, 0);
        hit = Physics2D.BoxCast(transform.position, collider.size, 0, new Vector2(movement.x, 0), Mathf.Abs(movement.x * speed * Time.deltaTime), collisionLayers);
        Debug.Log(hit.point);
        if ( hit.collider == null ) transform.Translate(movement.x * speed * Time.deltaTime, 0, 0);
    }

    private void FixedUpdate() {
    }

}
