using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCotroler : MonoBehaviour {

    public GameObject barreiraPreFab;
    public float rateSpawn;
    public float currentTime;
    private int position;
    private float y;
    public float posA;
    public float posB;

	// Use this for initialization
	void Start () {
        currentTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
            
        if(currentTime >= rateSpawn)
        {
            position = Random.Range(1,100);
            if(position > 50)
            {
                y = posA;
            }
            else
            {
                y = posB;
            }
            currentTime = 0;
            GameObject tempPreFab = Instantiate(barreiraPreFab) as GameObject;
            tempPreFab.transform.position = new Vector3(transform.position.x, y, tempPreFab.transform.position.z);   
        }

    }
}
