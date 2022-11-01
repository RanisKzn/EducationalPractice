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
using Word = Microsoft.Office.Interop.Word;
namespace InsuranceAgency.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ViewCompany_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ViewCompanyPage());
        }

        private void ViewAgents_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ViewAgentPage());
        }

        private void ViewEmployees_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ViewEmployeesPage());
        }

        private void GetWordList_Click(object sender, RoutedEventArgs e)
        {
            var contracts = EducationalPracticeBDEntities1.GetContext().Contract.ToList();

            var app = new Word.Application();
            Word.Document document = app.Documents.Add();

            int startRowIndex = 1;
            Word.Paragraph paragraph = document.Paragraphs.Add();
            Word.Range range = paragraph.Range;

            range.Text = $"Таблица договоров";
            paragraph.set_Style("Заголовок 1");
            range.InsertParagraphAfter();



            var tableParagraph = document.Paragraphs.Add();
            var tableRange = tableParagraph.Range;
            var clientTable = document.Tables.Add(tableRange, contracts.Count() + 1, 5);
            clientTable.Borders.InsideLineStyle = clientTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            clientTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Word.Range cellRange;
            cellRange = clientTable.Cell(1, 1).Range;
            cellRange.Text = "Начало контракта";
            cellRange = clientTable.Cell(1, 2).Range;
            cellRange.Text = "Конец контракта";
            cellRange = clientTable.Cell(1, 3).Range;
            cellRange.Text = "Выплаты по категориям";
            cellRange = clientTable.Cell(1, 4).Range;
            cellRange.Text = "Страховые выплаты";
            cellRange = clientTable.Cell(1, 5).Range;
            cellRange.Text = "Страховой агент";
            clientTable.Rows[1].Range.Bold = 1;
            clientTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            startRowIndex++;

            foreach (Contract contract in contracts)
            {
                cellRange = clientTable.Cell(startRowIndex, 1).Range;
                cellRange.Text = contract.ConclusionDate.ToShortDateString();
                cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cellRange = clientTable.Cell(startRowIndex, 2).Range;
                cellRange.Text = contract.CotractTerm.ToShortDateString();
                cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cellRange = clientTable.Cell(startRowIndex, 3).Range;
                cellRange.Text = contract.AmountPayments.ToString();
                cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cellRange = clientTable.Cell(startRowIndex, 4).Range;
                cellRange.Text = contract.InsurancePayments.ToString();
                cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                cellRange = clientTable.Cell(startRowIndex, 5).Range;
                cellRange.Text = contract.InsuranceAgent.FIO.ToString();

                startRowIndex++;
            }
            app.Visible = true;
            document.SaveAs2(@"D:\outputFileWord.docx");
            document.SaveAs2(@"D:\outputFilePdf.pdf", Word.WdExportFormat.wdExportFormatPDF);
        
        }

        private void ViewContracts_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ViewContractPage());
        }
    }
}
