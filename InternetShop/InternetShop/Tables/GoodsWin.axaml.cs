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

public partial class GoodsWin : Window
{
    public GoodsWin()
    {
        InitializeComponent();
        string fullTable = "SELECT товары.ID, товары.Наименование, товары.Цена, категории.Название FROM товары JOIN категории ON товары.Категория = категории.ID";
        ShowTable(fullTable);
    }
    
    private List<Товары> goods;
    string connStr = "server=localhost;database=InternetShop;port=3306;User Id=root;password=Qwerty_123456";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        goods = new List<Товары>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new Товары()
            {
                ID = reader.GetInt32("ID"),
                Наименование = reader.GetString("Наименование"),
                Цена = reader.GetInt32("Цена"),
                Название = reader.GetString("Название")
            };
            goods.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = goods;
    }
    
    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = goods;
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
        string fullTable = "SELECT товары.ID, товары.Наименование, товары.Цена, категории.Название FROM товары JOIN категории ON товары.Категория = категории.ID";
        ShowTable(fullTable);
        Search_Goods.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            Товары usr = DataGrid.SelectedItem as Товары;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM товары WHERE id = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            goods.Remove(usr);
            ShowTable("SELECT товары.ID, товары.Наименование, товары.Цена, категории.Название FROM товары JOIN категории ON товары.Категория = категории.ID");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    private void AddData(object? sender, RoutedEventArgs e)
    {
        Товары newUser = new Товары();
        CRUD.CRUD_Goods add = new CRUD.CRUD_Goods(newUser, goods);
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        Товары currenGoods = DataGrid.SelectedItem as Товары;
        if (currenGoods == null)
            return;
        CRUD.CRUD_Goods edit = new  CRUD.CRUD_Goods(currenGoods, goods);
        edit.Show();
        this.Close();
    }
}