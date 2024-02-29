using System.IO;
using System.Net;
using System.Text;
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
        bool addtoList = false;
        string[] tempArr = new string[] { };
        List<string[]> adBookInfo = new List<string[]>(); //stores all info of contacts
        string[] infos = new string[4]; //temp arr for infos
        //0-name
        //1-address
        //2-phone number
        //3-email address
        public MainWindow()
        {
            InitializeComponent();
            readFromCSV("adBook.csv");
        }
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
        }
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
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                infos[0] = tbName.Text;
                tbAddress.Focus();
            }
        }
        private void tbAddress_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                infos[1] = tbAddress.Text;
                tbNumber.Focus();
            }
        }
        private void tbNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                infos[2] = tbNumber.Text;
                tbEmailAd.Focus();
            }
        }
        private void tbEmailAd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
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
                if (infos[x] == "" /*|| lbView.Items.Contains(infos[0]) || lbView.Items.Contains(infos[2])*/)
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
                //adBookInfo.Add(infos);
            }
            else
                MessageBox.Show("Invalid Input. Either the contact already exists or not all fields are filled out.");
        }
        private void btnNew_Contact(object sender, RoutedEventArgs e)
        {
            btnAdd.Visibility = Visibility.Hidden;
            //infos = new string[4];
        }
        private void btnUpdate_Contact(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        private void lbView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbView.Items.Contains(lbView.SelectedItem))
            {

            }
        }
    }
}