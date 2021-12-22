using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TodoApps.Enties;

namespace TodoApps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Todo> todos_;
        DAO.TodoDAO todoDao_;
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            txtTaskTodo.Focus();
        }

        private void LoadData()
        {
            todoDao_ = new DAO.TodoDAO();
            todos_ = todoDao_.GetAll();
            LoadUI();
        }

        private void LoadUI()
        {
            stpTodoList.Children.Clear();
            foreach (Todo todo in todos_)
            {
                if (todo != null)
                {
                    CheckBox checkBox = new CheckBox();
                    TextBlock txbTask = new TextBlock()
                    {
                        Text = todo.task,
                        Tag = todo._id,
                        Margin = new Thickness(30, 0, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                    };

                    Path pathEdit = new Path();
                    pathEdit.Style = (Style)FindResource("pathEdit");
                    Button btnEdit = new Button()
                    {
                        Tag = todo._id,
                        Width = 35,
                        Height = 35,
                        BorderThickness = new Thickness(0),
                        Margin = new Thickness(0, 0, 35, 0),
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Center,
                        Content = pathEdit,
                    };

                    Path pathDelete = new Path();
                    pathDelete.Style = (Style)FindResource("pathTrashCan");
                    Button btnDelete = new Button()
                    {
                        Tag = todo._id,
                        Width = 35,
                        Height = 35,
                        BorderThickness = new Thickness(0),
                        Margin = new Thickness(0, 0, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Center,
                        Content = pathDelete
                    };

                    var converter = new System.Windows.Media.BrushConverter();
                    Border border = new Border()
                    {
                        Height = 35,
                        BorderThickness = new Thickness(0, 0, 0, 1),
                        BorderBrush = (Brush)converter.ConvertFromString("#e7e7e7"),
                        VerticalAlignment = VerticalAlignment.Bottom,
                    };
                    Grid grid = new Grid();
                    grid.Height = 50;

                    grid.Children.Add(checkBox);
                    grid.Children.Add(txbTask);
                    grid.Children.Add(btnEdit);
                    grid.Children.Add(btnDelete);
                    grid.Children.Add(border);

                    stpTodoList.Children.Add(grid);
                    btnEdit.Click += BtnEdit_Click;
                    btnDelete.Click += BtnDelete_Click;
                }
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string newTodo = txtTaskTodo.Text;
                if(todoDao_.Create(new Todo() { task = newTodo }))
                {
                    LoadData();
                    txtTaskTodo.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi phát sinh:" + Environment.NewLine + ex.Message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Todo todoEdit = todos_.Where(x=>x._id == btn.Tag.ToString()).FirstOrDefault();
            if(todoEdit != null)
            {
                txtTastEdit.Text = todoEdit.task;
                btnSave.Tag = todoEdit._id;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag == null)
                return;
            Todo todoEdit = todos_.Where(x => x._id == btn.Tag.ToString()).FirstOrDefault();
            todoEdit.task = txtTastEdit.Text;
            if (todoDao_.Edit(btn.Tag.ToString(), todoEdit))
            {
                txtTastEdit.Clear();
                LoadData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (todoDao_.Delete(btn.Tag.ToString()))
            {
                LoadData();
            }
        }
    }
}
