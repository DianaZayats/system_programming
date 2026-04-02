using System;
using System.Windows.Forms;

namespace Lab6_FileIO_Manager
{
    /// <summary>
    /// Точка входу Windows Forms: тут запускається цикл повідомлень ОС і показується головна форма.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// [STAThread] — обов’язково для WinForms: один потік зі «single-threaded apartment»,
        /// щоб елементи керування коректно працювали з COM (під капотом Win32/UI).
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // Сучасний вигляд кнопок і віджетів (теми, візуальні стилі Windows)
            Application.EnableVisualStyles();
            // Текст на контролах через GDI+, а не GDI (узгоджено зі стилями)
            Application.SetCompatibleTextRenderingDefault(false);
            // Запуск головного вікна; метод блокується, поки форму не закриють
            Application.Run(new Form1());
        }
    }
}
