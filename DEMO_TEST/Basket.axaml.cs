using Avalonia.Controls;
using DEMO_TEST.Models;
using System.Linq;
using System;
using System.Collections.Generic;
namespace DEMO_TEST; 
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System;

public partial class Basket : Window
{
    List<SpisokTovar> Search = Helper.Korzina.ToList();
    public Basket()
    {
        InitializeComponent();

        spisokTovats.Click += SpisokTovats_Click;// ������� � ������ �������
        DeleteInBasket.Click += DeleteInBasket_Click;// �������� ������� �� �������
        oplata.Click += Oplata_Click;


        sill();// ���������� � �������� ������
        Upi();
    }
    // ���������� ���������� ������ � �������
    private void minus_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var button = sender as Button;
        var id = (int)button.Tag;
        var item = Helper.Korzina.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            if (item.Count > 0)
            {
                item.Count--;
                if (item.Count == 0)
                {
                    Helper.Korzina.Remove(item);
                }
                sill();
            }
        }
    }
    public void Upi()
    {
        List<Punktvidasci> Punkt = new List<Punktvidasci>();// ��������
        Punkt = Helper.DataBase.Punktvidascis.ToList();// ������� ���������� �� ����� � ���������������

        Punkt.Add(new Punktvidasci() { Name = "�������� ����� ������" });//��������� � ���� 0 ������� 

        ComBox.ItemsSource = Punkt.OrderByDescending(x => x.Name == "�������� ����� ������").ToList();//��������� ����� ���������� ������� ��� ������
        ComBox.SelectedIndex = 0;// ������ ������� ���������
    }

    private void Oplata_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // ������� ����� ��������� ������ Zakaz
        Zakaz zakaz = new Zakaz();

        zakaz.Id = Helper.DataBase.Zakazs.Count() + 1; // ���������� ���������� ������������� ��� ������
        zakaz.Datez = DateOnly.FromDateTime(DateTime.Now);
        zakaz.Numberzakaz = Helper.GenNumber(); // ���������� ��������� ����� ������
        // �������� ������ ������
        zakaz.Sostavzakaz = string.Join(", ", Helper.Korzina.Select(x => $"{x.Name} (quantity: {x.Count})"));
        zakaz.Summazakaz = (int)Helper.Korzina.Sum(x => x.Prise * x.Count);
        zakaz.Summaskidka = Helper.Korzina.Sum(x => x.Skidka.HasValue ? x.Skidka.Value : 0);
        zakaz.Punktvidachi = ((Punktvidasci)ComBox.SelectedItem).Name;
        zakaz.IdPunktvidachi = ((Punktvidasci)ComBox.SelectedItem).Id;
        zakaz.Kod = Helper.GenCode(); // ���������� ��������� ��� �� 3 ����

        
        if (ComBox.SelectedIndex == 0)
        {
            // ��������� �� ������
            Error Error = new Error();
            Error.ShowDialog(this);
        }
        else
        {
            // ��������� zakaz � ���� ������
            Helper.DataBase.Zakazs.Add(zakaz);
            Helper.DataBase.SaveChanges();
            Helper.Korzina.Clear();

            var fileName = $"Check_{Guid.NewGuid()}.pdf";
            var filePath = Path.Combine("Asset", fileName);

            // ������� PDF-��������
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document pdfDoc = new Document();
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                pdfDoc.Add(new Paragraph("Order " + zakaz.Numberzakaz));
                pdfDoc.Add(new Paragraph("Order Status: New"));
                pdfDoc.Add(new Paragraph("Order Date: " + zakaz.Datez));
                pdfDoc.Add(new Paragraph("Order Composition: " + zakaz.Sostavzakaz));
                pdfDoc.Add(new Paragraph("Order Amount: " + zakaz.Summazakaz + " RUB"));
                pdfDoc.Add(new Paragraph("Discount: " + zakaz.Summaskidka + "%"));
                pdfDoc.Add(new Paragraph("Pickup Point: " + zakaz.Punktvidachi)); 
                Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                pdfDoc.Add(new Paragraph("Order Code: " + zakaz.Kod, boldFont));

                // ��������� ���������� ������� � ������
                if (Helper.Korzina.Count() >= 3)
                {
                    pdfDoc.Add(new Paragraph("Delivery Time: 3 days"));
                }
                else
                {
                    pdfDoc.Add(new Paragraph("Delivery Time: 6 days"));
                }

                pdfDoc.Close();
            }

            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
    // �������� ������ �� �������
    private void DeleteInBasket_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var selet = List_Box_Korz.SelectedItem as SpisokTovar;//������������ ������ �����
        Helper.Korzina.Remove(selet);// ������� ����� �� �������
        sill();
    }
    // ���������� ������
    public void sill()
    {
        Search = Helper.Korzina.GroupBy(x => x.Id)
        .Select(g => new SpisokTovar
        {
            Id = g.Key,
            Name = g.First().Name,
            Prise = g.First().Prise,
            Photo = g.First().Photo,
            Opisanie = g.First().Opisanie,
            Manufacturer = g.First().Manufacturer,
            Skidka = g.First().Skidka,
            Count = g.Sum(x => x.Count) // ��������� ���������� ��� ������� ������
        }).ToList();

        // ����� �������� �� ����������
        if (ComBox.SelectedIndex > 0)
        {
            var selectedPunktvidasci = (Punktvidasci)ComBox.SelectedItem; // �������� ����� ������
        }
        // ��������� ������������ ������
        Summa.Text = Convert.ToString(Helper.Korzina.Sum(x => x.Prise * x.Count) + " RUB");
        List_Box_Korz.ItemsSource = Search;
    }

    //�������� ������� � �������
    private void SpisokTovats_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow window = new MainWindow();
        window.Show();
        Close();
    }
}