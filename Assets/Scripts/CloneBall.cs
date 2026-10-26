using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBall : MonoBehaviour
{
    public GameObject ballPrefab;

    public float explosionForce = 8f;
    public float explosionRadius = 4f;
    public float spawnOffset = 0.1f;
    public static int ballMaximum = 640;
    static private int currentBalls = 0;

    public float dupCoolDownTime = 3f;
    private float nextDupAllowTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("CloneBall")) return;
        if (Time.time < nextDupAllowTime) return;
        if (currentBalls >= ballMaximum) return;
        ContactPoint contact = collision.contacts[0];
        Vector3 explosionPos = contact.point;
        Collider[] cols = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider c in cols) {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(
                    explosionForce,
                    explosionPos,
                    explosionRadius
                    );
            }
        }
        Instantiate(ballPrefab, explosionPos, Quaternion.identity);
        nextDupAllowTime = Time.time + dupCoolDownTime;
        ++currentBalls;
    }
    // Start is called before the first frame update
    void Start()
    {
        nextDupAllowTime = Time.time + dupCoolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
