using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private IDictionary<string, int> boxes = new Dictionary<string, int>() {
        {"box_light",10 },
        {"box_medium",50 },
        {"box_heavy",100 }
    };

    // swipee pois-possibility. Tarjolla x-määrä, lähtee sitten vajaalastilla :(

    // All possible boxes numeric values:
    private int boxes_left;
    private int boxes_failed;
    private int boxes_loaded;

    // How many boxes at the start
    private int boxes_init;

    // Boat capacity
    //private int boat_amount_capacity = 0; Not yet implemented 
    private int boat_weight_capacity = 0;

    // Start is called before the first frame update
    void Start()
    {
        boat_weight_capacity = 10;
        boat_weight_capacity = 100;
        boxes_init = 15;
        boxes_left = boxes_init;

        boxes_failed = 0;
        boxes_loaded = 0;
    }

    // Update is called once per frame
    void Update()
    {
        boxes_left -= boxes_failed;
        boxes_left -= boxes_loaded;

        // Overload:
        if (boxes_loaded == boat_weight_capacity) {

            Console.WriteLine("Boat full!");
        }

        if (boxes_loaded > boat_weight_capacity)
        {
            Console.WriteLine("Boat overloaded!");
        }
         if (boxes_left == 0)
        {
            Console.WriteLine("No more boxes");
        }
    }
}
