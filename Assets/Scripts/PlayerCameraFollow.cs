using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    Vector3 offset;
    public GameObject player;
    public bool enable_orbitMode = true;

    public float radius = 8f;
    public float speed = 1f;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) enable_orbitMode = !enable_orbitMode;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        if (enable_orbitMode)
            transform.position = player.transform.position + new Vector3(x, 2, z);
        else
            transform.position = offset + player.transform.position;
        transform.LookAt(player.transform);
    }
}
