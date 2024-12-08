using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_sc : MonoBehaviour
{
    public GameObject paddle; // Reference to the paddle GameObject
    public GameObject ball;   // Reference to the ball GameObject
    Rigidbody2D brb;          // Rigidbody2D component of the ball

    float yvel;               // Current y-axis velocity for paddle movement
    float paddleMinY = 8.8f;  // Minimum y-position for the paddle
    float paddleMaxY = 17.4f; // Maximum y-position for the paddle
    float paddleMaxSpeed = 15; // Maximum speed at which the paddle can move

    public float numSaved = 0; // Counter for the number of balls successfully intercepted
    public float numMissed = 0; // Counter for the number of balls missed

    ANN ann; // Artificial Neural Network instance

    // Called when the script is initialized
    void Start()
    {
        // Initialize the neural network with 6 input neurons, 1 output neuron,
        // 1 hidden layer, 4 neurons in the hidden layer, and a learning rate of 0.11
        ann = new ANN(6, 1, 1, 4, 0.11);

        // Get the Rigidbody2D component of the ball
        brb = ball.GetComponent<Rigidbody2D>();
    }

    // Runs the neural network with inputs and expected outputs
    // Parameters:
    // - bx, by: Ball's x and y positions
    // - bvx, bvy: Ball's x and y velocities
    // - px, py: Paddle's x and y positions
    // - pv: Expected paddle velocity
    // - train: Whether the network should train on this input/output pair
    List<double> Run(double bx, double by, double bvx, double bvy, double px, double py, double pv, bool train)
    {
        List<double> inputs = new List<double>();  // Neural network inputs
        List<double> outputs = new List<double>(); // Neural network expected outputs

        // Add inputs
        inputs.Add(bx);
        inputs.Add(by);
        inputs.Add(bvx);
        inputs.Add(bvy);
        inputs.Add(px);
        inputs.Add(py);

        // Add expected output
        outputs.Add(pv);

        // Train the network if specified, otherwise calculate the output
        if (train)
            return ann.Train(inputs, outputs); // Train and return error
        else
            return ann.CalcOutput(inputs, outputs); // Calculate the output
    }

    // Called once per frame
    void Update()
    {
        // Calculate the new y-position of the paddle based on the velocity
        float posy = Mathf.Clamp(
            paddle.transform.position.y + (yvel * Time.deltaTime * paddleMaxSpeed),
            paddleMinY,
            paddleMaxY);

        // Update the paddle's position
        paddle.transform.position = new Vector3(
            paddle.transform.position.x,
            posy,
            paddle.transform.position.z);

        // List to hold the neural network output
        List<double> output = new List<double>();

        // Raycasting to predict the ball's trajectory
        int layerMask = 1 << 9; // Only detect objects in layer 9
        RaycastHit2D hit = Physics2D.Raycast(ball.transform.position, brb.velocity, 1000, layerMask);

        // If the raycast hits something
        if (hit.collider != null)
        {
            // If it hits the "tops" tag, calculate the reflection
            if (hit.collider.gameObject.tag == "tops")
            {
                Vector3 reflection = Vector3.Reflect(brb.velocity, hit.normal);
                hit = Physics2D.Raycast(hit.point, reflection, 1000, layerMask);
            }

            // If it hits the "backwall" tag
            if (hit.collider != null && hit.collider.gameObject.tag == "backwall")
            {
                // Calculate the vertical difference between the paddle and the predicted hit point
                float dy = (hit.point.y - paddle.transform.position.y);

                // Run the neural network to determine the paddle's velocity
                output = Run(
                    ball.transform.position.x,
                    ball.transform.position.y,
                    brb.velocity.x,
                    brb.velocity.y,
                    paddle.transform.position.x,
                    paddle.transform.position.y,
                    dy,
                    true);

                // Set the paddle's y-velocity to the neural network's output
                yvel = (float)output[0];
            }
        }
        else
        {
            // If no object is hit, stop paddle movement
            yvel = 0;
        }
    }
}
