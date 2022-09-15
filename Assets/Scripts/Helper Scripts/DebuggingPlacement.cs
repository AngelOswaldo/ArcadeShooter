using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingPlacement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + new Vector3(1, 1, 0),
            transform.position + new Vector3(-1, 1, 0), Color.magenta);
        Debug.DrawLine(transform.position + new Vector3(1, 0, 0),
            transform.position + new Vector3(-1, 0, 0), Color.magenta);
        Debug.DrawLine(transform.position + new Vector3(0, 1, 1),
            transform.position + new Vector3(0, 1, -1), Color.magenta);
        Debug.DrawLine(transform.position + new Vector3(0, 0, 1),
            transform.position + new Vector3(0, 0, -1), Color.magenta);

        Debug.DrawLine(transform.position + new Vector3(1, 1, 1),
            transform.position + new Vector3(-1, 1, 1), Color.magenta);
        Debug.DrawLine(transform.position + new Vector3(1, 0, 1),
            transform.position + new Vector3(-1, 0, 1), Color.magenta);
        Debug.DrawLine(transform.position + new Vector3(1, 1, 1),
            transform.position + new Vector3(1, 1, -1), Color.magenta);
        Debug.DrawLine(transform.position + new Vector3(1, 0, 1),
            transform.position + new Vector3(1, 0, -1), Color.magenta);

        Debug.DrawLine(transform.position + new Vector3(1, 1, 0),
            transform.position + new Vector3(1, 0, 0), Color.magenta);
        Debug.DrawLine(transform.position + new Vector3(-1, 1, 0),
            transform.position + new Vector3(-1, 0, 0), Color.magenta);
        Debug.DrawLine(transform.position + new Vector3(0, 1, 1),
            transform.position + new Vector3(0, 0, 1), Color.magenta);
        Debug.DrawLine(transform.position + new Vector3(0, 1, -1),
            transform.position + new Vector3(0, 0, -1), Color.magenta);
    }
}
