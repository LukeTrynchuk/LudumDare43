using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogHouse.Services;
using DogHouse.Core.Services;

public class ChickenMovement: MonoBehaviour
{
    private ServiceReference<IInputService> inputService = new ServiceReference<IInputService>();
    private Rigidbody myBody;
    public float moveSpeed;

    // Start is called before the first frame update
    private void OnEnable()
    {
        inputService.AddRegistrationHandle(ServiceRegistered);
        myBody = GetComponent<Rigidbody>();
    }
    private void OnDisable()
    {
        inputService.Reference.OnMovementVectorCalculated -= MoveChicken;
    }
    private void ServiceRegistered()
    {
        inputService.Reference.OnMovementVectorCalculated -= MoveChicken;
        inputService.Reference.OnMovementVectorCalculated += MoveChicken;
    }

    private void MoveChicken(Vector2 move)
    {

        if (move.x > 0.5f || move.x < -0.5f)
        {
            //transform.Translate(new Vector3(move.x * moveSpeed * Time.deltaTime, 0f, 0f));
            myBody.velocity = new Vector2(move.x * moveSpeed, myBody.velocity.y);
        }
        if (move.y > 0.5f || move.y < -0.5f)
        {
            //transform.Translate(new Vector3(0f, move.y  * moveSpeed * Time.deltaTime, 0f));
            myBody.velocity = new Vector2(myBody.velocity.x, move.y * moveSpeed);
        }
        if (move.x < 0.5f && move.x > -0.5f)
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }
        if (move.y < 0.5f && move.y > -0.5f)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0f);
        }
    }
}
