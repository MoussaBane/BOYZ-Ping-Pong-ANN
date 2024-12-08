using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    // Stores the initial position of the ball
    Vector3 ballStartPosition;

    // Reference to the Rigidbody2D component of the ball
    Rigidbody2D rb;

    // Speed of the ball when launched
    float speed = 400;

    // Audio sources for sound effects when the ball hits different objects
    public AudioSource blip; // Played when hitting anything other than the backwall
    public AudioSource blop; // Played when hitting the backwall

    // Called when the script instance is loaded
    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        rb = this.GetComponent<Rigidbody2D>();

        // Save the initial position of the ball for resetting purposes
        ballStartPosition = this.transform.position;

        // Initialize the ball's state
        ResetBall();
    }

    // Called when the ball collides with another GameObject
    void OnCollisionEnter2D(Collision2D col)
    {
        // Check if the ball collided with the object tagged as "backwall"
        if (col.gameObject.tag == "backwall")
            blop.Play(); // Play the "blop" sound effect
        else
            blip.Play(); // Play the "blip" sound effect for other collisions
    }

    // Resets the ball's position and launches it in a random direction
    public void ResetBall()
    {
        // Reset the ball's position to its initial position
        this.transform.position = ballStartPosition;

        // Stop any current movement of the ball
        rb.velocity = Vector3.zero;

        // Generate a random direction for the ball
        Vector3 dir = new Vector3(Random.Range(100, 300), Random.Range(-100, 100), 0).normalized;

        // Apply a force to the ball to launch it
        rb.AddForce(dir * speed);
    }

    // Called once per frame
    void Update()
    {
        // Check if the "space" key is pressed
        if (Input.GetKeyDown("space"))
        {
            // Reset the ball when the space key is pressed
            ResetBall();
        }
    }
}
