using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayer : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.y > transform.position.y)
        {
            sprite.sortingOrder = 4;
        }
        else if (player.position.y < transform.position.y)
        {
            sprite.sortingOrder = 0;
        }
    }
}
