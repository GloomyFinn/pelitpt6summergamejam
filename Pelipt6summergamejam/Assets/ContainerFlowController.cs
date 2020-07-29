using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerFlowController : MonoBehaviour
{
    public int maxContainers = 10;

    public static string[] containersToLoad;

    public static int containersLeft;

    public static int containersFailed;

    public static int containersLoaded;
    

    // Start is called before the first frame update
    void Start()
    {
        containersLeft = maxContainers;

        containersToLoad = new string[maxContainers];

        int i = 0;

        while (i < containersToLoad.Length)
        {
            containersToLoad[i] = GetContainerWeight("i");
            i++;
        }
    }

    public string GetContainerWeight(string weight)
    {
        int weightID = Random.Range(1, 3);

        if (weightID == 1)
        {
            weight = "box_light";
        }
        else if (weightID == 2)
        {
            weight = "box_medium";
        }
        else
        {
            weight = "box_heavy";
        }

        return weight;
    }

    public static string LoadContainerToCrane(string container)
    {
        return containersToLoad[0];
    }
}
