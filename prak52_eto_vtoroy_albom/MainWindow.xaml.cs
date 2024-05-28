using DeSerializer;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prak52_eto_vtoroy_albom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<notes> notes;
        List<notes> notes_use = new List<notes>();
        int tmp_id = -1;
        public MainWindow()
        {
            InitializeComponent();
            notes = jsonka.deser<List<notes>>("notes.json") ?? new List<notes>();
            date.SelectedDate = DateTime.Now;
            set();
        }

        private void CUD(object sender, RoutedEventArgs e)
        {
            if ((sender as Button) == panel.Children[0])
            {
                foreach (var a in txtboxes.Children) if ((a as TextBox).Text == "") return;
                notes.Add(new notes((txtboxes.Children[0] as TextBox).Text, (txtboxes.Children[1] as TextBox).Text, date.SelectedDate.Value.ToShortDateString()));
            }
            else if ((sender as Button) == panel.Children[1] && data.SelectedItem != null)
            {
                foreach (var a in txtboxes.Children) if ((a as TextBox).Text == "") return;
                notes[tmp_id] = new notes((txtboxes.Children[0] as TextBox).Text, (txtboxes.Children[1] as TextBox).Text, date.SelectedDate.Value.ToShortDateString());
            }
            else if ((sender as Button) == panel.Children[2] && data.SelectedItem != null)
            {
                notes.RemoveAt(tmp_id);
            }
            jsonka.serial("notes.json", notes);
            set();
            tmp_id = -1;
        }

        void set()
        {
            data.ItemsSource = null;
            notes_use.Clear();
            foreach (var a in notes) if (a.date == date.SelectedDate.Value.ToShortDateString()) notes_use.Add(a);
            data.ItemsSource = notes_use;
        }
        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data.SelectedIndex != -1)
            {
                dynamic a = notes[data.SelectedIndex];
                Type type = a.GetType();
                for (int i = 0; i < type.GetProperties().Length; i++) (txtboxes.Children[i] as TextBox).Text = type.GetProperties()[i].GetValue(a).ToString();
            }
            for (int i = 0; i < notes.Count; i++)
            {
                if (notes[i].title == (txtboxes.Children[0] as TextBox).Text && notes[i].description == (txtboxes.Children[1] as TextBox).Text && notes[i].date == date.SelectedDate.Value.ToShortDateString())
                {
                    tmp_id = i;
                    break;
                }
            }
        }

        private void data_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var a in data.Columns.Select(i => i.Header.ToString()).ToList())
            {
                TextBlock tb = new TextBlock();
                tb.Text = a.ToString();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Width = 100;
                tb.FontSize = 16;
                tb.TextAlignment = TextAlignment.Center;
                tb.Margin = new Thickness(10, 0, 10, 0);
                tb.Foreground = new SolidColorBrush(Colors.Black);
                TextBox tbx = new TextBox();
                tbx.BorderThickness = new Thickness(0, 0, 0, 1);
                tbx.Background = new SolidColorBrush(Colors.Transparent);
                tbx.Width = 100;
                tbx.TextWrapping = TextWrapping.Wrap;
                tbx.Margin = new Thickness(10, 0, 10, 30);
                tbx.TextAlignment = TextAlignment.Center;
                tbx.FontSize = 16;
                tbx.BorderBrush = new SolidColorBrush(Colors.Black);
                tbx.Foreground = new SolidColorBrush(Colors.Black);
                titles.Children.Add(tb);
                txtboxes.Children.Add(tbx);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Изменить язык")
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary { Source = new Uri("pack://application:,,,/languages;component/eng_theme.xaml") });
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary { Source = new Uri("pack://application:,,,/languages;component/ru_theme.xaml") });
            }
        }

        private void date_CalendarClosed(object sender, RoutedEventArgs e) => set();
    }
}
