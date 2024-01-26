using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Student student = new Student();
        public MainWindow()
        {
            InitializeComponent();
            Button button = new Button();
            button.Content = "Кнопка из кода";
            button.Width = 200;
            button.Padding = new Thickness(5);
            button.Click += Button_Click;
            button.HorizontalAlignment = HorizontalAlignment.Left;
            grid1.Children.Add(button);

            // привязки внутри TextBox text1 
            // теперь будут работать с объектом 
            // student
            DataContext = student;
            Grid.SetRow(button, 1);
            Grid.SetColumn(button, 1);
// Binding (байдинг) данных
// Свойства объявленные как DependencyProperty 
// поддерживают привязку (Binding)
// DependencyProperty доступны в объектах
// унаследованных от DependencyObject


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            student.Group.Title = "1125";
        }
    }

    class Student
    { 
        public string Name { get; set; }
        public Group Group { get; set; } = new();
        public DateTime Birthday { get; set; } = DateTime.Parse("29.02.2000");
    }
    // можно унаследовать Group от DependencyObject
    // для того, чтобы иметь возможность создать
    // свойства зависимости, которые будут генерировать
    // уведомления для интерфейса о своем изменении
    /* class Group : DependencyObject
     {
         public string Title
         {
             get { 
                 return (string)GetValue(TitleProperty);
             }
             set { 
                 SetValue(TitleProperty, value); 
             }
         }

         // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
         public static readonly DependencyProperty TitleProperty =
             DependencyProperty.Register("Title", typeof(string), typeof(Group), null);
     }*/

    // можно реализовать интерфейс INotifyPropertyChanged
    // чтобы вручную отправлять уведомления об
    // изменении свойств в интерфейс
    public class Group : INotifyPropertyChanged
    {
        private string title;

        public string Title
        {
            get => title;
            set
            {
                title = value;
                Signal(nameof(Title));
                Signal();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        //void Signal(string property)
        //{
        //    PropertyChanged?.Invoke(this,
        //        new PropertyChangedEventArgs(property));
        //}
        // CallerMemberName - подставит имя метода, который вызвал данный метод
        // если значение не указано явно
        void Signal([CallerMemberName]string property = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(property));
        }
    }
}
