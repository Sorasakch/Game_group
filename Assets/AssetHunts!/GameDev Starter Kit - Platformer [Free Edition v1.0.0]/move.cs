using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    public float moveSpeed = 2.0f;
    public float jumpForce = 5.0f;
    private bool isGrounded = true;

    void Start()
    {
        // เข้าถึง Animator Component ที่อยู่ในตัวละคร
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // การเคลื่อนที่ไปทางซ้าย
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isMovingLeft", true);
            animator.SetBool("isMovingRight", false);
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        // การเคลื่อนที่ไปทางขวา
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isMovingRight", true);
            animator.SetBool("isMovingLeft", false);
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            // หยุดเคลื่อนไหว
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", false);
        }

        // การกระโดด
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // การตรวจสอบการตกลงมา
        if (rb.velocity.y < 0)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ตั้งค่า isGrounded เป็น true เมื่อสัมผัสพื้น
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }
}
