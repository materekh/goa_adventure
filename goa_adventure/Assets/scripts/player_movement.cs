using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Windows;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource normal;
    [SerializeField] private AudioSource sprinting;
    private Vector2 movementDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprinting.Stop();
        normal.Stop();
    }


    void Update()
    {
        movementDirection = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
        // Debug.Log(movementDirection.magnitude);
        if (movementDirection.magnitude != 0f && movementSpeed == 5f && !normal.isPlaying)
        {
            sprinting.Stop();
            normal.Play();
        }
        else if (movementDirection.magnitude == 0f && normal.isPlaying)
        {
            normal.Stop();
            sprinting.Stop();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movementSpeed * movementDirection;

        if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
        {
            sprint();
            if (movementDirection.magnitude != 0f && movementSpeed == 7f && !sprinting.isPlaying)
            {
                normal.Stop();
                sprinting.Play();
            }
            else if (movementSpeed == 5f && sprinting.isPlaying)
            {
                sprinting.Stop();
            }
        }
        else if (UnityEngine.Input.GetKey(KeyCode.LeftControl))
        {
            crouch();
            if (movementDirection.magnitude == 3f && sprinting.isPlaying)
            {
                sprinting.Stop();
            }
        }
        else
        {
            movementSpeed = 5f;
        }
    }

    void sprint()
    {
        movementSpeed = 7f;
    }
    void crouch()
    {
        movementSpeed = 3f;
    }
}