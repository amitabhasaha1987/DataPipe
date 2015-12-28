using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;

namespace SolsticeDataServices.TestTool
{
    public partial class MainWindow : Window
    {
        public delegate void AddNewDevice();
        public MainWindow()
            : base()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // the form will open in Maximize mode
            WindowState = WindowState.Maximized;
            // the form can never be resized
            ResizeMode = ResizeMode.NoResize;
            Loaded -= OnLoaded;
        }

        #region Add Logical Device
        private void AddDevice(object sender, RoutedEventArgs e)
        {

            // Checking if the textbox becomes blank
            if (txt_aldDeviceSerialNumber.Text.Trim() == "")
            {
                MessageBox.Show("Please, give DeviceSerialNumber");
                txt_aldDeviceSerialNumber.Focus();
                return;
            }

            // Checking if the textbox becomes blank
            if (txt_aldLogicalDeviceId.Text.Trim() == "")
            {
                MessageBox.Show("Please, give LogicalDeviceId");
                txt_aldLogicalDeviceId.Focus();
                return;
            }

            // Checking if the textbox becomes blank or zero
            if ((txt_aldCount.Text.Trim() == "") || (txt_aldCount.Text.Trim() == "0"))
            {
                MessageBox.Show("Please, give how many times you want to insert the device");
                txt_aldCount.Focus();
                return;
            }

            string DeviceSerialNo = txt_aldDeviceSerialNumber.Text;
            string LogicalDeviceId = txt_aldLogicalDeviceId.Text;
            string DeviceDefinitionId = cmb_aldDeviceDefinitionId.SelectionBoxItem.ToString();
            string Region = cmb_aldRegion.SelectionBoxItem.ToString();
            int count = Convert.ToInt32(txt_aldCount.Text.Trim());

            // call the ADD method using delegate asynchronously and run the operation that will be executed on the UI thread
            btnAddLogicalDevice.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new AddNewDevice(() => ADD(DeviceSerialNo, LogicalDeviceId, DeviceDefinitionId, Region, count)));

        }

        public void ADD(string DeviceSerialNo, string LogicalDeviceId, string DeviceDefinitionId, string Region, int count)
        {

            #region Kept For Future Reference
            //Action<string, string> act = delegate(string _Name,string _AccessKey)
            //{
            //    btnSubmit.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new AddNewDevice(() => this.ADDNewDevice(Name, AccessKey, count)));
            //JetStreamTestEntities edmxModel = new JetStreamTestEntities();
            //tbl_Device tbl_device = new tbl_Device()
            //{
            //    DeviceName = _Name,
            //    AccessKey = _AccessKey
            //};
            //edmxModel.tbl_Device.Add(tbl_device);
            //edmxModel.SaveChanges();
            //};

            //Parallel.For(0, count, i => { act.Invoke(Name, AccessKey); });
            #endregion

            // Parallel loop causes the usefulness of the all cores in CPU and so the code becomes fast
            Parallel.For(0, count, i =>
            {
                // Calling the AddNewDevice via delegate asynchronously and run the operation that will be executed on the UI thread
                btnAddLogicalDevice.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                    new AddNewDevice(() => this.ADDNewDevice(DeviceSerialNo, LogicalDeviceId, DeviceDefinitionId, Region)));
            });
            MessageBox.Show("Logical device added successfully.");
        }

        public void ADDNewDevice(string DeviceSerialNo, string LogicalDeviceId, string DeviceDefinitionId, string Region)
        {
            // Showing the URL which is used to add the device
            MessageBox.Show("http://115.254.101.27:2268/api/Device/AddLogicalDevice?DeviceSerialNumber=" + DeviceSerialNo.Trim() +
                "&LogicalDeviceId=" + LogicalDeviceId.Trim() + "&DeviceDefinitionId=" + DeviceDefinitionId + "&Region=" + Region);

            HttpResponseMessage Response = null;
            HttpClient client = new HttpClient();
            // Providing the base address where to submit data
            client.BaseAddress = new Uri("http://115.254.101.27:2268/");
            // Providing the Request Header type. Here it is JSON type.
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Calling the API asynchronously and getting the response
            Response = client.GetAsync("http://115.254.101.27:2268/api/Device/AddLogicalDevice?DeviceSerialNumber=" + DeviceSerialNo.Trim() +
            "&LogicalDeviceId=" + LogicalDeviceId.Trim() + "&DeviceDefinitionId=" + DeviceDefinitionId + "&Region=" + Region).Result;
        }

        // preventing the user from unwanted keypress on Device number Count
        private void txtCount_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            int key = (int)e.Key;

            e.Handled = !(key >= 34 && key <= 43 ||
            key >= 74 && key <= 83 || key == 2);
        }

        #endregion

        #region Device Specific Command
        private void DeviceSpecificCommand(object sender, RoutedEventArgs e)
        {
            // The code is for Device Specific Command
        }
        #endregion

        // As there is many more StackPanel here, this event catching the from where the request has come.
        // And enabling and disabling the labels and textboxes likewise.
        public void OpenService(object sender, RequestNavigateEventArgs e)
        {
            // At first hide all the labels and textboxes
            HideAll();

            // Cchecking the NavigateUri property
            string service = e.Uri.ToString();

            // Brush is prepared to change colour of the clicked Hyperlink
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString("#C6802A");

            if (service == "1")
            {
                // Hyperlink's colour is brushed up
                lnkDeviceSpecificCommand.Foreground = brush;
                // StackPanel associated with the Hyperlink going visible
                stkDeviceSpecificCommand.Visibility = Visibility.Visible;
            }
            else if (service == "2")
            {
                lnkGetConfigValluesCommand.Foreground = brush;
                stkGetConfigValluesCommand.Visibility = Visibility.Visible;
            }
            else if (service == "8")
            {
                lnkAddLogicalDevice.Foreground = brush;
                stkAddLogicalDevice.Visibility = Visibility.Visible;
            }
        }

        private void HideAll()
        {
            var bc = new BrushConverter();
            // There are only 3 links are working and 1 is functional
            // Set the text colour of the 3 links to default colour
            lnkDeviceSpecificCommand.Foreground = (Brush)bc.ConvertFrom("#373333");
            lnkGetConfigValluesCommand.Foreground = (Brush)bc.ConvertFrom("#373333");
            lnkAddLogicalDevice.Foreground = (Brush)bc.ConvertFrom("#373333");

            // Set the visibility mode of Stackpanel
            stkDeviceSpecificCommand.Visibility = Visibility.Hidden;
            stkGetConfigValluesCommand.Visibility = Visibility.Hidden;
            stkAddLogicalDevice.Visibility = Visibility.Hidden;
        }
    }
}
