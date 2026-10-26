using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class LookAtCam : MonoBehaviour
{
    [SerializeField] Camera _cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _cam.transform.forward, _cam.transform.up);
    }
}
