using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;
using InternetShop.CRUD;
using Mysqlx.Prepare;

namespace InternetShop;

public partial class RegUsers : Window
{
    public RegUsers()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM пользователи";
        ShowTable(fullTable);
    }
    private List<Пользователи> users;
    string connStr = "server=localhost;database=InternetShop;port=3306;User Id=root;password=Qwerty_123456";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        users = new List<Пользователи>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new Пользователи()
            {
                ID = reader.GetInt32("ID"),
                Фамилия  = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Логин = reader.GetString("Логин"),
                Пароль = reader.GetString("Пароль"),
                Почта = reader.GetString("Почта"),
                Адрес_доставки  = reader.GetString("Адрес_доставки")
            };
            users.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = users;
    }
    
    private void SearchLogin(object? sender, TextChangedEventArgs e)
    {
        var login = users;
        login = login.Where(x => x.Фамилия.Contains(Search_Login.Text)).ToList();
        DataGrid.ItemsSource = login;
    }
    
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Choise back = new Choise();
        Close();
        back.Show(); 
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        string fullTable = "SELECT * FROM пользователи";
        ShowTable(fullTable);
        Search_Login.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            Пользователи usr = DataGrid.SelectedItem as Пользователи;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM пользователи WHERE id = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            users.Remove(usr);
            ShowTable("SELECT * FROM пользователи");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    private void AddData(object? sender, RoutedEventArgs e)
    {
        Пользователи newUser = new Пользователи();
        CRUD_RegUsers add = new CRUD_RegUsers(newUser, users);
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        Пользователи currentUser = DataGrid.SelectedItem as Пользователи;
        if (currentUser == null)
            return;
        CRUD_RegUsers edit = new  CRUD_RegUsers(currentUser, users);
        edit.Show();
        this.Close();
    }
}