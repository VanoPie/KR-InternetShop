using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;

namespace InternetShop;

public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=InternetShop;port=3306;User Id=root;password=Qwerty_123456";
    private void Reg(object? sender, RoutedEventArgs e)
    {
        conn = new MySqlConnection(connStr);
        conn.Open();
        string regist = "INSERT INTO пользователи VALUES (" + Convert.ToInt32(id.Text) + ", '" + first_name.Text + "', '" + name.Text + "', '" + login.Text + "', '" + password.Text + "', '" + email.Text + "', '" + adres.Text + "');";
        MySqlCommand cmd = new MySqlCommand(regist, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        MainWindow auth = new MainWindow();
        this.Close();
        auth.Show();
    }
}