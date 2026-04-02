using System;
using System.Collections.Generic;

namespace лаб5
{
    /// <summary>
    /// Варіант 7 — «Меблі»: модель об'єкта, властивості якого потім читаються через рефлексію у Form1.
    /// </summary>
    public class Furniture
    {
        // Чотири властивості різних типів (одна — колекція рядків), як у методичці.
        public string Name { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public List<string> Colors { get; set; }

        // Конструктор без параметрів — валідний початковий стан усіх властивостей.
        public Furniture()
        {
            Name = "Невідомо";
            Material = "Невідомо";
            Price = 0;
            Colors = new List<string>();
        }

        // Конструктор з усіма властивостями — зручно для тестового об'єкта на формі.
        public Furniture(string name, string material, decimal price, List<string> colors)
        {
            Name = name;
            Material = material;
            Price = price;
            Colors = colors ?? new List<string>();
        }

        // Методи за завданням (у дереві вони не виводяться — показуються лише властивості через Reflection).
        public void AddColor(string color)
        {
            if (Colors == null)
                Colors = new List<string>();
            Colors.Add(color);
        }

        public int GetColorsCount()
        {
            return Colors != null ? Colors.Count : 0;
        }

        public string GetInfo()
        {
            return string.Format("{0}, {1}, {2}, кольорів: {3}", Name, Material, Price, GetColorsCount());
        }
    }
}
