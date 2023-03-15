using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	private float speed = 2;
	public float timeToDisplay;
    public Vector2 dir;
    private Vector2 origPos;
	
    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
		speed = 0;
		dir.y = -1;
		
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
		
		timeToDisplay += Time.deltaTime;
		float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		if ((seconds % 15) == 0) {
			speed = 2;
		} 
    }
	private void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.transform.tag.StartsWith("Paddle")){
			transform.position = origPos;
			speed = 0;
		}
		if (c.gameObject.transform.tag.StartsWith("Catch")){
			transform.position = origPos;
			speed = 0;
		}
	}
}
