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

public partial class CRUD_OrderWin : Window
{
    private List<Заказы> Orders;
    private Заказы CurrenOrder;
    public CRUD_OrderWin(Заказы currentOrder, List<Заказы> orders)
    {
        InitializeComponent();
        CurrenOrder = currentOrder;
        this.DataContext = currentOrder;
        Orders = orders;
    }
    
    private MySqlConnection conn;
    string connStr = "server=localhost;database=InternetShop;port=3306;User Id=root;password=Qwerty_123456";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Orders.FirstOrDefault(x => x.ID == CurrenOrder.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO заказы VALUES (" + Convert.ToInt32(id.Text) + ", '" + id_user.Text + "', '" + date_create.Text + "', '" + stat.Text + "');";
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
                string upd = "UPDATE заказы SET ID_пользователя = " + Convert.ToInt32(id_user.Text) + ", Дата_создания = '" +  date_create.Text + "', Статус = " + Convert.ToInt32(stat.Text) + " WHERE ID = " + Convert.ToInt32(id.Text) + ";";
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
        OrderWin back = new OrderWin();
        back.Icon =
            new WindowIcon(@"C:\Users\VanoP\Desktop\Новая папка\Practica\InternetShop\InternetShop\obj\icon.ico");
        this.Close();
        back.Show(); 
    }
}