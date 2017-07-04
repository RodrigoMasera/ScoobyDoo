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
    public Slider soundSlider;
    public AudioSource sound;
    public Slider musicSlider;
    public AudioSource music;
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

    public void setOperation(string comando) {
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
            case "voltarMenu":
                voltarMenu();
                break;
        }
    }

    public void voltarMenu() {
        painelButtons.SetActive(true);
        painelOptions.SetActive(false);
    }

    public void opcoes() {
        painelButtons.SetActive(false);
        painelOptions.SetActive(true);
    }

    public void play() {
        Application.LoadLevel("Jogo");
    }

    public void pausa() {
        Time.timeScale = 0;
    }

    public void sair() {
        Application.Quit();
    }
}
