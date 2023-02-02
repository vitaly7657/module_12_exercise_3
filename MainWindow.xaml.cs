using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace module_12_exercise_3
{
    public partial class MainWindow : Window
    {
        Storage clientsData;
        public void SelectedFill() //метод выбора счёта для удаления
        {
            if (lv_accounts.SelectedItem != null)
            {
                string[] selectedNumberAcc = (lv_accounts.SelectedItem.ToString()).Split(' '); //разделение строки данных выбанного в lv_accounts счёта на части через разделитель пробел
                string numberAcc = selectedNumberAcc[2]; //номер выбранного счёта
                tb_selected_acc.Text = numberAcc; //номер выбранного счёта записать в tb_selected_acc
            }
        }

        public void AccountsFull() //метод заполнения lv_accounts
        {
            string[] clientListID = (lv_clients.SelectedItem.ToString()).Split(' '); //разделение строки данных выбанного в lv_clients клиента на части через разделитель пробел
            int clID = Convert.ToInt32(clientListID[0]); //извлечение номера клиента из массива                       
            lv_accounts.ItemsSource = clientsData.AccountsDB.FindAll(a => a.ClientID == clID); //заполнение lv_accounts на основе AccountsDB с данным номером клиента
        }
        public MainWindow()
        {
            InitializeComponent();
            clientsData = Storage.CreateStorage(); //создание хранилища
            lv_clients.ItemsSource = clientsData.ClientsDB; //заполнение lv_clients из базы клиентов
            lv_clients.SelectedIndex = 0;
            cb_from_client.ItemsSource = clientsData.ClientsDB; //источник данных для cb_from_client
            cb_to_client.ItemsSource = clientsData.ClientsDB; //источник данных для cb_to_client
        }

        private void lv_clients_SelectionChanged(object sender, SelectionChangedEventArgs e) //дейсткие при выборе строчки в lv_clients
        {
            AccountsFull();
        }

        private void lv_accounts_SelectionChanged(object sender, SelectionChangedEventArgs e) //дейсткие при выборе строчки в lv_accounts
        {
            SelectedFill();
        }

        private void btn_del_acc_Click(object sender, RoutedEventArgs e) //кнопка удаления счёта
        {
            object[] selectedNumberAcc = (lv_accounts.SelectedItem.ToString()).Split(' '); //разделение строки данных выбранного в lv_accounts счёт через разделитель пробел
            int deleteAccID = Convert.ToInt32(selectedNumberAcc[0]); //извленение ID счёта через первый аргумент
            var deleteAcc = clientsData.AccountsDB.FirstOrDefault(p => p.ID == deleteAccID); //поиск в AccountsDB счёта с данным ID
            if (deleteAcc != null) //проверка на наличие счёта в базе
            {
                clientsData.AccountsDB.Remove(deleteAcc); //удаление счёта
            }
            AccountsFull();
            tb_selected_acc.Text = "выбранный счёт";
        }

        private void btn_add_acc_Click(object sender, RoutedEventArgs e) //кнопка добавления счёта
        {
            if (tb_new_acc.Text == "") //проверка заполнения поля нового счёта
            {
                MessageBox.Show("Заполните поле счёта!"); // вывод окна уведомления о неверных данных
            }
            else
            {
                object maxAccID = clientsData.AccountsDB.Max(point => point.ID); //поиск в базе AccountsDB максимального ID
                object[] clientListID = (lv_clients.SelectedItem.ToString()).Split(' '); //разделение строки данных выбранного в lv_clients счёта на части через разделитель пробел
                int clID = Convert.ToInt32(clientListID[0]);
                if (cb_deposit.IsChecked == false)
                {
                    clientsData.AccountAdd(Convert.ToInt32(maxAccID) + 1, clID, tb_new_acc.Text, 0, "недепозитный"); //добавление нового недепозитного счёта
                }
                if (cb_deposit.IsChecked == true)
                {
                    clientsData.AccountAdd(Convert.ToInt32(maxAccID) + 1, clID, tb_new_acc.Text, 0, "депозитный"); //добавление нового депозитного счёта
                }
                AccountsFull();
            }
        }

        private void cb_from_client_SelectionChanged(object sender, SelectionChangedEventArgs e) //действие при выборе в cb_from_client
        {
            string[] clientListID = (cb_from_client.SelectedItem.ToString()).Split(' '); //разделение строки данных выбанного в cb_from_client клиента на части через разделитель пробел
            int clID = Convert.ToInt32(clientListID[0]); //извлечение ID клиента                     
            cb_from_account.ItemsSource = clientsData.AccountsDB.FindAll(a => a.ClientID == clID); //заполнение cb_from_account на основе ID клиента
            cb_from_account.SelectedIndex = 0; //нулевой индекс по умолчанию         
        }

        private void cb_to_client_SelectionChanged(object sender, SelectionChangedEventArgs e) //действие при выборе в cb_to_client
        {
            string[] clientListID = (cb_to_client.SelectedItem.ToString()).Split(' '); //разделение строки данных выбанного в cb_to_client клиента на части через разделитель пробел
            int clID = Convert.ToInt32(clientListID[0]); //извлечение ID клиента

            var depositAcc = new List<Account<int, int, object, object, string>>();
            depositAcc = clientsData.AccountsDB.FindAll(a => a.ClientID == clID); //заполнение произвольной переменной на основе ID клиента

            cb_to_account.ItemsSource = depositAcc.FindAll(a => a.IsDeposit == "депозитный"); //заполнение cb_to_account депозитными счетами
            cb_to_account.SelectedIndex = 0; //нулевой индекс по умолчанию 
        }

        private void btn_transfer_Click(object sender, RoutedEventArgs e) //кнопка перевода средств
        {
            object[] tempFrom = (cb_from_account.SelectedItem.ToString()).Split(' '); //извлечение массива из cb_from_account
            string accountFrom = Convert.ToString(tempFrom[2]); //номер счёта от кого
            int summFrom = Convert.ToInt32(tempFrom[3]); //сумма на счёте от кого

            object[] tempTo = (cb_to_account.SelectedItem.ToString()).Split(' '); //извлечение массива из cb_to_account
            string accountTo = Convert.ToString(tempTo[2]); //номер счёта кому
            int summTo = Convert.ToInt32(tempTo[3]); //сума на счёте кому

            int transferSumm = Convert.ToInt32(tb_transfer_summ.Text); //данные из поля tb_transfer_summ как сумма перевода

            int chechSumm = summFrom - transferSumm; //переменная проверки счедств на счёте

            if (chechSumm < 0) //проверка счедств на счёте
            {
                MessageBox.Show("Недостаточно средств на счёте для осуществления перевода!");
            }

            else if (chechSumm >= 0) //проверка счедств на счёте
            {
                summFrom -= transferSumm; //формирование суммы на счёте от кого
                summTo += transferSumm; //формирование суммы на счёте кому        
            }

            clientsData.AccountsDB.FindAll(a => a.AccountNumber.ToString() == accountFrom).ForEach(b => b.AccountSumm = summFrom); //запись полученной суммы на счёт от кого
            clientsData.AccountsDB.FindAll(a => a.AccountNumber.ToString() == accountTo).ForEach(b => b.AccountSumm = summTo); //запись полученной суммы на счёт кому

            AccountsFull();
        }
    }
}
