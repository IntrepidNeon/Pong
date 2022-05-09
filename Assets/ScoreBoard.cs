using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Ball BallRef;
    public Text ScoreText;
    public int P1Score;
    public int P2Score;
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        if (BallRef.transform.position.x > Camera.main.orthographicSize * 2)
        {
            P1Score += 1;
            UpdateScores();
        }
        if (BallRef.transform.position.x < -Camera.main.orthographicSize * 2)
        {
            P2Score += 1;
            UpdateScores();
        }
    }

    // Update is called once per frame
    void UpdateScores()
    {
        BallRef.Sender = null;
        BallRef.transform.position = BallRef.StartPos;
        BallRef.Speed = BallRef.InitialSpeed;
        ScoreText.text = P1Score + " : " + P2Score;
    }
}
