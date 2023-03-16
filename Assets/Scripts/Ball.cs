using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public TextMeshProUGUI txtHealthLeft;
    public TextMeshProUGUI txtHealthRight;
	public TextMeshProUGUI timeText;

    public GameObject explosion;

    public int healthLeft;
    public int healthRight;
    public float speed = 4;
	public float timeToDisplay;
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

    public Sprite sanic;
    public Sprite cannon;
    public Sprite leftWall;
    public Sprite leftDoor;
    public Sprite leftWallCrack;
    public Sprite leftDoorCrack;
    public Sprite leftRubble;
    public Sprite rightWall;
    public Sprite rightDoor;
    public Sprite rightWallCrack;
    public Sprite rightDoorCrack;
    public Sprite rightRubble;
	
	public AudioSource audioPlayer;
	public AudioSource swordHit;
	public AudioSource sidesHit;
	

    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    public GameObject wall5;
    public GameObject wall6;
    private SpriteRenderer Lwall1;
    private SpriteRenderer Lwall2;
    private SpriteRenderer Lwall3;
    private SpriteRenderer Rwall1;
    private SpriteRenderer Rwall2;
    private SpriteRenderer Rwall3;
	

    // Start is called before the first frame update
    void Start()
    {
		//strengthy = some.GetComponent<Paddle>();
        healthLeft = 250;
        healthRight = 250;
        txtHealthLeft.text = "250";
        txtHealthRight.text = "250";
		timeText.text = "0";
        origPos = transform.position;
		

        left_script = paddle_left.GetComponent<Paddle>();
        right_script = paddle_right.GetComponent<Paddle>();
        left_castle = castle_left.GetComponent<Renderer>();
        right_castle = castle_right.GetComponent<Renderer>();
        Physics2D.IgnoreCollision(middle_boundary.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		
        Lwall1 = wall1.GetComponent<SpriteRenderer>();
        Lwall2 = wall2.GetComponent<SpriteRenderer>();
        Lwall3 = wall3.GetComponent<SpriteRenderer>();
        Rwall1 = wall4.GetComponent<SpriteRenderer>();
        Rwall2 = wall5.GetComponent<SpriteRenderer>();
        Rwall3 = wall6.GetComponent<SpriteRenderer>();


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
		
		timeToDisplay += Time.deltaTime;
		float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		
		//if (seconds >= 10.0 && seconds < 20){
		//	speed = 6;
		//}
		//else if (seconds >= 20.0) {
		//	speed = 9;
		//}
		//else {
		//	speed = 4;
		//}
    }

	
	
    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.CompareTag("PaddleRight")){
			swordHit.Play();
			dir.x *= -1;

            if (speed == 8){
                GetComponent<SpriteRenderer>().sprite = cannon;
                speed = 4;
            }
			if (right_script.speedPower) {
                GetComponent<SpriteRenderer>().sprite = sanic;
				speed = 8;
                right_script.speedPower = false;
			}
		}
        else if (c.gameObject.CompareTag("PaddleLeft")){
			swordHit.Play();
			dir.x *= -1;
			if (speed == 8){
                GetComponent<SpriteRenderer>().sprite = cannon;
                speed = 4;
            }
            if (left_script.speedPower) {
                GetComponent<SpriteRenderer>().sprite = sanic;
				speed = 8;
                left_script.speedPower = false;
			}
		}
        else if (c.gameObject.CompareTag("TopBottom Boundary")){
			sidesHit.Play();
            dir.y *= -1;
        }
        else if (c.gameObject.CompareTag("Right Boundary")){
			audioPlayer.Play();
            var expo = Instantiate(explosion, c.contacts[0].point, Quaternion.identity);
            Destroy(expo, 1.0f);

            GetComponent<SpriteRenderer>().sprite = cannon;
            healthRight = healthRight - left_script.strength;
            updateHealth();


            if(left_script.strength == 100){
                left_script.strength = 50;
            }
			timeToDisplay = timeToDisplay - timeToDisplay;
            if (speed == 8){
                speed = 4;
            }

            if(healthRight > 150){
                Rwall1.sprite = rightWall;
                Rwall2.sprite = rightDoor;
                Rwall3.sprite = rightWall;
            }
            if(healthRight <= 150 && healthRight > 50){
                Rwall1.sprite = rightWallCrack;
                Rwall2.sprite = rightDoorCrack;
                Rwall3.sprite = rightWallCrack;
            }
            else if (healthRight <= 50 && healthRight > 0){
                Rwall1.sprite = rightRubble;
                Rwall2.sprite = rightRubble;
                Rwall3.sprite = rightRubble;
            }


            if (healthRight <= 0){
                txtHealthRight.text = "0";
                gameOver.GetComponent<UIManager>().GameOverSequence();
                speed = 0;
            }


            transform.position = origPos;
        }
        else if (c.gameObject.CompareTag("Left Boundary")){
			audioPlayer.Play();
            var expo = Instantiate(explosion, c.contacts[0].point, Quaternion.identity);
            Destroy(expo, 1.0f);

            GetComponent<SpriteRenderer>().sprite = cannon;
            healthLeft = healthLeft - right_script.strength;
            updateHealth();


            if(right_script.strength == 100){
                right_script.strength = 50;
            }
			timeToDisplay = timeToDisplay - timeToDisplay;
            if (speed == 8){
                speed = 4;
            }

            if(healthLeft > 150){
                Lwall1.sprite = leftWall;
                Lwall2.sprite = leftDoor;
                Lwall3.sprite = leftWall;
            }
            if(healthLeft <= 150 && healthLeft > 50){
                Lwall1.sprite = leftWallCrack;
                Lwall2.sprite = leftDoorCrack;
                Lwall3.sprite = leftWallCrack;
            }
            else if (healthLeft <= 50 && healthLeft > 0){
                Lwall1.sprite = leftRubble;
                Lwall2.sprite = leftRubble;
                Lwall3.sprite = leftRubble;
            }


            if (healthLeft <= 0){
                txtHealthLeft.text = "0";
                gameOver.GetComponent<UIManager>().GameOverSequence();
                speed = 0;
            }


            transform.position = origPos;
        }
    }

    public void updateHealth(){
        if(healthRight > 150){
            Rwall1.sprite = rightWall;
            Rwall2.sprite = rightDoor;
            Rwall3.sprite = rightWall;
        }
        if(healthRight <= 150 && healthRight > 50){
            Rwall1.sprite = rightWallCrack;
            Rwall2.sprite = rightDoorCrack;
            Rwall3.sprite = rightWallCrack;
        }
        else if (healthRight <= 50 && healthRight > 0){
            Rwall1.sprite = rightRubble;
            Rwall2.sprite = rightRubble;
            Rwall3.sprite = rightRubble;
        }

        if(healthLeft > 150){
            Lwall1.sprite = leftWall;
            Lwall2.sprite = leftDoor;
            Lwall3.sprite = leftWall;
        }
        if(healthLeft <= 150 && healthLeft > 50){
            Lwall1.sprite = leftWallCrack;
            Lwall2.sprite = leftDoorCrack;
            Lwall3.sprite = leftWallCrack;
        }
        else if (healthLeft <= 50 && healthLeft > 0){
            Lwall1.sprite = leftRubble;
            Lwall2.sprite = leftRubble;
            Lwall3.sprite = leftRubble;
        }

        if(healthLeft > 250){
            healthLeft = 250;
        }
        if(healthRight > 250){
            healthRight = 250;
        }
        txtHealthLeft.text = healthLeft.ToString();
        txtHealthRight.text = healthRight.ToString();
    }
}
