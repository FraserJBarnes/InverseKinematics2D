using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [Range(1, 10)]
    public double ABLength = 5;
    [Range(1, 10)]
    public double BCLength = 5;
    GameObject jointA = null;
    GameObject jointB = null;
    GameObject armAB = null;
    GameObject armBC = null;
    GameObject pointC = null;

    int ScreenX = Screen.width;
    int ScreenY = Screen.height;

    [Range(-10, 10)]
    public double x = 6;
    [Range(-10, 10)]
    public double y = 6;
    double ACLength;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        jointA = GameObject.Find("Joint A");
        jointB = GameObject.Find("Joint B");
        pointC = GameObject.Find("Point C");
        armAB = GameObject.Find("Arm AB");
        armBC = GameObject.Find("Arm BC");
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        x = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)).x;
        y = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)).y;
        ACLength = -Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        while(ACLength < -(ABLength + BCLength))
        {
            x = x / 1.00001;
            y = y / 1.00001;
            ACLength = -Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        armAB.transform.localScale = new Vector3((float)ABLength, 0.4f, 1);
        armBC.transform.localScale = new Vector3((float)BCLength, 0.4f, 1);
        armAB.transform.localPosition = new Vector3((float)ABLength / 2, 0, 0);
        armBC.transform.localPosition = new Vector3((float)BCLength / 2, 0, 0);
        jointB.transform.localPosition = new Vector3((float)ABLength, 0, 0);
        pointC.transform.localPosition = new Vector3((float)BCLength, 0, 0);

        double angleA2 = Math.Atan(y / x) * (180/Math.PI);
        double angleA = (Math.Acos((-Math.Pow(BCLength, 2) + Math.Pow(ACLength, 2) + Math.Pow(ABLength, 2)) / (2 * ACLength * ABLength)) * (180/Math.PI));
        double angleB = Math.Acos((-Math.Pow(ACLength, 2) + Math.Pow(BCLength, 2) + Math.Pow(ABLength, 2)) / (2 * BCLength * ABLength)) * (180 / Math.PI);
        if (x > 0)
        {
            jointA.transform.localEulerAngles = new Vector3(0, 0, (float)angleA + (float)angleA2 + 180);
        }
        else
        {
            jointA.transform.localEulerAngles = new Vector3(0, 0, (float)angleA + (float)angleA2);
        }
        jointB.transform.localEulerAngles = new Vector3(0, 0, 180 - (float)angleB);
    }
}
