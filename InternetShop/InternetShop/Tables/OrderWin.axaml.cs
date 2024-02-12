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

namespace InternetShop.Tables;

public partial class OrderWin : Window
{
    public OrderWin()
    {
        InitializeComponent();
        ShowTable(fullTable);
        FillStatus();
    }
    string fullTable = "SELECT заказы.ID, пользователи.Логин, заказы.Дата_создания, статусы.Статус FROM заказы JOIN пользователи ON заказы.ID_пользователя = пользователи.ID JOIN статусы ON заказы.Статус = статусы.ID";

    private List<Заказы> orders;
    private List<Статусы> stats;
    string connStr = "server=localhost;database=InternetShop;port=3306;User Id=root;password=Qwerty_123456";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        orders = new List<Заказы>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new Заказы()
            {
                ID = reader.GetInt32("ID"),
                Логин = reader.GetString("Логин"),
                Дата_создания = reader.GetString("Дата_создания"),
                Статус = reader.GetString("Статус")
            };
            orders.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = orders;
    }
    
    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = orders;
        gds = gds.Where(x => x.Логин.Contains(Search_Goods.Text)).ToList();
        DataGrid.ItemsSource = gds;
    }
    
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Choise back = new Choise();
        Close();
        back.Show(); 
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable(fullTable);
        Search_Goods.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            Заказы usr = DataGrid.SelectedItem as Заказы;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM заказы WHERE ID = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            orders.Remove(usr);
            ShowTable(fullTable);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    private void AddData(object? sender, RoutedEventArgs e)
    {
        Заказы newOrder = new Заказы();
        CRUD.CRUD_OrderWin add = new CRUD.CRUD_OrderWin(newOrder, orders);
        add.Icon =
            new WindowIcon(@"C:\Users\VanoP\Desktop\Новая папка\Practica\InternetShop\InternetShop\obj\icon.ico");
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        Заказы currenOrder = DataGrid.SelectedItem as Заказы;
        if (currenOrder == null)
            return;
        CRUD.CRUD_OrderWin edit = new  CRUD.CRUD_OrderWin(currenOrder, orders);
        edit.Icon =
            new WindowIcon(@"C:\Users\VanoP\Desktop\Новая папка\Practica\InternetShop\InternetShop\obj\icon.ico");
        edit.Show();
        this.Close();
    }

    private void CmbStatus(object? sender, SelectionChangedEventArgs e)
    {
        var genderComboBox = (ComboBox)sender;
        var currentGender = genderComboBox.SelectedItem as Статусы;
        var filteredUsers = orders
            .Where(x => x.Статус == currentGender.Статус)
            .ToList();
        DataGrid.ItemsSource = filteredUsers;
    }
    
    public void FillStatus()
    {
        stats = new List<Статусы>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from статусы", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGender = new Статусы()
            {
                ID = reader.GetInt32("ID"),
                Статус = reader.GetString("Статус"),

            };
            stats.Add(currentGender);
        }
        conn.Close();
        var genderComboBox = this.Find<ComboBox>("CmbGender");
        genderComboBox.ItemsSource = stats;
    }
}