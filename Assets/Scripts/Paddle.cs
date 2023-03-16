using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.01f;
	public float timeBtwSpawns;
	public float startTimeBtwSpawns;
	public GameObject echo;

    public bool speedPower = false;

	public GameObject power_left;
    public GameObject power_right;
    public GameObject cannon;
    private Ball ball;
    private Powerup left_power;
    private Powerup right_power;

    public int strength = 50;

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        left_power = power_left.GetComponent<Powerup>();
        right_power = power_right.GetComponent<Powerup>();
        ball = cannon.GetComponent<Ball>();

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
		if (timeBtwSpawns <= 0) {
			Instantiate(echo, transform.position, Quaternion.identity);
			timeBtwSpawns = startTimeBtwSpawns;
		} else {
			timeBtwSpawns -= Time.deltaTime;
		}
        if (transform.CompareTag("PaddleRight")) {
            if (Input.GetKey(KeyCode.UpArrow)) {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow)) {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow)) {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
        else if (transform.CompareTag("PaddleLeft")){
    
            if (Input.GetKey(KeyCode.W)) {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A)) {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D)) {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }
	private void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.transform.tag.StartsWith("Powerup")){
            if (transform.CompareTag("PaddleRight")){
                if(right_power.randPowerup == "Speed"){
                    speedPower = true;
                }
                else if (right_power.randPowerup == "Damage"){
                    strength = 100;
                }
                else if(right_power.randPowerup == "Health"){
                    ball.healthRight += 50;
                    ball.updateHealth();
                }
			}

            else if (transform.CompareTag("PaddleLeft")){
                if(left_power.randPowerup == "Speed"){
                    speedPower = true;
                }
                else if (left_power.randPowerup == "Damage"){
                    strength = 100;
                }
                else if(left_power.randPowerup == "Health"){
                    ball.healthLeft += 50;
                    ball.updateHealth();
                }
            }
        }
	}
}
