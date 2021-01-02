using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {
    public static string direction;
    public static bool go;
    public float speed;
    public float rotatespeed;

	public GameObject enemy;
	public float enemyrate;

    public GameObject explosion;
    public int health; 
	public static int ammo;
	// Use this for initialization
	void Start () {
		ammo = 5;
		Camera.main.ResetAspect ();
        direction = "center";
        go = true;
		StartCoroutine ("SpawnEnemies");
	}
	
	// Update is called once per frame
	void Update () {

        GameObject.Find("HealthText").GetComponent<TextMesh>().text = "Health: " + health;
		GameObject.Find ("Ammo").GetComponent<TextMesh> ().text = "Ammo: " + ammo;
        if (go)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
            if (direction.Equals("left"))
            {
                transform.Rotate(new Vector3(0, -1.0f * rotatespeed * Time.deltaTime, 0));
            }
            if (direction.Equals("right"))
            {
                transform.Rotate(new Vector3(0, rotatespeed * Time.deltaTime, 0));
            }

        if(health <=0)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            if (transform.rotation.x > -30)
            {
                transform.Rotate(new Vector3(3 * Time.deltaTime, 0, 0));
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("EnemyCannon"))
        {
            health--;
            collision.gameObject.SetActive(false);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }

	public IEnumerator SpawnEnemies()
	{
		while (true) {
			yield return new WaitForSeconds (enemyrate);
			Vector3 pos = new Vector3 (Random.Range (-60, 70), transform.position.y, Random.Range (-100, 70));
			while (Vector3.Distance (transform.position, pos) < 60) {
				pos = new Vector3 (Random.Range (-60, 70), transform.position.y, Random.Range (-100, 70));
			}
			Instantiate (enemy,pos, Quaternion.identity);
		}
	}
}
