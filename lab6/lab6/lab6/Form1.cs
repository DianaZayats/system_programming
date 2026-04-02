using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Lab6_FileIO_Manager
{
    /// <summary>
    /// Головна форма лабораторної №6: один застосунок поєднує три завдання теми «Файлове введення-виведення»:
    /// (1) ієрархія папок і файлів у TreeView;
    /// (2) пошук файлів за ім’ям/маскою з повною інформацією (FileInfo);
    /// (3) графічний файловий менеджер: вибір кореня, дерево, список вмісту, пошук, блок властивостей файлу.
    /// </summary>
    public partial class Form1 : Form
    {
        // --- Допоміжні класи для збереження даних у вузлах інтерфейсу ---

        /// <summary>
        /// Що прив’язано до вузла TreeView: це папка чи файл і який повний шлях на диску.
        /// Без цього ми б не знали, що саме користувач обрав після кліку (лише ім’я вузла недостатньо).
        /// </summary>
        private sealed class TreeNodeTag
        {
            public bool IsDirectory { get; set; }
            public string FullPath { get; set; } = string.Empty;
        }

        /// <summary>
        /// Що прив’язано до рядка ListView: папка чи файл і повний шлях (для переходу в підпапку або показу FileInfo).
        /// </summary>
        private sealed class ListItemTag
        {
            public bool IsDirectory { get; set; }
            public string FullPath { get; set; } = string.Empty;
        }

        public Form1()
        {
            InitializeComponent(); // створює всі кнопки, поля, TreeView тощо з Form1.Designer.cs
        }

        // --- Допоміжні перевірки типу вузла дерева (зручні імена замість ручної перевірки Tag) ---

        private static bool IsDirectoryNode(TreeNode node)
        {
            return node?.Tag is TreeNodeTag t && t.IsDirectory;
        }

        private static bool IsFileNode(TreeNode node)
        {
            return node?.Tag is TreeNodeTag t && !t.IsDirectory;
        }

        // --- Обробники кнопок верхньої панелі ---

        /// <summary>
        /// «Огляд...»: стандартний діалог Windows для вибору папки; обраний шлях підставляємо в textBoxRootPath.
        /// </summary>
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    textBoxRootPath.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                ShowError("Не вдалося відкрити діалог вибору папки.", ex);
            }
        }

        /// <summary>
        /// «Завантажити»: читаємо шлях з поля, перевіряємо, що він не порожній, і будуємо дерево каталогу (завдання 1 + частина 3).
        /// </summary>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            string path = textBoxRootPath.Text.Trim();
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show(this, "Вкажіть шлях до каталогу.", "Завантаження", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LoadDirectoryTree(path);
        }

        /// <summary>
        /// «Пошук»: корінь пошуку — те саме поле шляху; маска — з textBoxSearchPattern. Викликає завдання 2.
        /// </summary>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string root = textBoxRootPath.Text.Trim();
            if (string.IsNullOrEmpty(root))
            {
                MessageBox.Show(this, "Спочатку вкажіть кореневу папку або натисніть «Завантажити».", "Пошук", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string pattern = textBoxSearchPattern.Text.Trim();
            if (string.IsNullOrEmpty(pattern))
            {
                MessageBox.Show(this, "Введіть ім’я файлу або маску (наприклад, *.txt).", "Пошук", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SearchFiles(root, pattern);
        }

        // --- Взаємодія з деревом і списком ---

        /// <summary>
        /// Після вибору вузла в TreeView: якщо папка — показуємо її вміст справа; якщо файл — очищаємо список і показуємо властивості файлу.
        /// Подія AfterSelect спрацьовує після того, як вузол уже виділений.
        /// </summary>
        private void treeViewMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tag = e.Node?.Tag as TreeNodeTag;
            if (tag == null)
            {
                return;
            }

            if (IsDirectoryNode(e.Node))
            {
                ShowDirectoryContents(tag.FullPath);
                textBoxFileInfo.Clear();
            }
            else if (IsFileNode(e.Node))
            {
                try
                {
                    listViewContents.Items.Clear();
                    // Статичний клас File (тема 6.6): перевірка, що файл ще існує перед FileInfo
                    if (!File.Exists(tag.FullPath))
                    {
                        MessageBox.Show(this, "Файл не знайдено на диску.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var fi = new FileInfo(tag.FullPath);
                    ShowFileInfo(fi);
                }
                catch (Exception ex)
                {
                    ShowError("Не вдалося прочитати інформацію про файл.", ex);
                }
            }
        }

        /// <summary>
        /// Вибір рядка в ListView: підпапка — «зайти» і показати вміст; файл — показати FileInfo внизу.
        /// Працює і для звичайного перегляду папки, і для списку результатів пошуку (там лише файли).
        /// </summary>
        private void listViewContents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewContents.SelectedItems.Count == 0)
            {
                return;
            }

            var item = listViewContents.SelectedItems[0];
            var lit = item.Tag as ListItemTag;
            if (lit == null)
            {
                return;
            }

            try
            {
                if (lit.IsDirectory)
                {
                    ShowDirectoryContents(lit.FullPath);
                    textBoxFileInfo.Clear();
                }
                else
                {
                    if (!File.Exists(lit.FullPath))
                    {
                        MessageBox.Show(this, "Файл не знайдено на диску.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var fi = new FileInfo(lit.FullPath);
                    ShowFileInfo(fi);
                }
            }
            catch (Exception ex)
            {
                ShowError("Не вдалося обробити вибір у списку.", ex);
            }
        }

        /// <summary>
        /// Завдання 1: очистка старого дерева, створення кореневого вузла, рекурсивне наповнення, розгортання кореня.
        /// BeginUpdate/EndUpdate — щоб WinForms не перемальовував дерево після кожного доданого вузла (швидше і без миготіння).
        /// </summary>
        private void LoadDirectoryTree(string path)
        {
            treeViewMain.BeginUpdate();
            try
            {
                treeViewMain.Nodes.Clear();
                listViewContents.Items.Clear();
                textBoxFileInfo.Clear();

                DirectoryInfo rootDir;
                try
                {
                    // DirectoryInfo (тема 6.1) — об’єктна модель каталогу: шлях передаємо в конструктор
                    rootDir = new DirectoryInfo(path);
                }
                catch (Exception ex)
                {
                    ShowError("Некоректний шлях до каталогу.", ex);
                    return;
                }

                if (!rootDir.Exists)
                {
                    MessageBox.Show(this, "Каталог не існує: " + path, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var rootNode = new TreeNode(rootDir.Name)
                {
                    Tag = new TreeNodeTag { IsDirectory = true, FullPath = rootDir.FullName }
                };
                treeViewMain.Nodes.Add(rootNode);

                AddDirectoryNodes(rootNode, rootDir);
                rootNode.Expand();
            }
            finally
            {
                treeViewMain.EndUpdate();
            }
        }

        /// <summary>
        /// Рекурсія: для поточної папки (DirectoryInfo) додаємо вузли підпапок і заходимо в кожну глибше;
        /// потім додаємо вузли файлів (FileInfo). Недоступні каталоги тихо пропускаємо — програма не падає.
        /// </summary>
        private void AddDirectoryNodes(TreeNode parentNode, DirectoryInfo dir)
        {
            try
            {
                // Спочатку всі підкаталоги
                DirectoryInfo[] subDirs;
                try
                {
                    subDirs = dir.GetDirectories();
                }
                catch (UnauthorizedAccessException)
                {
                    return;
                }
                catch (DirectoryNotFoundException)
                {
                    return;
                }
                catch (IOException)
                {
                    return;
                }
                catch (Exception)
                {
                    return;
                }

                foreach (DirectoryInfo sub in subDirs)
                {
                    var node = new TreeNode(sub.Name)
                    {
                        Tag = new TreeNodeTag { IsDirectory = true, FullPath = sub.FullName }
                    };
                    parentNode.Nodes.Add(node);
                    try
                    {
                        AddDirectoryNodes(node, sub); // рекурсивний вхід у вкладену папку
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // одна гілка недоступна — решта дерева будується далі
                    }
                    catch (DirectoryNotFoundException)
                    {
                    }
                    catch (IOException)
                    {
                    }
                    catch (Exception)
                    {
                    }
                }

                // Потім файли безпосередньо в цій папці (не всередині підпапок — ті вже в окремих вузлах)
                FileInfo[] files;
                try
                {
                    files = dir.GetFiles();
                }
                catch (UnauthorizedAccessException)
                {
                    return;
                }
                catch (DirectoryNotFoundException)
                {
                    return;
                }
                catch (IOException)
                {
                    return;
                }
                catch (Exception)
                {
                    return;
                }

                foreach (FileInfo file in files)
                {
                    var fileNode = new TreeNode(file.Name)
                    {
                        Tag = new TreeNodeTag { IsDirectory = false, FullPath = file.FullName }
                    };
                    parentNode.Nodes.Add(fileNode);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (IOException)
            {
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Завдання 3 (фрагмент): вміст однієї обраної папки у вигляді таблиці — підпапки та файли з розміром.
        /// Використовуємо DirectoryInfo.GetDirectories() / GetFiles() (теми 6.1–6.2).
        /// </summary>
        private void ShowDirectoryContents(string path)
        {
            listViewContents.Items.Clear();

            try
            {
                if (!Directory.Exists(path))
                {
                    MessageBox.Show(this, "Каталог не знайдено: " + path, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var di = new DirectoryInfo(path);

                foreach (DirectoryInfo d in di.GetDirectories())
                {
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add("Папка");
                    item.SubItems.Add("—");
                    item.Tag = new ListItemTag { IsDirectory = true, FullPath = d.FullName };
                    listViewContents.Items.Add(item);
                }

                foreach (FileInfo f in di.GetFiles())
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add("Файл");
                    item.SubItems.Add(f.Length.ToString());
                    item.Tag = new ListItemTag { IsDirectory = false, FullPath = f.FullName };
                    listViewContents.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                ShowError("Немає доступу до вмісту каталогу.", ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                ShowError("Каталог не знайдено.", ex);
            }
            catch (IOException ex)
            {
                ShowError("Помилка читання каталогу.", ex);
            }
            catch (Exception ex)
            {
                ShowError("Не вдалося показати вміст каталогу.", ex);
            }
        }

        /// <summary>
        /// Завдання 2: пошук файлів за маскою з рекурсивним обходом дерева каталогів.
        /// Перевіряє корінь, далі делегує <see cref="SearchFilesRecursive"/> — помилка доступу в одній папці не зриває весь пошук.
        /// </summary>
        private void SearchFiles(string rootPath, string pattern)
        {
            listViewContents.Items.Clear();
            textBoxFileInfo.Clear();

            // Критична перевірка кореня — без цього сенсу в обході немає; повідомлення одне, на старті
            if (!Directory.Exists(rootPath))
            {
                MessageBox.Show(this, "Кореневий каталог не існує: " + rootPath, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Рекурсія всередині сама ловить відмову доступу по кожній папці (без MessageBox на кожну)
                SearchFilesRecursive(rootPath, pattern);

                if (listViewContents.Items.Count == 0)
                {
                    MessageBox.Show(this, "Файлів за заданою маскою не знайдено.", "Пошук", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (ArgumentException ex)
            {
                // Некоректна маска для EnumerateFiles (наприклад, заборонені символи в шаблоні)
                ShowError("Некоректний шаблон пошуку або шлях.", ex);
            }
            catch (Exception ex)
            {
                ShowError("Критична помилка під час запуску пошуку.", ex);
            }
        }

        /// <summary>
        /// Рекурсивний обхід: у поточній папці шукає файли за маскою (без AllDirectories),
        /// потім заходить у кожну підпапку. Для кожної операції — локальний try/catch, щоб пропускати лише проблемний каталог.
        /// Використовує EnumerateFiles/EnumerateDirectories — послідовне перерахування без збору всього масиву одразу.
        /// </summary>
        private void SearchFilesRecursive(string currentPath, string pattern)
        {
            // Крок 1: лише файли в цій папці (маска без AllDirectories — глибину дає рекурсія нижче)
            try
            {
                foreach (string fullPath in Directory.EnumerateFiles(currentPath, pattern))
                {
                    try
                    {
                        var fi = new FileInfo(fullPath);
                        var item = new ListViewItem(fi.Name);
                        item.SubItems.Add("Файл");
                        item.SubItems.Add(fi.Length.ToString());
                        item.Tag = new ListItemTag { IsDirectory = false, FullPath = fi.FullName };
                        listViewContents.Items.Add(item);
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                    catch (IOException)
                    {
                    }
                    catch (Exception)
                    {
                        // один файл міг зникнути або заблокуватися під час обходу
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (IOException)
            {
            }
            catch (Exception)
            {
                // не вдалося прочитати вміст цієї папки — пропускаємо її файли, але підпапки спробуємо окремо
            }

            // Крок 2: окремий try — навіть якщо перелік файлів у папці зірвався, підкаталоги іншої гілки можуть бути доступні
            try
            {
                foreach (string subDir in Directory.EnumerateDirectories(currentPath))
                {
                    SearchFilesRecursive(subDir, pattern);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (IOException)
            {
            }
            catch (Exception)
            {
                // не вдалося отримати список підпапок — цю гілку дерева не розгортаємо
            }
        }

        /// <summary>
        /// Формуємо текст для поля внизу форми з полів FileInfo (тема 6.5): ім’я, шлях, розмір, дати, атрибути.
        /// </summary>
        private void ShowFileInfo(FileInfo file)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Ім’я файлу: " + file.Name);
            sb.AppendLine("Повний шлях: " + file.FullName);
            sb.AppendLine("Розмір: " + file.Length + " байт");
            sb.AppendLine("Дата створення: " + file.CreationTime.ToString("dd.MM.yyyy HH:mm:ss"));
            sb.AppendLine("Дата зміни: " + file.LastWriteTime.ToString("dd.MM.yyyy HH:mm:ss"));
            sb.AppendLine("Атрибути: " + file.Attributes);
            textBoxFileInfo.Text = sb.ToString();
        }

        /// <summary>
        /// Єдине місце показу нефатальних помилок користувачу через MessageBox.
        /// </summary>
        private void ShowError(string title, Exception ex)
        {
            string message = title + Environment.NewLine + DescribeException(ex);
            MessageBox.Show(this, message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Розрізняємо типи винятків (лабораторна вимога): зрозуміліші повідомлення українською.
        /// </summary>
        private static string DescribeException(Exception ex)
        {
            switch (ex)
            {
                case DirectoryNotFoundException dne:
                    return "Каталог не знайдено: " + dne.Message;
                case UnauthorizedAccessException uae:
                    return "Немає доступу: " + uae.Message;
                case IOException ioe:
                    return "Помилка вводу-виводу: " + ioe.Message;
                default:
                    return ex.GetType().Name + ": " + ex.Message;
            }
        }
    }
}
