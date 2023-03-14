using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.01f;

    public int strength = 50;

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        if (transform.CompareTag("PaddleRight")) {
            
        }
        else {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.CompareTag("PaddleRight")) {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, Mathf.Abs(Mathf.Sin(Time.time)));
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
            GetComponent<SpriteRenderer>().color = new Color(Mathf.Abs(Mathf.Sin(Time.time)), 0, Mathf.Abs(Mathf.Sin(Time.time)));
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
}
