using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public Animator anime;
    public Rigidbody2D playerRigidbody;
    public int forceJump;
    public Transform groundCheck;
    public bool groundDed;
    public LayerMask chao;
    public bool pressing = false;
    public Vector3 clickPosition;
    public Transform colisor;
    public bool slide = false;
    public float timeTemp;
    public float slideTemp;
	public bool trocaComando = true;
	public float timeTrocaComando = 0;

    public UnityEngine.UI.Text pontos;
    public static int pontuacao;

    public GameObject PainelGameOver;

    public UnityEngine.UI.Text nPontos;
    public UnityEngine.UI.Text nRecorde;

    public AudioSource sound;
    public AudioSource music;
    public AudioSource gameOverMusic;

    public bool controleTimeInicio;
    public int cont;

    // Use this for initialization
    void Start () {
        pontuacao = 0;
        PainelGameOver.SetActive(false);
        sound.volume = PlayerPrefs.GetFloat("sound");
        music.volume = PlayerPrefs.GetFloat("music");
        gameOverMusic.volume = PlayerPrefs.GetFloat("music");
        music.Play();
        gameOverMusic.Stop();
    }

	// Update is called once per frame
	void Update () {
        
        pontos.text = pontuacao.ToString();

        if (Input.GetMouseButtonDown(0) && groundDed)
        {
            
            clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
			if(clickPosition.x > 0 && trocaComando)
            {
                playerRigidbody.AddForce(new Vector2(0, forceJump));
                if (slide)
                {
                    colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.3f, colisor.position.z);
                    slide = false;
                }
                
                
            }
            else if(!slide)
            {

                colisor.position = new Vector3(colisor.position.x, colisor.position.y - 0.3f, colisor.position.z);
                slide = true;
                timeTemp = 0;

            }
        }
        groundDed = Physics2D.OverlapCircle(groundCheck.position, 0.2f, chao);

        if (slide)
        {
            timeTemp += Time.deltaTime;

            if (timeTemp >= slideTemp)
            {
                colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.3f, colisor.position.z);
                slide = false;
            }
        }
        anime.SetBool("jump", !groundDed);
        anime.SetBool("abaixa", slide);


    }

    void OnTriggerEnter2D()
    {
        music.Stop();
        gameOverMusic.Play();
        PlayerPrefs.SetInt("pontuacao", pontuacao);
        if(pontuacao > PlayerPrefs.GetInt("recorde"))
        {
            PlayerPrefs.SetInt("recorde", pontuacao);
        }
        PainelGameOver.SetActive(true);
        nPontos.text = PlayerPrefs.GetInt("pontuacao").ToString();
        nRecorde.text = PlayerPrefs.GetInt("recorde").ToString();
        Time.timeScale = 0;

    }

    public void voltarMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("inicial");
    }

    public void jogarNovamente()
    {
        PainelGameOver.SetActive(false);
        Time.timeScale = 1;
        Application.LoadLevel("Jogo");
    }

}
