using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterControl : MonoBehaviour
{
    UnityEngine.CharacterController characterController;
    float directionX, directionZ;
    bool reverseControl;
    Vector3 movement = Vector3.zero;

    [SerializeField]
    float speed;

    [SerializeField]
    float gravity;

    [SerializeField]
    float jumpHeight;
    //float min_force = 1f;
    //float max_force = 1000f;
    bool jump;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        gravity = 0.05f;
        jumpHeight = 0.5f;
        jump = false;
        reverseControl = false;
        characterController = GetComponent<UnityEngine.CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {


        directionX = reverseControl ? -Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal");

        directionZ = reverseControl ? -Input.GetAxis("Vertical") : Input.GetAxis("Vertical");

        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }


    }

    void FixedUpdate()
    {
        movement.x = speed * directionX * Time.fixedDeltaTime;
        movement.z = speed * directionZ * Time.fixedDeltaTime;

        if (jump)
        {
            movement.y = jumpHeight;
            jump = false;
        }


        if (jump && characterController.isGrounded)
        {
            movement.y = 0;
        }
        else
        {
            movement.y -= gravity;
        }

        characterController.Move(movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "RedBall":
                speed -= 3f; // Decrease speed
                break;
            case "GreenBall":
                speed += 3f; // Increase speed
                break;
            case "BlueBall":
                jumpHeight += 2f; // Increase jump
                break;
            case "BlackBall":
                jump = false; // Disable jump
                jumpHeight = 0;
                break;
            case "YellowBall":
                reverseControl = true;
                break;

        }

        GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;

        other.gameObject.SetActive(false);

    }
    //private void ReverseControls()
    //{
    //reverseControl = !reverseControl;
    //}



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.name == "WhiteBall")
        {
            Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f)), ForceMode.Impulse);
            }



        }



        //collidedObject.SetActive(false);

    }

}






