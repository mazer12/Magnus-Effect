using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraChange : MonoBehaviour
{

    public GameObject MainCamera;
    public GameObject Cam1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
           Cam1.SetActive(true);
           MainCamera.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.F2))
        {
            Cam1.SetActive(false);
            MainCamera.SetActive(true);
        }

    }
}
