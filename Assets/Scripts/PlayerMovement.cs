using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity = 0.1f;

    [Header("Gravity")]
    public float gravity;
    private float currentGravity;
    public float constantGravity;
    public float maxGravity;
    private Vector3 gravityMovement;
    
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        CalculateGravity();
        Movement();
    }

    #region Movement

    private void Movement() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward) + gravityMovement;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    #endregion

    #region Gravity

    private bool IsGrounded() {
        return controller.isGrounded;
    }

    private void CalculateGravity() {
        if (IsGrounded()) {
            currentGravity = constantGravity;
        }
        else if (currentGravity > maxGravity) {
                currentGravity -= gravity * Time.deltaTime;
        }

        gravityMovement = Vector3.down * -currentGravity;
    }

    #endregion

}
