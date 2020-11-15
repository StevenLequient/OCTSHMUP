using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purpose of this class is to map the robots input to its corresponding animation
public class RobotMovement : MonoBehaviour
{
    public Animator m_animator = null;
    private float velocity_x = 0.0f;
    private bool is_idle = false;
    private bool mouse_left_click = false;
    private bool mouse_right_click = false;

    void Start() {
        m_animator = GetComponent<Animator>();
    }

    void Update() {
        GetInputs();
        SetAnimationParameters();
        MoveRobot();
        Shoot();
    }
    void GetInputs() {
        // Please see 'Edit > Project Settings > Input Manager > Axes'
        // for information on inputs "Horizontal", "Fire1", and "Fire2"
        velocity_x = Input.GetAxisRaw("Horizontal");
        mouse_left_click = Input.GetButton("Fire1");
        mouse_right_click = Input.GetButton("Fire2");
        is_idle = (velocity_x == 0.0f);
    }
    void SetAnimationParameters() {
        // Please double-click the file "Robot_Animator.controller" to Animator window
        // Then in the Animator window, click on Parameters tab in the top-left
        m_animator.SetFloat("velocity_x", velocity_x);
        m_animator.SetBool("is_idle", is_idle);
        m_animator.SetBool("mouse_left_click", mouse_left_click);
        m_animator.SetBool("mouse_right_click", mouse_right_click);
    }
    void MoveRobot() {
        transform.position += Vector3.right * velocity_x * Time.deltaTime;
    }
    void Shoot() {
        if (mouse_left_click) {
            // Insert your left gun shooting code here
            ;
        }
        else if (mouse_right_click) {
            // Insert your right gun shooting code here
            ;
        }
    }
}
