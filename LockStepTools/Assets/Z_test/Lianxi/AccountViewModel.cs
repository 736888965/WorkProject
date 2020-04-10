using Loxodon.Framework.Observables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountViewModelTest : ObservableObject
{
    private int id;
    private string username;
    private string password;
    private string email;
    private DateTime birthday;
    private readonly ObservableProperty<string> address = new ObservableProperty<string>();

    public int ID
    {
        get { return this.id; }
        set { this.Set<int>(ref this.id, value, "ID"); }
    }
    public string Username
    {
        get { return this.username; }
        set { this.Set<string>(ref this.username, value, "Username"); }
    }
    public string Password
    {
        get { return this.password; }
        set { this.Set<string>(ref this.password, value, "Password"); }
    }

    public string Email
    {
        get { return this.email; }
        set { this.Set<string>(ref this.email, value, "Email"); }
    }
    public DateTime Birthday
    {
        get { return this.birthday; }
        set { this.Set<DateTime>(ref this.birthday, value, "Birthday"); }
    }
    public ObservableProperty<string> Address
    {
        get { return this.address; }
    }
}
