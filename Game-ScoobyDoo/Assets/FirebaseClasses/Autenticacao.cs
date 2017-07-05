using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;

public class Autenticacao : MonoBehaviour
{

    private FirebaseAuth auth;
    private FirebaseUser user;

    void Start() {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
    }

    public FirebaseUser getUser() {
        return this.user;
    }

    public void criarUsuario(string email, string password) {
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

    public void logar(string email, string password) {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync foi cancelado.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encontrou um erro: " + task.Exception);
                return;
            }

            user = task.Result;
            Debug.LogFormat("Usuário logado com sucesso: {0} ({1})",
                user.DisplayName, user.UserId);
        });
        InitializeFirebase();
    }

    private void InitializeFirebase() {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn =
                (user != auth.CurrentUser) && (auth.CurrentUser != null);

            if (!signedIn && user != null) {
                Debug.Log("Deslogado " + user.UserId);
            }

            user = auth.CurrentUser;
            if (signedIn) {
                Debug.Log("Logado " + user.UserId);
                PlayerPrefs.SetString("idUsuario", user.UserId ?? "");
                PlayerPrefs.SetString("nomeUsuario", user.DisplayName ?? "");
                PlayerPrefs.SetString("emailUsuario", user.Email ?? "");
                PlayerPrefs.SetString("fotoUrlUsuario", user.PhotoUrl.ToString() ?? "");
            }
        }
    }

    void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

}