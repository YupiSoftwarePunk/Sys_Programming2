using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace exercise2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void StartClicked(object sender, RoutedEventArgs e)
        {
            var thread1 = new Thread(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;

                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    Dispatcher.Invoke(() =>
                    {
                        Progress1.Text = $"1 поток, Выполнено: {i}%";
                    });
                }
            });

            var thread2 = new Thread(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Normal;

                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    Dispatcher.Invoke(() =>
                    {
                        Progress2.Text = $"2 поток, Выполнено: {i}%";
                    });
                }
            });

            var thread3 = new Thread(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;

                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(100);
                    Dispatcher.Invoke(() =>
                    {
                        Progress3.Text = $"3 поток, Выполнено: {i}%";
                    });
                }
            });

            thread1.IsBackground = true;
            thread2.IsBackground = true;
            thread3.IsBackground = true;

            thread1.Start();
            thread2.Start();
            thread3.Start();


            var closeThread = new Thread(() =>
            {
                thread1.Join();
                thread2.Join();
                thread3.Join();


                Dispatcher.Invoke(() =>
                {
                    Progress1.Text = "1 поток завершен";
                    Progress2.Text = "2 поток завершен";
                    Progress3.Text = "3 поток завершен";
                });
            });

            closeThread.Start();
        }
    }
}