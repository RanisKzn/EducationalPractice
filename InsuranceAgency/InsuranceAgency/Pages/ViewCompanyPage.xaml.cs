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
    /// Логика взаимодействия для ViewCompanyPage.xaml
    /// </summary>
    public partial class ViewCompanyPage : Page
    {
        public ViewCompanyPage()
        {
            InitializeComponent();
            DGridCompany.ItemsSource = EducationalPracticeBDEntities1.GetContext().Company.ToList();
        }

        
    }
}
