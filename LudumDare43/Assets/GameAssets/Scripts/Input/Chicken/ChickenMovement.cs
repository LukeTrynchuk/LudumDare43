using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogHouse.Services;
using DogHouse.Core.Services;

public class ChickenMovement : MonoBehaviour
{
    private ServiceReference<IInputService> inputService = new ServiceReference<IInputService>();
    private Rigidbody myBody;
    public float moveSpeed;
    // Start is called before the first frame update
    private void OnEnable()
    {
        inputService.AddRegistrationHandle(RegisterIntput);
        myBody = GetComponent<Rigidbody>();
    }
    private void OnDisable()
    {
        if(inputService.isRegistered())
        {
            inputService.Reference.OnMovementVectorCalculated -= MoveChicken;
        }
        
    }
    private void RegisterIntput()
    {
        inputService.Reference.OnMovementVectorCalculated -= MoveChicken;
        inputService.Reference.OnMovementVectorCalculated += MoveChicken;
    }
    private void MoveChicken(Vector2 move)
    {
        if (move.x > 0.5f || move.x < -0.5f)
        {
            //transform.Translate(new Vector3(move.x * moveSpeed * Time.deltaTime, 0f, 0f));
            myBody.velocity = new Vector3(move.x * moveSpeed, myBody.velocity.y, myBody.velocity.z);
        }
        if (move.y > 0.5f || move.y < -0.5f)
        {
            //transform.Translate(new Vector3(0f, 0f, move.x * moveSpeed * Time.deltaTime));
            myBody.velocity = new Vector3(myBody.velocity.x, myBody.velocity.y, move.y * moveSpeed);
        }
        if (move.x < 0.5f && move.x > -0.5f)
        {
            myBody.velocity = new Vector3(0f, 0f, myBody.velocity.z);
        }
        if (move.y < 0.5f && move.y > -0.5f)
        {
            myBody.velocity = new Vector3(myBody.velocity.x, 0f, 0f);
        }
    }

}
