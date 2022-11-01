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

namespace InsuranceAgency.Pages
{
    /// <summary>
    /// Логика взаимодействия для ContractEditPage.xaml
    /// </summary>
    public partial class ContractEditPage : Page
    {
        private Contract _currentContract = new Contract();
        public ContractEditPage(Contract selectedContract)
        {
            InitializeComponent();
            if (selectedContract != null)
                _currentContract = selectedContract;

            DataContext = _currentContract;
            CBInsuranceAgent.ItemsSource = EducationalPracticeBDEntities1.GetContext().InsuranceAgent.ToList();

        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentContract.ConclusionDate == null)
                errors.AppendLine("Укажите корректную дату начала контратка.");
            if (_currentContract.CotractTerm == null)
                errors.AppendLine("Укажите корректную дату конца контракта.");
            if (_currentContract.CotractTerm < DateTime.Now || _currentContract.CotractTerm < _currentContract.ConclusionDate)
                errors.AppendLine("Укажите корректную дату конца, дата не должна быть меньше текущей и меньше даты начала контракта.");
            if (string.IsNullOrWhiteSpace(_currentContract.AmountPayments))
                errors.AppendLine("Выплаты по категориям не должны быть пустыми.");
            if (_currentContract.InsurancePayments <= 0)
                errors.AppendLine("Укажите корректные страховые выплаты, они не могут быть меньше нуля или равняться нулю.");
            if (_currentContract.InsuranceAgent == null)
                errors.AppendLine("Выберите страхового агента.");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentContract.Id == 0)
                EducationalPracticeBDEntities1.GetContext().Contract.Add(_currentContract);


            try
            {
                EducationalPracticeBDEntities1.GetContext().SaveChanges();
                MessageBox.Show("Договор успешно сохранен");
                Manager.MainFrame.GoBack();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
