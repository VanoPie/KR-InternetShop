using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using InternetShop.Tables;

namespace InternetShop;

public partial class Choise : Window
{
    public Choise()
    {
        InitializeComponent();
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void Users(object? sender, RoutedEventArgs e)
    {
        RegUsers usr = new RegUsers();
        this.Close();
        usr.Show();
    }

    private void Orders(object? sender, RoutedEventArgs e)
    {
        OrderWin ord = new OrderWin();
        this.Close();
        ord.Icon = new WindowIcon(@"C:\Users\VanoP\Desktop\Новая папка\Practica\InternetShop\InternetShop\obj\icon.ico");
        ord.Show();
    }

    private void FullOrders(object? sender, RoutedEventArgs e)
    {
        FullOrderWin full = new FullOrderWin();
        this.Close();
        full.Show();
    }

    private void Goods(object? sender, RoutedEventArgs e)
    {
        GoodsWin gds = new GoodsWin();
        this.Close();
        gds.Show();
    }
}