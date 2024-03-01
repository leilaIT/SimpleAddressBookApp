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

    //current features:
    //-add contact
    //-read from csv
    //-write to csv
    //-view selected contacts
    //-new contact(clear fields)
    //-update adbook info while in app
    //-update adbook info in csv
    //-search function
    //-added clear for search
    //-fixed UI
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
            string tempWord = "";
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    tempArr = line.Split(',');
                    for (int x = 0; x < tempArr.Length; x++)
                    {
                        if (tempArr[x][0] == ' ') //if first letter is space
                        {
                            tempWord = tempArr[x];
                            tempArr[x] = "";
                            for (int i = 1; i < tempWord.Length; i++)
                                tempArr[x] += tempWord[i];
                        }
                    }
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
        private void overwriteFile(string fileName)
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

        #region Textchanged_events
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
        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            btnUpdate.IsEnabled = false;
            for(int x = 0; x < adBookInfo.Count;x++)
            {
                if (adBookInfo[x][0].ToLower().Contains(tbSearch.Text.ToLower()))
                {
                    assignValuetoArr(adBookInfo[x]);
                }
            }
        }
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
            btnAdd.IsEnabled = true;
            if (e.Key == Key.Enter)
            {
                infos[3] = tbEmailAd.Text;
            }
        }
        #endregion

        #region Button_events
        private void btnSearch_Contact(object sender, RoutedEventArgs e)
        {
            clearSearchBar();
        }
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
        private void btnClear_Contact(object sender, RoutedEventArgs e)
        {
            btnAdd.IsEnabled = false;
            btnUpdate.IsEnabled = false;
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
                        overwriteFile("adBook.csv");
                        break;
                    }
                }
            }
            else
                MessageBox.Show("Invalid selection");
            btnUpdate.IsEnabled = false;
        }
        #endregion

        #region Other_stuff
        private void lbView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearSearchBar();
            btnUpdate.IsEnabled = true;
            currIndex = lbView.SelectedIndex;
            if (currIndex >= 0 && currIndex < adBookInfo.Count)
            {
                tempArr = adBookInfo[currIndex];
                assignValuetoArr(tempArr);
            }
            else
                MessageBox.Show("Invalid selection");
        }
        private void clearSearchBar()
        {
            btnUpdate.IsEnabled = false;
            tbSearch.Text = "";
        }
        private void assignValuetoArr(string[] assignArr)
        {
            tbName.Text = assignArr[0];
            tbAddress.Text = assignArr[1];
            tbNumber.Text = assignArr[2];
            tbEmailAd.Text = assignArr[3];
        }
        #endregion
    }
}