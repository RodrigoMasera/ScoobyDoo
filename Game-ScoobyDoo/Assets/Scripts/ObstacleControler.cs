using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControler : MonoBehaviour {

    public float speed;
    private float x;
    public GameObject player;
    private bool pontua;
    private int dificuldade;

	// Use this for initialization
	void Start () {
        dificuldade = 5;
        player = GameObject.Find("Player") as GameObject;	
	}
	
	// Update is called once per frame
	void Update () {

        x = transform.position.x;
        x -= speed * Time.deltaTime;

        transform.position = new Vector3(x, transform.position.y, transform.position.z);

		if(x <= -5)
        {
            Destroy(transform.gameObject);
        }

        if(x < player.transform.position.x && !pontua)
        {
            pontua = true;
            PlayerController.pontuacao++;
        }


        if (PlayerController.pontuacao > dificuldade && dificuldade < 20)
        {
            dificuldade += 5;
            speed += 0.05f;
        }
    }
}
