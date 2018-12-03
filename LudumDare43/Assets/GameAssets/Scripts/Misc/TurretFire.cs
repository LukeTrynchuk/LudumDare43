using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField]
    private float reloadTime;
    [SerializeField]
    private float bulletSpeed;
    private float counter;
    public GameObject barrel;
    private bool fired;
    // Start is called before the first frame update
    void Start()
    {
        fired = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!fired)
        {
            
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
            {
                if (hit.transform.tag == "Chicken" || hit.transform.tag == "Player")
                {
                    Debug.Log("Hit Chicken");
                    GameObject p = Instantiate(projectile, barrel.transform.position, Quaternion.identity);
                    p.GetComponent<Rigidbody>().AddForce(p.transform.forward * bulletSpeed, ForceMode.Impulse);
                    fired = true;
                }
            }
        }
        else
        {
            counter += Time.deltaTime;
            if(counter >= reloadTime)
            {
                counter = 0;
                fired = false;
            }
        }

    }
}
