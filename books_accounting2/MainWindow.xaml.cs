using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.IO;
using System.Windows.Media;
using iTextSharp.text.html.simpleparser;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Windows.Threading;
using System.Reflection;
using iTextSharp.text.html;
using System.Net;
using System.Diagnostics;

namespace books_accounting2
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
        private void Dgrd_RowEditEnding1(object sender, DataGridRowEditEndingEventArgs e)
        {
            UpdateDB();
        }

        string connectionString;
        SqlDataAdapter adapter;
        DataTable booksTable;

        public object Response { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (choose.Text == "Д.Лондон")
            {
                string name = txt1.Text;
                string sqlExpression = "INSERT INTO London (Name) VALUES (N'" + name + "') ;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // добавление
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
                txt1.Clear();
            }
            else if (choose.Text == "Э.Ремарк")
            {
                string name = txt1.Text;
                string sqlExpression = "INSERT INTO Remark (Name) VALUES (N'" + name + "') ;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // добавление
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
                txt1.Clear();
            }
        }
        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(booksTable);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }
        private void GetBD()
        {
            string sql = "SELECT * FROM Knigi";
            booksTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("sp_InsertBook", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
                //  adapter.InsertCommand.Parameters.Add(new SqlParameter("@author", SqlDbType.NVarChar, 50, "Author"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter.Fill(booksTable);
                Dgrd.ItemsSource = booksTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (choose.Text == "Д.Лондон")
            {
                string sql = "SELECT * FROM London";
                booksTable = new DataTable();
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand(sql, connection);
                    adapter = new SqlDataAdapter(command);

                    // установка команды на добавление для вызова хранимой процедуры
                    adapter.InsertCommand = new SqlCommand("sp_InsertBook", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
                    //  adapter.InsertCommand.Parameters.Add(new SqlParameter("@author", SqlDbType.NVarChar, 50, "Author"));
                    SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    connection.Open();
                    adapter.Fill(booksTable);
                    Dgrd.ItemsSource = booksTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            else if (choose.Text == "Э.Ремарк")
            {
                string sql = "SELECT * FROM Remark";
                booksTable = new DataTable();
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand(sql, connection);
                    adapter = new SqlDataAdapter(command);

                    // установка команды на добавление для вызова хранимой процедуры
                    adapter.InsertCommand = new SqlCommand("sp_InsertBook", connection);
                    adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                    adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
                    //  adapter.InsertCommand.Parameters.Add(new SqlParameter("@author", SqlDbType.NVarChar, 50, "Author"));
                    SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    parameter.Direction = ParameterDirection.Output;

                    connection.Open();
                    adapter.Fill(booksTable);
                    Dgrd.ItemsSource = booksTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
        }

        private void Dgrd_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //  e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void ComboBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            string sql = "SELECT * FROM Remark";
            booksTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("sp_InsertBook", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
                //  adapter.InsertCommand.Parameters.Add(new SqlParameter("@author", SqlDbType.NVarChar, 50, "Author"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter.Fill(booksTable);
                Dgrd.ItemsSource = booksTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

        }

        private void ComboBoxItem_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            string sql = "SELECT * FROM London";
            booksTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("sp_InsertBook", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "Name"));
                //  adapter.InsertCommand.Parameters.Add(new SqlParameter("@author", SqlDbType.NVarChar, 50, "Author"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter.Fill(booksTable);
                Dgrd.ItemsSource = booksTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

        }
        void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader)
        {

            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            //Create a new stylesheet

            BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            //Report Header
            BaseFont bfntHead = baseFont;
            Font fntHead = new Font(bfntHead, 16, 1);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
            document.Add(prgHeading);


            //Author
            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = baseFont;
            Font fntAuthor = new Font(btnAuthor, 8, 2);
            prgAuthor.Alignment = Element.ALIGN_RIGHT;
            prgAuthor.Add(new Chunk("Author : Personal development", fntAuthor));
            prgAuthor.Add(new Chunk("\nRun Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
            document.Add(prgAuthor);

            //Add a line seperation
            // Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, , Element.ALIGN_LEFT, 1)));
            // document.Add(p); 

            //Add line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
            //Table header
            BaseFont btnColumnHeader = baseFont;
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1);
            for (int i = 0; i < dtblTable.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                //  cell.BackgroundColor = Color.GRAY;
                cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper()));
                table.AddCell(cell);
            }

            //table Data
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count; j++)
                {
                    table.AddCell(dtblTable.Rows[i][j].ToString());
                }
            }

            var author = "Jack London";
            var spacer = new Paragraph(author)
            {
                SpacingAfter = 60f,
            };
            spacer.Alignment = Element.ALIGN_CENTER;
            document.Add(spacer);

            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (choose.Text == "Д.Лондон")
            {
                try
                {
                    ExportDataTableToPdf(booksTable, @"C:\Microsoft\London.pdf", "2019 Books");
                    Process.Start(@"C:\Microsoft\London.pdf");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
            }
            else if (choose.Text == "Э.Ремарк")
            {
                try
                {
                    ExportDataTableToPdf(booksTable, @"C:\Users\Zeka\source\repos\WpfApp1\WpfApp1\bin\Debug\Remark.pdf", "2019 Books");
                    Process.Start(@"C:\Users\Zeka\source\repos\WpfApp1\WpfApp1\bin\Debug\Remark.pdf");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }
            }

        }
    }
}
