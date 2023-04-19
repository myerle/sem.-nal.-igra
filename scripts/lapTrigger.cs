using UnityEngine;
using TMPro;

public class lapTrigger : MonoBehaviour
{
    public int best_lap;
    public TMP_Text best_lap_points_txt;

    public car_controller car_controller;
    public int all_points;

    private void Start()
    {
        GameObject carObject = GameObject.Find("Chev666");

        car_controller = carObject.GetComponent<car_controller>();

    }

    void Update()
    {
        all_points = car_controller.all_points;
    }


    public void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Chev666"))
        {
            int current_lap = all_points;

            if (current_lap > best_lap)
            {
                best_lap = current_lap;

                best_lap_points_txt.SetText("BEST LAP: " + best_lap.ToString());
            }

            car_controller.all_points = 0;
        }
    }
}
