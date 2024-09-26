using Avalonia.Media.Imaging;
using DEMO_TEST.Context;
using DEMO_TEST.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_TEST
{
    internal class Helper
    {
        public static readonly DimaBaseContext DataBase = new DimaBaseContext(); // Заменил " User738Context " НА " DimaBaseContext "

        public static List<SpisokTovar> Korzina = new List<SpisokTovar>();

        public static int GenNumber()
        {
            Random random = new Random();
            return random.Next(1000, 9999); // генерируем случайный номер между 1000 и 9999
        }

        public static int GenCode()
        {
            Random random = new Random();
            return random.Next(100, 999); // генерируем случайный код между 100 и 999
        }

    }
}