using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private Transform ballStartPos;

    [SerializeField]
    private float force;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject go = (GameObject)Instantiate(ball, ballStartPos.position, Quaternion.identity);
            //go.GetComponent<Rigidbody>().AddForce(Vector3.left * force, ForceMode.Impulse);
        }
    }

}
