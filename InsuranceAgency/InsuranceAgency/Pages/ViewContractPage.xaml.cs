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
    /// Логика взаимодействия для ViewContractPage.xaml
    /// </summary>
    public partial class ViewContractPage : Page
    {
        public ViewContractPage()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ContractEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var contractForRemobing = DGridContract.SelectedItems.Cast<Contract>().ToList();
            if( MessageBox.Show($"Вы точно хотите удалить следующие {contractForRemobing.Count()} элементов? ", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    EducationalPracticeBDEntities1.GetContext().Contract.RemoveRange(contractForRemobing);
                    EducationalPracticeBDEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridContract.ItemsSource = EducationalPracticeBDEntities1.GetContext().Contract.ToList();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void BtnEditEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditContract_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ContractEditPage((sender as Button).DataContext as Contract));
        }

        private void DGridContract_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                EducationalPracticeBDEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridContract.ItemsSource = EducationalPracticeBDEntities1.GetContext().Contract.ToList();
            }
        }
    }
}
