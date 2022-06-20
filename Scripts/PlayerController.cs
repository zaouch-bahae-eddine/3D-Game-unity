using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2.5f;//The distance between tow lanes
    public float maxSpeed;

    public float jumpForce;
    public float gravity = -20;

    //Animation
    public Animator animator;
    public bool isGrounded;
    private bool isSliding = false;
    public float slideDuration = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;
        // Augmenter la vitesse
        if (forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;
        animator.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        //saute
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jump();
                animator.SetBool("isGrounded", false);
            }
        } else
        {
            direction.y += gravity * Time.deltaTime;
            animator.SetBool("isGrounded", true);
        }

        // changement de positionement

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            StartCoroutine(Slide());
        }
        //Position suivante

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        } else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        controller.Move(direction * Time.deltaTime);
    }


    private void jump()
    {
        direction.y = jumpForce;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        yield return new WaitForSeconds(0.25f / Time.timeScale);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds((slideDuration - 0.25f) / Time.timeScale);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;

        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}
