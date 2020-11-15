using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purpose of this class is to map the robots input to its corresponding animation
public class Player : MonoBehaviour
{
    public Animator Animator = null;
    public Rigidbody2D Rb;
    private Vector2 movement;
    private bool isIdle = false;
    private bool mouseLeftClick = false;
    private bool mouseRightClick = false;

    private Transform firePointLeft;
    private Transform firePointRight;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private float weaponCooldown = 0.5f;
    private float leftWeaponLastShot;
    private float rightWeaponLastShot;

    void Start() {
        Animator = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        firePointLeft = transform.Find("FirePointLeft");
        firePointRight = transform.Find("FirePointRight");
    }

    void Update() {
        GetInputs();
    }
    void GetInputs() {
        // Please see 'Edit > Project Settings > Input Manager > Axes'
        // for information on inputs "Horizontal", "Fire1", and "Fire2"
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mouseLeftClick = Input.GetButton("Fire1");
        mouseRightClick = Input.GetButton("Fire2");
        isIdle = (movement.x == 0.0f && movement.y == 0.0f);
    }
    void SetAnimationParameters() {
        // Please double-click the file "Robot_Animator.controller" to Animator window
        // Then in the Animator window, click on Parameters tab in the top-left
        Animator.SetFloat("velocity_x", movement.x);
        Animator.SetBool("is_idle", isIdle);
        Animator.SetBool("mouse_left_click", mouseLeftClick);
        Animator.SetBool("mouse_right_click", mouseRightClick);
    }
    void MoveRobot() {
        Rb.MovePosition(Rb.position + movement * ShmupController.Instance.PlayerSpeed * Time.fixedUnscaledDeltaTime);
    }
    void Shoot() {
        if (mouseLeftClick && Time.fixedTime > leftWeaponLastShot + weaponCooldown) {
            ShootLeftWeapon();
            leftWeaponLastShot = Time.fixedTime;
        }
        if (mouseRightClick && Time.fixedTime > rightWeaponLastShot + weaponCooldown) {
            ShootRightWeapon();
            rightWeaponLastShot = Time.fixedTime;
        }
    }

    void ShootLeftWeapon()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
        bullet.transform.SetParent(ShmupController.Instance.transform);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletForce;
    }

    void ShootRightWeapon()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);
        bullet.transform.SetParent(ShmupController.Instance.transform);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletForce;
    }

    void Hit()
    {
        TetrisController.Instance.Damage();
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.collider.GetComponent<Enemy>() != null)
        {
            Hit();
        }
    }

    void FixedUpdate()
    {
        SetAnimationParameters();
        Shoot();
        MoveRobot();
    }
}
