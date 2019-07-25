using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    private GameObject target = null;
    private Vector3 offset;
    void Start()
    {
        target = null;
    }   

    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }

    void Update()
    {

        transform.Translate(new Vector3(0.2f, 0, 0)*Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        target = collision.gameObject;
        offset = target.transform.position - transform.position;
    }

    private void OnCollisionExit(Collision collision)
    {
        target = null;
    }


}
