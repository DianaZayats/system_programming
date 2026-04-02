using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace лаб5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Точка демонстрації: після завантаження форми створюється об'єкт і передається в метод з рефлексією.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            Furniture furniture = new Furniture(
                "Стіл",
                "Дерево",
                4500,
                new List<string> { "Білий", "Чорний", "Коричневий" }
            );
            ShowObjectPropertiesInTreeView(furniture);
        }

        /// <summary>
        /// Динамічна ідентифікація: ім'я та тип властивостей не прописані вручну — зчитуються під час виконання.
        /// Перелік властивостей об'єкта виводиться під кореневим вузлом з іменем класу (зв'язок із Type як ядром рефлексії).
        /// </summary>
        private void ShowObjectPropertiesInTreeView(object obj)
        {
            treeView1.Nodes.Clear();
            if (obj == null)
                return;

            Type type = obj.GetType();
            TreeNode rootNode = new TreeNode("Клас: " + type.Name);
            treeView1.Nodes.Add(rootNode);

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                object value = prop.GetValue(obj);
                string typeName = prop.PropertyType.Name;
                string name = prop.Name;

                if (value != null && value is IEnumerable && !(value is string))
                {
                    TreeNode collectionNode = new TreeNode(string.Format("{0} {1}", typeName, name));
                    foreach (object item in (IEnumerable)value)
                    {
                        string itemText = item != null ? item.ToString() : string.Empty;
                        collectionNode.Nodes.Add(new TreeNode(itemText));
                    }
                    rootNode.Nodes.Add(collectionNode);
                }
                else
                {
                    string valueText = value != null ? value.ToString() : string.Empty;
                    string nodeText = string.Format("{0} {1} = {2}", typeName, name, valueText);
                    rootNode.Nodes.Add(new TreeNode(nodeText));
                }
            }

            rootNode.ExpandAll();
        }
    }
}
