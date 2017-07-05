using System.Collections;
using System.Collections.Generic;

public class Usuario {
    
    public long id;
    public string email;
    public string senha;

    public Usuario() {
    }

    public Usuario(long id, string email, string senha) {
        this.id = id;
        this.email = email;
        this.senha = senha;
    }

    public Usuario(string email, string senha) {
        this.email = email;
        this.senha = senha;
    }

    public void setId(long id) {
        this.id = id;
    }

    public long getId() {
        return this.id;
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
