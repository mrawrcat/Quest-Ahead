using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector2 movement, normMovement;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        normMovement = movement.normalized;
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + normMovement * speed * Time.fixedDeltaTime);
    }

}
