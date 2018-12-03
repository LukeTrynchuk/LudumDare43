using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DogHouse.VFX;
using DogHouse.Core.Services;
using DogHouse.Services;

public class BombLogic : MonoBehaviour
{
    [SerializeField]
    private ExplosiveVFXController controller;
    private ServiceReference<ISceneManager> sceneMan = new ServiceReference<ISceneManager>();

    private void OnEnable()
    {
        controller.OnExplodingFinished -= Explode;
        controller.OnExplodingFinished += Explode;
    }

    private void OnDisable()
    {
        if(controller != null)
        {
            controller.OnExplodingFinished -= Explode;
        }
    }

    private void Explode()
    {
        Destroy(this.gameObject);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(Vector3.Distance(player.transform.position,this.transform.position)<5f)
        {
            if (sceneMan.isRegistered())
            {
                sceneMan.Reference.LoadMainMenuScene();
            }
        }
        GameObject[] chicken = GameObject.FindGameObjectsWithTag("Chicken");
        chicken = chicken.Where(x => Vector3.Distance(x.transform.position, this.transform.position) < 5f).ToArray();
        foreach(GameObject c in chicken)
        {
            Destroy(c);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Chicken")
        {
            controller.BeginExploding();
        }
    }
}
