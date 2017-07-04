using System.Collections;
using System.Collections.Generic;

public class Usuario {
    
    public string email;
    public string senha;

    public Usuario() {
    }

    public Usuario(string email, string senha) {
        this.email = email;
        this.senha = senha;
    }

    public void setEmail(string email) {
        this.email = email;
    }

    public void setSenha(string senha) {
        this.senha = senha;
    }

    public string getEmail() {
        return this.email;
    }

    public string getSenha() {
        return this.senha;
    }

}
