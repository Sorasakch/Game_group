using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    private Rigidbody rb;

    public int ballspeed = 2;
    public int jumpspeed = 5;
    private bool istouching = false;

    public int sizeOfcoin = 10;
    private int currentScore;

   
    private GameObject[] coins;

    public float currentTime = 0f;
    public float maxTime = 10f;

public AudioClip collectSound;
    private AudioSource audioSource;
    public TMPro.TextMeshProUGUI textUI;
    public TMPro.TextMeshProUGUI textUITimer;

    void Start()
    {
                audioSource = GetComponent<AudioSource>();

        currentTime = maxTime;
        rb = GetComponent<Rigidbody>();
            textUI.text = "Score" + currentScore;
        textUITimer.text = "Time:" + currentTime;

        coins = new GameObject[sizeOfcoin];

     
    }

    void Update()
{
    textUI.text = "Score:" + currentScore;
    textUITimer.text = "Time: " + currentTime.ToString("F2");
    currentTime -= Time.deltaTime;
    if (currentTime <= 0)
    {
        currentTime = 0;
        PauseGame();
    }

    // ตรวจสอบว่าลูกบอลตกลงจากพื้นหรือไม่
    if (transform.position.y < -5) // กำหนดระดับความสูงที่ต้องการ
    {
        PauseGame();
        Debug.Log("Ball fell off the platform! Game Paused!");
    }

    // การควบคุมการเคลื่อนที่และกระโดด
    if (Input.GetKey(KeyCode.A))
    {
        rb.AddForce(new Vector3(-1f * ballspeed, 0f, 0f), ForceMode.Force);
        if (Input.GetKeyDown(KeyCode.Space) && istouching)
        {
            rb.AddForce(new Vector3(0f, 1f * jumpspeed, 0f), ForceMode.Impulse);
        }
    }

    if (Input.GetKey(KeyCode.D))
    {
        rb.AddForce(new Vector3(1f * ballspeed, 0f, 0f), ForceMode.Force);
        if (Input.GetKeyDown(KeyCode.Space) && istouching)
        {
            rb.AddForce(new Vector3(0f, 1f * jumpspeed, 0f), ForceMode.Impulse);
        }
    }

    if (Input.GetKeyDown(KeyCode.Space) && istouching && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
    {
        rb.AddForce(new Vector3(0f, 1f * jumpspeed, 0f), ForceMode.Impulse);
    }
}


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor") || collision.gameObject.CompareTag("movingPlatform"))
        {
            istouching = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            istouching = true;
        }

        // หากชนกับแพลตฟอร์มที่ขยับได้ ให้ตั้ง Parent เป็นแพลตฟอร์มนั้น
        if (collision.gameObject.CompareTag("movingPlatform"))
        {
            istouching = true;
            transform.parent = collision.transform;
        }


        // หากถึงเส้นชัย ให้ชนะ
        if (collision.gameObject.CompareTag("finish"))
        {
         Wingame();
        }
    }


    

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor") || collision.gameObject.CompareTag("movingPlatform"))
        {
            istouching = false;
        }

        // หากออกจากแพลตฟอร์มที่ขยับได้ ให้ปลด Parent ออก
        if (collision.gameObject.CompareTag("movingPlatform"))
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("collect"))
        {
            CollectCoin(other.gameObject);
        }
    }

    private void CollectCoin(GameObject coin)
    {
        currentScore++;
        coin.SetActive(false);
    // เล่นเสียงเมื่อเก็บเหรียญ
    if (collectSound != null && audioSource != null)
    {
        audioSource.PlayOneShot(collectSound);
    }


    }


    void PauseGame()
    {
        // หยุดเกมชั่วคราวเมื่อหมดเวลา
        Time.timeScale = 0;
        Debug.Log("Game Paused!");
    }

        void Wingame()
    {
        
        Time.timeScale = 0;
        Debug.Log("Game win!");
    }
}
