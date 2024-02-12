using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;

namespace InternetShop.CRUD;

public partial class CRUD_RegUsers : Window
{
    private List<Пользователи> Users;
    private Пользователи CurrentUsers;
    public CRUD_RegUsers(Пользователи currentUser, List<Пользователи> users)
    {
        InitializeComponent();
        CurrentUsers = currentUser;
        this.DataContext = currentUser;
        Users = users;
    }
    private MySqlConnection conn;
    string connStr = "server=localhost;database=InternetShop;port=3306;User Id=root;password=Qwerty_123456";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Users.FirstOrDefault(x => x.ID == CurrentUsers.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO пользователи VALUES (" + Convert.ToInt32(id.Text) + ", '" + first_name.Text + "', '" + name.Text + "', '" + login.Text + "', '" + password.Text + "', '" + email.Text + "', '" + adres.Text + "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE пользователи SET Фамилия = '" + first_name.Text + "', Имя = '" +  name.Text + "', Логин = '" + login.Text + "', Пароль = '" + password.Text + "', Почта = '" + email.Text + "', Адрес_доставки = '" + adres.Text + "' WHERE id = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        RegUsers back = new RegUsers();
        this.Close();
        back.Show(); 
    }
}