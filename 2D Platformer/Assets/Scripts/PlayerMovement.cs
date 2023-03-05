using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float move_speed = 2;

    [Header("Jump")]
    [SerializeField] float jump_speed = 1000;
    
    void Update()
    {
        //TODO: move using the newest button clicked

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(move_speed * Time.deltaTime * Vector2.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(move_speed * Time.deltaTime * Vector2.left);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //need to check if on ground

            transform.position = Vector2.Lerp(transform.position, transform.position + transform.up, jump_speed * Time.deltaTime);
        }
    }
}