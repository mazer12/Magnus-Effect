using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inputs : MonoBehaviour
{
    [HideInInspector] public string F1 , F2, F3;
    [HideInInspector] public string w1 , w2, w3;
    [HideInInspector] public string Cm;
    [HideInInspector] public string Cd;
    public TMP_InputField inputF1;
    public TMP_InputField inputF2;
    public TMP_InputField inputF3;
    public TMP_InputField inputw1;
    public TMP_InputField inputw2;
    public TMP_InputField inputw3;
    public TMP_InputField inputCd;
    public TMP_InputField inputCm;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetButton();
        Debug.Log(Cm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadInputF1(string a)
    {
        F1 = a;
        Debug.Log(F1);
    }
    public void ReadInputF2(string a)
    {
        F2 = a;
        Debug.Log(F2);
    }
    public void ReadInputF3(string a)
    {
        F3 = a;
        Debug.Log(F3);
    }


    public void ReadInputW1(string a)
    {
        w1 = a;
        Debug.Log(w1);
    }
    public void ReadInputW2(string a)
    {
        w2 = a;
        Debug.Log(w2);
    }
    public void ReadInputW3(string a)
    {
        w3 = a;
        Debug.Log(w3);
    }


    public void ReadInputCm(string a)
    {
        Cm = a;
        Debug.Log(Cm);
    }
    public void ReadInputCd(string b)
    {
        Cd = b;
        Debug.Log(Cd);
    }

    public void ResetButton()
    {
        inputF1.text = 30.0.ToString();
        inputF2.text = 10.5.ToString();
        inputF3.text = 1.0.ToString();
        inputw1.text = 0.0.ToString();
        inputw2.text = 50.0.ToString();
        inputw3.text = (-50.0).ToString();
        inputCd.text = 0.35.ToString();
        inputCm.text = 0.175.ToString();
        //Cm = 0.175.ToString();
        Debug.Log("reset");
    }

}
