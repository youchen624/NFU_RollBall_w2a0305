using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    public float speed = 1f;
    public int winCount = 5;
    public GameObject MysteryBoxParticlePrefab;
    public GameObject BumpParticlePrefab;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public GameObject sfx;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //txt = GetComponent<TextMeshProUGUI>();
        if (!rb) Debug.Log("Player RB Not Found.");
        scoreText.text = "count = " + count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 movement = new(movementX, 0.0f, movementY);
        // if (Input.GetKey(KeyCode.Space)) movement /= -2;
        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    /*
    void OnStop(InputValue v)
    {
        bool b = v.Get<bool>();
        if (b) movementX = movementY = 0;
        else return;
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MysteryBox"))
        {
            other.gameObject.SetActive(false);
            GameObject obj = Instantiate(MysteryBoxParticlePrefab, other.transform.position, Quaternion.identity);
            sfx.GetComponent<SFXManager>().playEating();
            ++count;
            scoreText.text = "count = " + count;
            if (count >= winCount)  // win
            {
                winText.gameObject.SetActive(true);
                sfx.GetComponent<SFXManager>().playWin();
                GameObject.FindGameObjectWithTag("Enemy").SetActive(false);
            }
            // Debug.Log($"count = {count}");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))   // lose
        {
            gameObject.SetActive(false);
            winText.text = "YOU LOSE";
            winText.gameObject.SetActive(true);
            sfx.GetComponent<SFXManager>().playLose();

        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameObject obj = Instantiate(BumpParticlePrefab, transform.position, Quaternion.identity);
            Destroy(obj, 2f);
            sfx.GetComponent<SFXManager>().playBumping();
        }
    }
}
