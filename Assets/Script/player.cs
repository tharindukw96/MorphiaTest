using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    private float inputDirection;
    private float verticalVelocity;
    private float gravity = 30.0f;
    private Vector3 moveVector;
    private Vector3 lastMove;
    private float speed = 5.0f;
    private float jumpForce = 10;
    private bool secondJumpAvail = false;
    private CharacterController controller;
    // Use this for initialization
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        inputDirection = Input.GetAxis("Horizontal" ) * speed;
        moveVector = Vector3.zero;
        if (IsControllerGrounded())
        {
            verticalVelocity = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
                secondJumpAvail = true;
            }
            moveVector.x = inputDirection;
        }
        else {
            if (secondJumpAvail)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    verticalVelocity = jumpForce;
                    secondJumpAvail = false;
                    
                }
            }
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector.x = lastMove.x;
        }
        //moveVector = new Vector3(inputDirection, verticalVelocity, 0);
        moveVector.y = verticalVelocity;
        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;
    }

    private bool IsControllerGrounded()
    {
        Vector3 leftRayStart;
        Vector3 rightRayStart;

        leftRayStart = controller.bounds.center;
        rightRayStart = controller.bounds.center;

        leftRayStart.x -= controller.bounds.extents.x;
        rightRayStart.x += controller.bounds.extents.x;
        Debug.DrawRay(leftRayStart, Vector3.down, Color.red);
        Debug.DrawRay(rightRayStart, Vector3.down, Color.green);
        if (Physics.Raycast(leftRayStart, Vector3.down, (controller.height / 2) + 0.1f)) {
            return true;
        }
        if (Physics.Raycast(rightRayStart, Vector3.down, (controller.height / 2) + 0.1f))
        {
            return true;
        }
        return false;

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (controller.collisionFlags == CollisionFlags.Sides) {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.DrawRay(hit.point, hit.normal, Color.red, 2.0f);
                moveVector = hit.normal * speed;
                verticalVelocity = jumpForce;
                secondJumpAvail = true;
            }

        }
        switch (hit.gameObject.tag) {
            case "Coin":
                levelManager.instance.collectCoin();
                Destroy(hit.gameObject);

                break;
            case "JumpPad":
                verticalVelocity = jumpForce * 2;
                break;
            case "Telepoter":
                transform.position = hit.transform.GetChild(0).position;
                break;
            case "winposition":
                levelManager.instance.win();
                break;
            default:
                break;
        

        }
            
    }
}
