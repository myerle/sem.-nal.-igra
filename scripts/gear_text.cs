using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class gear_text : MonoBehaviour
{
    public TMP_Text accText;
    public int current_Gear;

    public car_controller car_controller;

    private void Start()
    {
        GameObject carObject = GameObject.Find("Chev666");

        car_controller = carObject.GetComponent<car_controller>();

    }

    void Update()
    {
        current_Gear = car_controller.currentGear;

        accText.SetText(current_Gear.ToString());
    }
}
