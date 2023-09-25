using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnusForce : MonoBehaviour
{

    [SerializeField]
    private float areaDensity;

    private List<Physics> mpList;

    void Start()
    {
        mpList = new List<Physics>();
    }

    void FixedUpdate()
    {
        foreach (Physics mp in mpList)
        {
            float planeVel = new Vector3( mp.Rigidbody.velocity.x, mp.Rigidbody.velocity.y , mp.Rigidbody.velocity.z).magnitude;
            float forceM = (mp.DragCoefficient * areaDensity * mp.CrossSectionalArea * Mathf.Pow(planeVel, 2f)) / 2;
            mp.Rigidbody.AddForce(Vector3.up * forceM);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        mpList.Add(other.GetComponent<Physics>());
    }

    private void OnTriggerExit(Collider other)
    {
        mpList.Remove(other.GetComponent<Physics>());
    }

}