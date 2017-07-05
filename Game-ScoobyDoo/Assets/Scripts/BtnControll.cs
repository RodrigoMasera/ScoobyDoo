using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class BtnControll : MonoBehaviour {
    
    public GameObject painelButtons;
    public GameObject painelOptions;
    public GameObject painelCadastro;
    public GameObject painelRecorde;
    public Slider soundSlider;
    public AudioSource sound;
    public Slider musicSlider;
    public AudioSource music;
    public InputField emailInput;
    public InputField senhaInput;
    private DatabaseReference reference;

    void Start() { 
        painelOptions.SetActive(false);
        painelButtons.SetActive(true);
        PlayerPrefs.SetFloat("sound", sound.volume);
        PlayerPrefs.SetFloat("music", music.volume);
    }

    public void btnClick(string comando) {
        setOperation(comando);
    }

    void Update() {
        if (soundSlider.value != sound.volume) {
            sound.volume = soundSlider.value;
            sound.Play();
            PlayerPrefs.SetFloat("sound", sound.volume);
        }

        if (musicSlider.value != music.volume) {
            music.volume = musicSlider.value;
            PlayerPrefs.SetFloat("music", music.volume);
        }
    }

    private void setOperation(string comando) {
        switch (comando) {
            case "jogo":
                play();
                break;
            case "pausa":
                pausa();
                break;
            case "sair":
                sair();
                break;
            case "opcoes":
                opcoes();
                break;
            case "cadastro":
                cadastro();
                break;
            case "novoUsuario":
                novoUsuario();
                break;
            case "voltarMenu":
                voltarMenu();
                break;
            case "recorde":
                recorde();
                break;
        }
    }

    private void novoUsuario() {
        Autenticacao autenticacao = new Autenticacao();
        autenticacao.criarUsuario(emailInput.text, senhaInput.text);
        voltarMenu();
    }

    private void voltarMenu() {
        setCadastro();
        setOptions();
        painelOptions.SetActive(false);
        painelCadastro.SetActive(false);
        painelRecorde.SetActive(false);
        painelButtons.SetActive(true);
    }

    private void opcoes() {
        setMeio(painelOptions);
        setCadastro();
        painelCadastro.SetActive(false);
        painelButtons.SetActive(false);
        painelRecorde.SetActive(false);
        painelOptions.SetActive(true);
    }

    private void cadastro() {
        setMeio(painelCadastro);
        setOptions();
        painelButtons.SetActive(false);
        painelOptions.SetActive(false);
        painelRecorde.SetActive(false);
        painelCadastro.SetActive(true);

    }

    private void recorde()
    {
        setMeio(painelRecorde);
        setOptions();
        painelButtons.SetActive(false);
        painelOptions.SetActive(false);
        painelCadastro.SetActive(false);
        painelRecorde.SetActive(true);

    }

    private void play() {
        Application.LoadLevel("Jogo");
    }

    private void pausa() {
        Time.timeScale = 0;
    }

    private void sair() {
        Application.Quit();
    }

    private void setMeio(GameObject gameObject) {
        gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0.160002f, 0f);
        gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(2.824515e-05f, 1f);
    }

    private void setOptions() {
        painelOptions.GetComponent<RectTransform>().offsetMin = new Vector2(1420f, 0f);
        painelOptions.GetComponent<RectTransform>().offsetMax = new Vector2(-1384f, 1f);
    }

    private void setCadastro() {
        painelCadastro.GetComponent<RectTransform>().offsetMin = new Vector2(-1420f, 0f);
        painelCadastro.GetComponent<RectTransform>().offsetMax = new Vector2(1384f, 1f);
    }
}
