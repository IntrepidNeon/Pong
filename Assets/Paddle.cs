using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer;

    public float Speed = 1;

    public float Velocity;

    private float RememberY = 0;

    private float MoveInput;

    private float MovementRange;

    private float dt;

    private float diff;

    Vector3 StartPos;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        MovementRange = Camera.main.orthographicSize - transform.localScale.y / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        Velocity = (transform.position.y - RememberY) / dt;
        RememberY = transform.position.y;
        if (isPlayer)
        {
            processInputs();
        }
        else
        {
            think();
        }
        
        move();
    }
    void processInputs()
    {
        MoveInput = Camera.main.ScreenToWorldPoint(new Vector3(0f, Input.mousePosition.y, 0f)).y;

    }
    void think()
    {
        Ball Ballref = FindObjectOfType<Ball>();

        diff = Ballref.transform.position.y - transform.position.y;
            MoveInput = ((transform.position.y < Ballref.transform.position.y) ? 1 : -1);
            GetComponent<Renderer>().material.color = new Color(1, 
                Mathf.Lerp(GetComponent<Renderer>().material.color.g, 1 - Mathf.Abs(Velocity) / Speed, dt),
                Mathf.Lerp(GetComponent<Renderer>().material.color.b, 1 - Mathf.Abs(Velocity) / Speed, dt));
        
    }
    void move()
    {
        float NewPos;

        if (isPlayer)
        {
            NewPos  = MoveInput;
        }
        else
        {
            NewPos = transform.position.y + (MoveInput * Speed * dt) * Mathf.Pow(Mathf.Abs(diff),3);
        }
        NewPos = Mathf.Clamp(NewPos, -MovementRange, MovementRange);

        transform.position = new Vector3(transform.position.x, NewPos, transform.position.z);
    }
}
