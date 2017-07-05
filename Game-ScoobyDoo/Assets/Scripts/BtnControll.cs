using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
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
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

<<<<<<< HEAD
    void Start() {


        dependencyStatus = Firebase.FirebaseApp.CheckDependencies();
        if (dependencyStatus != Firebase.DependencyStatus.Available)
        {
            Firebase.FirebaseApp.FixDependenciesAsync().ContinueWith(task => {
                dependencyStatus = Firebase.FirebaseApp.CheckDependencies();
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    InitializeFirebase();
                }
                else
                {
                    // This should never happen if we're only using Firebase Analytics.
                    // It does not rely on any external dependencies.
                    Debug.LogError(
                        "Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        }
        else
        {
            InitializeFirebase();
        }




=======
    void Start() { 
>>>>>>> c8d15937bfacd4e80ba3099a55065d428497c617
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
        criarUsuario(emailInput.text, senhaInput.text);
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

    public void criarUsuario(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
    }
    // Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            if (user == null && auth.CurrentUser != null)
            {
                Debug.Log("Signed in " + auth.CurrentUser.DisplayName);
            }
            else if (user != null && auth.CurrentUser == null)
            {
                Debug.Log("Signed out " + user.DisplayName);
            }
            user = auth.CurrentUser;
            //Debug.Log("Signed in " + auth.CurrentUser.DisplayName);

        }
    }

}
