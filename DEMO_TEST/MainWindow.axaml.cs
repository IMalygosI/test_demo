using Avalonia.Controls;
using DEMO_TEST.Context;
using DEMO_TEST.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DEMO_TEST
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DobavitInBasket.Click += DobavitInBasket_Click;
            basket.Click += Basket_Click;

            Tovars.Text = Convert.ToString(Helper.DataBase.SpisokTovars.Count());// ���������� ����� ������
            
            sill();// �������� � ���������� ������
        }
        // ���������� ������ � �������
        private void DobavitInBasket_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selet = List_Box.SelectedItem as SpisokTovar;//������������ ������ �����
            var item = Helper.Korzina.FirstOrDefault(x => x.Id == selet.Id);
            if (item != null)
            {
                item.Count++;
            }
            else
            {
                Helper.Korzina.Add(new SpisokTovar
                {
                    Id = selet.Id,
                    Name = selet.Name,
                    Photo = selet.Photo,
                    Opisanie = selet.Opisanie,
                    Manufacturer = selet.Manufacturer,
                    Prise = selet.Prise,
                    Skidka = selet.Skidka,
                    Count = 1,
                });
            }
            sill();
        }

        // ���������� ������
        public void sill()
        {            
            // �������� �������
            if (Helper.Korzina.Count() > 0) 
            {
                basket.IsVisible = true; // ���������� ������
            }

            List_Box.ItemsSource = Helper.DataBase.SpisokTovars.OrderBy(z => z.Id);
        }
        // �������
        private void Basket_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Basket window = new Basket();
            window.Show();
            Close();
        }
    }
}