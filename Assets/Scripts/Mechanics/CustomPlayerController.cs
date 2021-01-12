using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerController : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 newVelocity = new Vector2(0, 0);
    KinematicObject kinematicObject;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        kinematicObject = GetComponents<KinematicObject>()[1];
    }

    void Update()
    {
        newVelocity.x = 0;

        if (Input.GetKey(KeyCode.D))
            newVelocity.x = 5;
        if (Input.GetKey(KeyCode.A))
            newVelocity.x = -5;
        if (Input.GetKeyDown(KeyCode.Space))
            newVelocity.y = 10;

        if(kinematicObject.velocity.y > 0)
            newVelocity.y *= .8f;

        kinematicObject.velocity = newVelocity;
    }
}
