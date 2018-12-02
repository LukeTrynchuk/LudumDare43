using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSpawn : MonoBehaviour
{
    private Vector3 finalPosition;
    private Vector3 startPosition;
    private bool hasFinalPosition;
    private float timePassed = 0f;
    [SerializeField]
    private float timeThreshold = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    public void GetPosition(Vector3 pos)
    {
        finalPosition = pos;
        hasFinalPosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasFinalPosition)
        {
            timePassed += Time.deltaTime;
            float t = Mathf.Clamp01(timePassed / timeThreshold);
            this.transform.position = Vector3.Lerp(startPosition, finalPosition, t);
            if(Mathf.Approximately(t,1f))
            {
                this.enabled = false;
            }
        }
    }
}
