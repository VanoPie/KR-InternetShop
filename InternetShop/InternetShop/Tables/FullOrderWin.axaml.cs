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

public partial class FullOrderWin : Window
{
    string fullTable = "SELECT состав_заказа.ID_заказа, товары.Наименование, состав_заказа.Количество_товара FROM состав_заказа JOIN заказы ON состав_заказа.ID_заказа = заказы.ID JOIN товары ON состав_заказа.ID_товара = товары.ID";
    public FullOrderWin()
    {
        InitializeComponent();
        ShowTable(fullTable);
    }
    
    private List<Состав_заказа> staff;
    string connStr = "server=localhost;database=InternetShop;port=3306;User Id=root;password=Qwerty_123456";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        staff = new List<Состав_заказа>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new Состав_заказа()
            {
                ID_заказа = reader.GetInt32("ID_заказа"),
                Наименование = reader.GetString("Наименование"),
                Количество_товара = reader.GetInt32("Количество_товара"),
            };
            staff.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = staff;
    }
    
    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = staff;
        gds = gds.Where(x => x.Наименование.Contains(Search_Goods.Text)).ToList();
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
            Состав_заказа usr = DataGrid.SelectedItem as Состав_заказа;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM состав_заказа WHERE ID_заказа = " + usr.ID_заказа;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            staff.Remove(usr);
            ShowTable(fullTable);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    private void AddData(object? sender, RoutedEventArgs e)
    {
        /*Товары newUser = new Товары();
        CRUD.CRUD_Goods add = new CRUD.CRUD_Goods(newUser, staff);
        add.Show();
        this.Close();*/
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        /*Товары currenGoods = DataGrid.SelectedItem as Товары;
        if (currenGoods == null)
            return;
        CRUD.CRUD_Goods edit = new CRUD.CRUD_Goods(currenGoods, staff);
        edit.Show();
        this.Close();*/
    }
}