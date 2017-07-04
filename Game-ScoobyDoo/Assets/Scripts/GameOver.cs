using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOver : MonoBehaviour {

    public UnityEngine.UI.Text pontos;
    public UnityEngine.UI.Text recorde;

    public GameObject PainelGameOver;

	// Use this for initialization
	void Start () {
        PainelGameOver.SetActive(false);    
    }
	
	// Update is called once per frame
	void Update () {
        pontos.text = PlayerPrefs.GetInt("pontuacao").ToString();
        recorde.text = PlayerPrefs.GetInt("recorde").ToString();

    }
}
