using Project.Control;
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
using System.Windows.Shapes;

namespace Project.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();

            // Gán các sự kiện Click cho các nút sidebar
            // Khi một nút được nhấn, chúng ta sẽ gán một instance mới của UserControl tương ứng vào contentArea.
            btnManageInput.Click += BtnManageInput_Click;
            btnManageOutput.Click += BtnManageOutput_Click;
            btnManageObjects.Click += BtnManageObjects_Click;
            btnManageUnits.Click += BtnManageUnits_Click;
            btnManageSuppliers.Click += BtnManageSuppliers_Click;
            btnManageCustomers.Click += BtnManageCustomers_Click;
            btnManageUsers.Click += BtnManageUsers_Click;
            btnLogout.Click += BtnLogout_Click;

            // Tùy chọn: Tải một UserControl mặc định khi khởi động ứng dụng
            // Ví dụ: Hiển thị quản lý khách hàng ngay khi ứng dụng mở
            // contentArea.Content = new CustomerManagementControl();
        }

        // --- Event Handlers cho từng nút chức năng ---

        private void BtnManageInput_Click(object sender, RoutedEventArgs e)
        {

            contentArea.Content = new InputManagementControl();

        }

        private void BtnManageOutput_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = new OutputManagementControl();
        }

        private void BtnManageObjects_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = new ObjectManagementControl();
        }

        private void BtnManageUnits_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = new UnitManagementControl();
        }

        private void BtnManageSuppliers_Click(object sender, RoutedEventArgs e)
        {
            contentArea.Content = new SupplierManagementControl();
        }

        private void BtnManageCustomers_Click(object sender, RoutedEventArgs e)
        {
            // Đây là UserControl quản lý khách hàng mà chúng ta đã làm việc
            contentArea.Content = new CustomerManagementControl();
        }

        private void BtnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            // Thay thế "UserManagementControl" bằng tên UserControl thực tế của bạn
            contentArea.Content = new UserManagementControl();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Hide();
                LoginWindow loginWindow = new LoginWindow();
                this.Close();
            }
        }
    }
}
