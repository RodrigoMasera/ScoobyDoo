using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;

public class Autenticacao {

    private FirebaseAuth auth;
    private FirebaseUser user;

    public Autenticacao() {
    }

    public FirebaseUser getUser() {
        return this.user;
    }

    public void criarUsuario(string email, string password) {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync foi cancelado.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encontrou um erro: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            user = task.Result;
            Debug.LogFormat("Usuario criado com sucesso: {0} ({1})",
                user.DisplayName, user.UserId);
        });

        logar(email, password);
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