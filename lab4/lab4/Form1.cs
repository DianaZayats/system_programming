using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource _cts;

        // конструктор форми
        public Form1()
        {
            InitializeComponent();
        }

        // початкове налаштування стану UI при завантаженні форми
        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyIdleUiState();
        }

        // статус юі
        private void ApplyIdleUiState()
        {
            buttonStart.Enabled = true;
            buttonCancel.Enabled = false;
            progressBar1.Value = 0;
            labelProgress.Text = "0%";
            labelResult.Text = "Result: -";
            labelStatus.Text = "Idle";
        }

        // фонова довга операція: показує прогрес і повертає результат
        private Task<int> ProcessAsync(int count, IProgress<int> progress, CancellationToken token)
        {
            return Task.Run(async () => // не блокується юі, виконуємо в фоновому потоці
            {
                for (int i = 1; i <= 100; i++)
                {
                    token.ThrowIfCancellationRequested(); // перевірка на скасування, якщо та - зупинка
                    progress?.Report(i);
                    await Task.Delay(50, token).ConfigureAwait(false); // якщо натиснули скасувати
                }

                int sum = 0;
                for (int j = 1; j <= count; j++)
                {
                    token.ThrowIfCancellationRequested();
                    sum += j;
                }

                return sum;
            }, token);
        }

        // запускає асинхронну операцію та оновлює UI
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            _cts?.Dispose(); // якщо попередній токен існує — звільняє його ресурси
            _cts = new CancellationTokenSource();

            var progress = new Progress<int>(percent =>
            {
                progressBar1.Value = percent;
                labelProgress.Text = percent + "%";
            });

            buttonStart.Enabled = false;
            buttonCancel.Enabled = true;
            labelStatus.Text = "Running...";

            try // основна асинхронна робота
            {
                const int count = 100;
                int result = await ProcessAsync(count, progress, _cts.Token).ConfigureAwait(true); // запускає фонову операцію, чекає завершення і отримує результат
                labelResult.Text = "Result: " + result;
                labelStatus.Text = "Completed";
            }
            catch (OperationCanceledException) // операцію було скасовано через токен
            {
                labelStatus.Text = "Cancelled";
                labelResult.Text = "Result: -";
                progressBar1.Value = 0;
                labelProgress.Text = "0%";
            }
            finally
            {
                buttonStart.Enabled = true;
                buttonCancel.Enabled = false;
                _cts?.Dispose();
                _cts = null;
            }
        }

        // надсилає запит на скасування поточної операції
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        // звільняє ресурси при закритті форми
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _cts?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
