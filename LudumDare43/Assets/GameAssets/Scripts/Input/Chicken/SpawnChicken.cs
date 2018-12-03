using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogHouse.Core.Services;
using DogHouse.Services;
using Sirenix.OdinInspector;

public class SpawnChicken : MonoBehaviour
{
    int chickenCounter = 0;
    int maxChicken = 10;
    [SerializeField]
    GameObject chicken;

    private Vector3 finalPosition;

    private ServiceReference<IInputService> inputService = new ServiceReference<IInputService>();

    private void OnEnable()
    {
        inputService.AddRegistrationHandle(RegisterInput);
    }

    private void OnDisable()
    {
        if (inputService.isRegistered())
        {
            inputService.Reference.OnSpawnButtonPressed -= SpawnAChicken;
        }
    }

    private void RegisterInput()
    {
        inputService.Reference.OnSpawnButtonPressed -= SpawnAChicken;
        inputService.Reference.OnSpawnButtonPressed += SpawnAChicken;
    }


    private void SpawnAChicken()
    {
        Debug.Log("Spawn Chicken");
        if(chickenCounter < maxChicken)
        {
            Debug.Log("Can Spawn Chicken");
            finalPosition = this.transform.position + (this.transform.forward * 3);

            GameObject c = Instantiate(chicken,this.transform.position,Quaternion.identity);

            c.GetComponent<LerpSpawn>().GetPosition(finalPosition);

            chickenCounter++;
        }
    }
}
