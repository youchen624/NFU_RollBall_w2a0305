using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMov : MonoBehaviour
{
    [SerializeField] GameObject SFX;
    [SerializeField] GameObject obstacleSpark;
    float moveSpeed = 10.0f;

    float spawn_time;

    void Awake()
    {
        SFX = GameObject.FindWithTag("SFX");
    }

    private void Start()
    {
        spawn_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 10));
        if (spawn_time + 5 < Time.time) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) {
            GameObject spark = Instantiate(obstacleSpark, transform.position, Quaternion.identity);
            SFX.GetComponent<SFXManager>().playEating(); print("sound..");
            Destroy(spark, 1.5f);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Enemy")) {
            (collision.gameObject.GetComponentInParent<EnemyMovement>()).HitBy_Fireball();
            GameObject spark = Instantiate(obstacleSpark, transform.position, Quaternion.identity);
            SFX.GetComponent<SFXManager>().playEating(); print("sound..");
            Destroy(spark, 1.5f);
            Destroy(gameObject);
        }
        //Destroy(gameObject);
    }
}
