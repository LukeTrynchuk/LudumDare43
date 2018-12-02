using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFirstChicken : MonoBehaviour
{
    private bool spawn = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject c = GameObject.FindGameObjectWithTag("Chicken");
        c.transform.position = this.transform.position;
    }
    private void Update()
    {
        if(!spawn)
        {
            GameObject c = GameObject.FindGameObjectWithTag("Chicken");
            c.transform.position = this.transform.position;
            spawn = true;
        }

    }

}
