using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public TextMeshProUGUI txtHealthLeft;
    public TextMeshProUGUI txtHealthRight;

    private int healthLeft;
    private int healthRight;
    public float speed = 4;
    public Vector2 dir;
    private Vector2 origPos;

    public GameObject paddle_left;
    public GameObject paddle_right;
    public GameObject gameOver;
    public GameObject castle_left;
    public GameObject castle_right;
    public GameObject middle_boundary;
    private Paddle left_script;
    private Paddle right_script;
    private Renderer left_castle;
    private Renderer right_castle;

    // Start is called before the first frame update
    void Start()
    {
        healthLeft = 250;
        healthRight = 250;
        txtHealthLeft.text = "250";
        txtHealthRight.text = "250";
        origPos = transform.position;

        left_script = paddle_left.GetComponent<Paddle>();
        right_script = paddle_right.GetComponent<Paddle>();
        left_castle = castle_left.GetComponent<Renderer>();
        right_castle = castle_right.GetComponent<Renderer>();
        Physics2D.IgnoreCollision(middle_boundary.GetComponent<Collider2D>(), GetComponent<Collider2D>());

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
        else if (c.gameObject.CompareTag("Right Boundary")){
            healthRight = healthRight - left_script.strength;
            if(healthRight <= 150 && healthRight > 50){
                right_castle.material.color = new Color(255, 244, 0, 1);
            }
            else if (healthRight <= 50 && healthRight > 0){
                right_castle.material.color = new Color(255, 0, 0, 1);
            }
            if (healthRight <= 0){
                txtHealthRight.text = "0";
                gameOver.GetComponent<UIManager>().GameOverSequence();
                speed = 0;
            }
            else{
                txtHealthRight.text = healthRight.ToString();
            }
            transform.position = origPos;
        }
        else if (c.gameObject.CompareTag("Left Boundary")){
            healthLeft = healthLeft - right_script.strength;
            if(healthLeft <= 150 && healthLeft > 50){
                left_castle.material.color = new Color(255, 244, 0, 1);
            }
            else if (healthLeft <= 50 && healthLeft > 0){
                left_castle.material.color = new Color(255, 0, 0, 1);
            }
            if (healthLeft <= 0){
                txtHealthLeft.text = "0";
                gameOver.GetComponent<UIManager>().GameOverSequence();
                speed = 0;
            }
            else{
                txtHealthLeft.text = healthLeft.ToString();
            }
            transform.position = origPos;
        }
    }
}
