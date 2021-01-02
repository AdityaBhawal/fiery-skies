using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int health;
    public int firehealth;
    public GameObject flame;
    public GameObject explosion;
    public float speed;
    public float rotatespeed;

    private bool shooting;
    private bool inproximinity;
    public float proximityvalue;
    private GameObject leftcannon;
    private GameObject rightcannon;
    public GameObject cannonball;
    public float firefrequency;

    private GameObject player; 
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Ship");
        leftcannon = transform.Find("LeftCannon").gameObject;
        rightcannon = transform.Find("RightCannon").gameObject;

        StartCoroutine("Shoot");
    }
	
	// Update is called once per frame
	void Update () {

        Debug.DrawRay(transform.position, transform.forward);

        double distancetoplayer = Vector3.Distance(transform.position, player.transform.position);
        if(distancetoplayer > proximityvalue)
        {
            transform.LookAt(player.transform.position);
        }
        else
        { 
            if(transform.rotation.y > player.transform.rotation.y)
            {
                transform.Rotate(new Vector3(0.0f, -0.5f * rotatespeed * Time.deltaTime, 0.0f));
            }
            else if(transform.rotation.y < player.transform.rotation.y)
            {
                transform.Rotate(new Vector3(0.0f, 0.5f * rotatespeed * Time.deltaTime, 0.0f));
            }
        }


        transform.Translate(0,0,speed * Time.deltaTime);
        if(health <= 0)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            if(transform.rotation.x > -30)
            {
                transform.Rotate(new Vector3(3 * Time.deltaTime, 0, 0));
            }
        }
        

        if(distancetoplayer < proximityvalue && shooting == false)
        {
            inproximinity = true;
        }
        else
        {
            inproximinity = false;
        }

        if(inproximinity)
        {
            StartCoroutine("Shoot");
            inproximinity = false;
        }
	}

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Cannonball"))
        {
            Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
            ScoreText.score++;
            other.gameObject.SetActive(false);
            health--;
            if (health <= firehealth)
            {
				PlayerShip.ammo += 5;
                GameObject fire = Instantiate(flame, other.gameObject.transform.position, Quaternion.identity);
                fire.transform.SetParent(transform);
            }
        }
    }

    private IEnumerator Shoot()
    {
        shooting = true;
        while(Vector3.Distance(transform.position, player.transform.position) < proximityvalue)
        {
            
            Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
            if (relativePoint.x < 0.0)
                Instantiate(cannonball, leftcannon.transform.position, leftcannon.transform.rotation);
                
            else if (relativePoint.x > 0.0)
                Instantiate(cannonball, rightcannon.transform.position, rightcannon.transform.rotation);
            yield return new WaitForSeconds(firefrequency);
        }
        shooting = false;
    }
}
