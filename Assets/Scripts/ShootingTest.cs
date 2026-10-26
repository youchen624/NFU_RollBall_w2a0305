using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTest : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject fireball;

    float turnAmount;
    float trunSpeed = 0.5f;

    float lastAltTime;
    float allHoldTime = 4;
    void Start()
    {
        lastAltTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(fireball, muzzle.position, muzzle.rotation);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if ((Time.time - lastAltTime) > allHoldTime)
            {
                print("I am XO");
                Instantiate(fireball, muzzle.position, muzzle.rotation);
            }
        } else {
            lastAltTime = Time.time;
        }

            turnAmount = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * trunSpeed * turnAmount);
    }
}
