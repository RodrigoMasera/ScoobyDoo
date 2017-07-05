using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class BancoDoFirebase {
    private DatabaseReference reference;

    public BancoDoFirebase() {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://scooby-doo-c9316.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void writeNewUser(string userId) {
        Debug.Log("entrei");
        Usuario usuario = new Usuario("teste@teste.com", "teste");
        string json = JsonUtility.ToJson(usuario);
        reference.Child("usuarios").Child(userId).SetRawJsonValueAsync(json);
    }

}