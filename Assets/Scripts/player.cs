using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null; //SerializeField exposes variable to inspector but not other classes 
    [SerializeField] private LayerMask playerMask ;
    private bool jumpKeyWasPressed = false;
    private float horizontalInput = 0;
    private Rigidbody rigidbodyComponet;
    private int supperJumpsRemaining = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponet = GetComponent<Rigidbody>();//This is better practice then to keep getting the Rigidbody
    }

    // Update is called once per frame
    void Update()//Should not apply force in this update because it is based on computer (bad practice to apply forces here )
    {

        if (Input.GetKeyDown(KeyCode.Space) )
        {
            jumpKeyWasPressed = true;       
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    //fixed update is called once every physics update updates very 100 times a second 
    void FixedUpdate(){//Apply forces here 
    if (Physics.OverlapSphere(groundCheckTransform.position,0.1f,playerMask).Length != 0)
    {
        if (jumpKeyWasPressed)
        {
            float jumpPower = 5;
            if (supperJumpsRemaining >0)
            {
                jumpPower *=2;
                supperJumpsRemaining --;
            }
           rigidbodyComponet.AddForce(Vector3.up*jumpPower,ForceMode.VelocityChange);  
           jumpKeyWasPressed = false;
        }
    }
        rigidbodyComponet.velocity =new Vector3(horizontalInput, rigidbodyComponet.velocity.y,0);
    }

    private void OnCollisionEnter(Collision collision){
    }

    private void OnCollisionExit(Collision collision){
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            supperJumpsRemaining++;
        }
    }
}

