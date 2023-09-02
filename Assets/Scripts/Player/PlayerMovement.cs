using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private LayerMask collisionLayers;
    private BoxCollider2D playerCollider;
    private RaycastHit2D hit;
    private Vector2 direction;

    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        direction = new Vector2(horizontalMovement, verticalMovement).normalized;
        Vector2 movement = direction * speed * Time.deltaTime;

        hit = Physics2D.BoxCast(transform.position, playerCollider.size, 0, new Vector2(0, direction.y).normalized, Mathf.Abs(movement.y), collisionLayers);
        if ( hit.collider == null ) transform.Translate(0, movement.y, 0);
        hit = Physics2D.BoxCast(transform.position, playerCollider.size, 0, new Vector2(direction.x, 0).normalized, Mathf.Abs(movement.x), collisionLayers);
        if ( hit.collider == null ) transform.Translate(movement.x, 0, 0);
    }
}
