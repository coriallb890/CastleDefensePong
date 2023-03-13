using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public TextMeshProUGUI txtScoreLeft;
    public TextMeshProUGUI txtScoreRight;

    private int scoreLeft;
    private int scoreRight;

    public float speed = 4;
    public Vector2 dir;
    private Vector2 origPos;

    // Start is called before the first frame update
    void Start()
    {
        scoreLeft = 0;
        scoreRight = 0;
        txtScoreLeft.text = "0";
        txtScoreRight.text = "0";
        origPos = transform.position;
        float result = Random.Range(0f, 1f);
        if (result < 0.5) {
            dir = Vector2.left;
        }
        else {
            dir = Vector2.right;
        }
        result = Random.Range(0f, 1f);
        if (result < 0.5) {
            dir.y = 1;
        }
        else {
            dir.y = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.transform.tag.StartsWith("Paddle")){
        dir.x *= -1;
    }
        else if (c.gameObject.CompareTag("TopBottom Boundary")){
            dir.y *= -1;
        }
        else if (c.gameObject.CompareTag("Left Boundary")){
            print("right scores");
            scoreRight++;
            txtScoreRight.text = scoreRight.ToString();
            transform.position = origPos;
        }
        else if (c.gameObject.CompareTag("Right Boundary")){
            print("left scores");
            scoreLeft++;
            txtScoreLeft.text = scoreLeft.ToString();
            transform.position = origPos;
        }
    }
}
