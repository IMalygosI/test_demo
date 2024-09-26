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

            Tovars.Text = Convert.ToString(Helper.DataBase.SpisokTovars.Count());// Количество типов товара
            
            sill();// Загрузка и обновление данных
        }
        // Добавление товара в корзину
        private void DobavitInBasket_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var selet = List_Box.SelectedItem as SpisokTovar;//обнаруживаем нужный товар
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

        // Обновление данных
        public void sill()
        {            
            // Проверка корзины
            if (Helper.Korzina.Count() > 0) 
            {
                basket.IsVisible = true; // Показываем кнопку
            }

            List_Box.ItemsSource = Helper.DataBase.SpisokTovars.OrderBy(z => z.Id);
        }
        // Корзина
        private void Basket_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Basket window = new Basket();
            window.Show();
            Close();
        }
    }
}