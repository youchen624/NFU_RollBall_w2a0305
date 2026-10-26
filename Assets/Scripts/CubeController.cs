using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    float xAmount;
    float zAmount;

    float moveSpeed = 20f;
    float rotateSpeed = 100f;


    float healthMax = 10;
    float health;
    [SerializeField] HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //healthBar = GetComponentInChildren<HealthBar>();
        //Debug.Log("find healthBar");
        health = healthMax;
        healthBar.HealthBarChange(health, healthMax);
    }

    // Update is called once per frame
    void Update()
    {
        xAmount = Input.GetAxis("Horizontal");
        zAmount = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(0, 0, zAmount) * Time.deltaTime * moveSpeed);
        transform.RotateAround(transform.position, Vector3.up, xAmount * Time.deltaTime * rotateSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(health);
            if (health > 0)
            {
                health -= 1;
                if (health < 3)
                    healthBar.HealthBarColorChange(Color.red);
                healthBar.HealthBarChange(health, healthMax);
            }
        }
    }
}
