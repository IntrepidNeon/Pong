using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float InitialSpeed;
    public float Speed = 0f;
    public float Angle = 0;

    public Rigidbody2D BallBody;

    public GameObject Sender;

    public Vector3 StartPos;
    // Start is called before the first frame update
    void Start()
    {
        Speed = InitialSpeed;
        StartPos = transform.position;
        Angle = Random.Range(165, 195);
    }

    // Update is called once per frame
    void Update()
    {
        Angle = Angle > 0 ? Angle : 360 + Angle;
        if (Mathf.Abs(Mathf.Sin(Angle * Mathf.Deg2Rad)) > 0.8)
        {
            if (Mathf.Sin(Angle * Mathf.Deg2Rad) > 0)
            {
                Angle += Mathf.Cos(Angle * Mathf.Deg2Rad) > 0 ? -15 : 15;
            }
            else
            {
                Angle -= Mathf.Cos(Angle * Mathf.Deg2Rad) > 0 ? -15 : 15;
            }
        }
        if (transform.position.y >= Camera.main.orthographicSize - transform.localScale.y / 2 && Mathf.Sin(Angle*Mathf.Deg2Rad) > 0)
        {
            Angle = Angle * -1;
        }
        if (transform.position.y <= -Camera.main.orthographicSize + transform.localScale.y / 2 && Mathf.Sin(Angle * Mathf.Deg2Rad) < 0)
        {
            Angle = Angle * -1;
        }
        BallBody.velocity = new Vector3(Mathf.Cos(Angle * Mathf.Deg2Rad), Mathf.Sin(Angle * Mathf.Deg2Rad), 0) * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Paddle>() != null && collision.gameObject != Sender)
        {
            Sender = collision.gameObject;
            Speed += 0.2f;
            Paddle PaddleRef = collision.GetComponent<Paddle>();
            Angle = (180 - Angle + +Mathf.Atan(transform.position.y-PaddleRef.transform.position.y) * Mathf.Rad2Deg) % 360;
        }
    }
}
