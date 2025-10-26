using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace exercise1
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


        private void StartThreadClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        ProgressBar.Value = i;
                        UpdateText.Text = $"Выполнено: {i}%";
                    });

                    Thread.Sleep(50);
                }
            });

            thread.IsBackground = true;
            thread.Start();
        }

        private void StartPriorityThreadClick(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(() =>
            {
                Thread.CurrentThread.Priority = ThreadPriority.Normal;

                for (int i = 0; i <= 33; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        ProgressBar.Value = i;
                        UpdateText.Text = $"Фаза 1/3: Normal приоритет";
                    });
                    Thread.Sleep(100);
                }

                Thread.CurrentThread.Priority = ThreadPriority.Highest;

                for (int i = 33; i <= 66; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        ProgressBar.Value = i;
                        UpdateText.Text = $"Фаза 2/3: Highest приоритет";
                    });
                    Thread.Sleep(100);
                }

                Thread.CurrentThread.Priority = ThreadPriority.Lowest;

                for (int i = 66; i <= 100; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        ProgressBar.Value = i;
                        UpdateText.Text = $"Фаза 3/3: Lowest приоритет";
                    });
                    Thread.Sleep(100);
                }

                Dispatcher.Invoke(() =>
                {
                    UpdateText.Text = "Завершено!\nВсе фазы выполнены";
                });
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private void UpdateUIClick(object sender, RoutedEventArgs e)
        {
            UpdateText.Text = "Запуск демонстрации ошибки обновления UI без Dispatcher";

            var thread = new Thread(() =>
            {
                try
                {
                    UpdateText.Text = "Обновление из фонового потока...";
                    ProgressBar.Value = 50;
                }
                catch (InvalidOperationException ex)
                {
                    string errorMessage = $"ОШИБКА: {ex.GetType().Name}\n{ex.Message}";

                    Dispatcher.Invoke(() =>
                    {
                        UpdateText.Text = "Произошла ошибка!" + "\n" + errorMessage;
                    });
                }
                catch (Exception ex)
                {
                    string errorMessage = $"НЕОЖИДАННАЯ ОШИБКА: {ex.GetType().Name}\n{ex.Message}";
                    UpdateText.Text = errorMessage;
                }
            });

            thread.IsBackground = true;
            thread.Start();
        }
    }
}