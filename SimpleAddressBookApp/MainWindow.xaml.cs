using System.IO;
using System.Net;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SimpleAddressBookApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
       //REQS
    //-Must be able to save contacts to a .csv file
    //-Name [1]
    //-Address [1]
    //-Phone Number [1]
    //-Email Address [1]
    //-Add contacts [1] (idea: once ADD or UPDATE are triggered, change isEnabled property of relevant components)
    //-Update contacts [1]
    //-Must be able to search through the list [1]

    //DOCU
    //-Explaining the controls(buttons, labels, listbox, etc)
    //-Why you chose them and how they interact with the user
    //-Include the code for it
    //-Screenshots of the program running
    public partial class MainWindow : Window
    {
        private bool addtoList = false;
        private int currIndex = 0;
        private string[] tempArr = new string[] { };
        private List<string[]> adBookInfo = new List<string[]>(); //stores all info of contacts
        private string[] infos = new string[4]; //temp arr for infos
        //0-name
        //1-address
        //2-phone number
        //3-email address
        public MainWindow()
        {
            InitializeComponent();
            readFromCSV("adBook.csv");
        }
        #region CSV_stuff
        private void readFromCSV(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    tempArr = line.Split(',');
                    lbView.Items.Add(tempArr[0]);
                    adBookInfo.Add(tempArr);
                }
            }
            tempArr = new string[] { };
        }
        private void writeToFile(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                for(int x = adBookInfo.Count - 1; x > adBookInfo.Count - 2; x--)
                {
                    sw.WriteLine($"{adBookInfo[x][0]}, {adBookInfo[x][1]}, {adBookInfo[x][2]}, {adBookInfo[x][3]}");
                }
            }
        }
        private void writeForUpdatedInfo(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.Write("");
            }

            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                for (int x = 0; x < adBookInfo.Count; x++)
                {
                    sw.WriteLine($"{adBookInfo[x][0]}, {adBookInfo[x][1]}, {adBookInfo[x][2]}, {adBookInfo[x][3]}");
                }
            }
        }
        #endregion

        #region Textchanged_stuff
        private void Txtbox_name(object sender, TextChangedEventArgs e)
        {
        }
        private void Txtbox_address(object sender, TextChangedEventArgs e)
        {
        }
        private void Txtbox_phonenum(object sender, TextChangedEventArgs e)
        {
        }
        private void Txtbox_emailad(object sender, TextChangedEventArgs e)
        {
        }
        private void Txtbox_search(object sender, TextChangedEventArgs e)
        {
        }
        #endregion

        #region KeyUp_events
        private void tbName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                infos[0] = tbName.Text;
                tbAddress.Focus();
            }
        }
        private void tbAddress_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                infos[1] = tbAddress.Text;
                tbNumber.Focus();
            }
        }
        private void tbNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                infos[2] = tbNumber.Text;
                tbEmailAd.Focus();
            }
        }
        private void tbEmailAd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                infos[3] = tbEmailAd.Text;
                btnAdd.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Button_events
        private void btnAdd_Contact(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < infos.Length; x++)
            {
                if (infos[x] == "" || lbView.Items.Contains(infos[0]) || lbView.Items.Contains(infos[2]))
                {
                    addtoList = false;
                    break;
                }
                else
                    addtoList = true;
            }

            if (addtoList)
            {
                lbView.Items.Add(infos[0]);
                adBookInfo.Add(infos);
                writeToFile("adBook.csv");
            }
            else
                MessageBox.Show("Invalid Input. Either the contact already exists or not all fields are filled out.");
        }
        private void btnNew_Contact(object sender, RoutedEventArgs e)
        {
            btnAdd.Visibility = Visibility.Hidden;
            tbName.Text = "";
            tbAddress.Text = "";
            tbNumber.Text = "";
            tbEmailAd.Text = "";
        }
        private void btnUpdate_Contact(object sender, RoutedEventArgs e)
        {
            currIndex = lbView.SelectedIndex;
            if (currIndex >= 0 && currIndex < adBookInfo.Count)
            {
                for(int x = 0; x < adBookInfo.Count; x++)
                {
                    if (x == currIndex)
                    {
                        adBookInfo[x][0] = tbName.Text;
                        adBookInfo[x][1] = tbAddress.Text;
                        adBookInfo[x][2] = tbNumber.Text;
                        adBookInfo[x][3] = tbEmailAd.Text;
                        writeForUpdatedInfo("adBook.csv");
                        break;
                    }
                }
            }
            else
                MessageBox.Show("Invalid selection");
        }
        #endregion
        private void lbView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currIndex = lbView.SelectedIndex;
            if (currIndex >= 0 && currIndex < adBookInfo.Count)
            {
                tempArr = adBookInfo[currIndex];
                tbName.Text = tempArr[0];
                tbAddress.Text = tempArr[1];
                tbNumber.Text = tempArr[2];
                tbEmailAd.Text = tempArr[3];
            }
            else
                MessageBox.Show("Invalid selection");
        }
    }
}