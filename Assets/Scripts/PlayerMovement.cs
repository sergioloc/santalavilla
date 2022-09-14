using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity = 0.1f;
    
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0f, v).normalized;

        if (direction.magnitude > 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
        }
    }

}
