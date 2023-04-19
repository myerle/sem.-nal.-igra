using TMPro;
using UnityEngine;

public class points_shower : MonoBehaviour
{
    public TMP_Text curr_points_txt;
    public TMP_Text all_points_txt;

    public int current_points;
    public int all_points;

    public car_controller car_controller;

    private void Start()
    {
        GameObject carObject = GameObject.Find("Chev666");

        car_controller = carObject.GetComponent<car_controller>();

    }

    void Update()
    {
        current_points = car_controller.current_points;
        all_points = car_controller.all_points;

        if (current_points > 0)
            curr_points_txt.SetText(current_points.ToString());

        else curr_points_txt.SetText("");

        all_points_txt.SetText("POINTS: " + all_points.ToString());
    }
}

