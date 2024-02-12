using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using InternetShop.Tables;

namespace InternetShop.CRUD;

public partial class CRUD_Goods : Window
{
    private List<Товары> Goods;
    private Товары CurrenGoods;
    public CRUD_Goods(Товары currenGoods, List<Товары> goods)
    {
        InitializeComponent();
        CurrenGoods = currenGoods;
        this.DataContext = currenGoods;
        Goods = goods;
    }
    
    private MySqlConnection conn;
    string connStr = "server=localhost;database=InternetShop;port=3306;User Id=root;password=Qwerty_123456";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Goods.FirstOrDefault(x => x.ID == CurrenGoods.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO товары VALUES (" + Convert.ToInt32(id.Text) + ", '" + name.Text + "', '" + Convert.ToInt32(price.Text) + "', " + Convert.ToInt32(category.Text) + ");";
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
                string upd = "UPDATE товары SET Наименование = '" + name.Text + "', Цена = '" +  Convert.ToInt32(price.Text) + "', Категория = " + Convert.ToInt32(category.Text) + " WHERE id = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        GoodsWin back = new GoodsWin();
        this.Close();
        back.Show(); 
    }

}