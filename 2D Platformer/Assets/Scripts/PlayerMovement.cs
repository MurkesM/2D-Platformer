using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float move_speed = 10;
    [SerializeField] float sprint_speed = 15;

    [Header("Jump")]
    [SerializeField] float jump_speed = 1000;
    [SerializeField] float jump_height = 2;
    Vector3 jump_height_vector;

    void Awake()
    {
        jump_height_vector = new Vector3(0, jump_height, 0);
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
    }

    void HandleMovement()
    {
        //TODO: move using the newest button pressed as your direction instead of defaulting to right

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                transform.Translate(sprint_speed * Time.deltaTime * Vector2.right);
            else
                transform.Translate(move_speed * Time.deltaTime * Vector2.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                transform.Translate(sprint_speed * Time.deltaTime * Vector2.left);
            else
                transform.Translate(move_speed * Time.deltaTime * Vector2.left);
        }
    }

    void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //need to check if on ground
            transform.Translate(jump_speed * Time.deltaTime * jump_height_vector);
        }
    }
}