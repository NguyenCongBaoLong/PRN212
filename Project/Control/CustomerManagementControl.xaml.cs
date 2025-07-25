using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Project.Control
{
    /// <summary>
    /// Interaction logic for CustomerManagementControl.xaml
    /// </summary>
    public partial class CustomerManagementControl : UserControl
    {
        private Prn212ProjectContext _context;
        private ObservableCollection<Customer> _customerDetails;

        public CustomerManagementControl()
        {
            InitializeComponent();
            InitializeDbContextAndLoadData();
        }
        private void InitializeDbContextAndLoadData()
        {
            // Quan trọng: Dispose context cũ nếu nó tồn tại để giải phóng tài nguyên
            _context?.Dispose();
            _context = new Prn212ProjectContext();
            _customerDetails = new ObservableCollection<Customer>(); // Khởi tạo lại ObservableCollection
            LoadCustomers(); // Tải tất cả khách hàng khi khởi tạo (không có tham số tìm kiếm)
            ClearInputFields(); // Xóa các trường nhập liệu
            dgCustomers.SelectedItem = null; // Bỏ chọn trên DataGrid
            txtSearch.Clear(); // Xóa ô tìm kiếm
        }
        private void LoadCustomers(string searchTerm = null)
        {
            IQueryable<Customer> query = _context.Customers;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Tìm kiếm theo DisplayName, Address, Phone, hoặc Email
                query = query.Where(c => c.DisplayName.Contains(searchTerm) ||
                                         c.Address.Contains(searchTerm) ||
                                         c.Phone.Contains(searchTerm) ||
                                         c.Email.Contains(searchTerm));
            }

            _customerDetails = new ObservableCollection<Customer>(query.ToList());
            dgCustomers.ItemsSource = _customerDetails;
        }

        // Xử lý sự kiện khi một hàng trong DataGrid được chọn
        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Kiểm tra xem có một Customer nào đang được chọn không
            if (dgCustomers.SelectedItem is Customer selectedCustomer)
            {
                // Đổ dữ liệu của Customer được chọn vào các TextBox và DatePicker
                txtDisplayName.Text = selectedCustomer.DisplayName;
                txtAddress.Text = selectedCustomer.Address;
                txtPhone.Text = selectedCustomer.Phone;
                txtEmail.Text = selectedCustomer.Email;
                txtMoreInfo.Text = selectedCustomer.MoreInfo;
                dpContractDate.SelectedDate = selectedCustomer.ContractDate;
            }
            else
            {
                // Nếu không có Customer nào được chọn (ví dụ: sau khi xóa), thì làm trống các ô nhập liệu
                ClearInputFields();
            }
        }

        // Phương thức làm trống các ô nhập liệu
        private void ClearInputFields()
        {
            txtDisplayName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMoreInfo.Text = string.Empty;
            dpContractDate.SelectedDate = null; // Đặt ngày thành null
            dgCustomers.SelectedItem = null; // Bỏ chọn trên DataGrid để ClearInputFields cũng được gọi khi bỏ chọn
        }

        // Xử lý sự kiện Click cho nút "Thêm"
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào cơ bản
            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageBox.Show("Tên khách hàng không được để trống.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Tạo một đối tượng Customer mới từ dữ liệu trong các ô nhập liệu
                Customer newCustomer = new Customer
                {
                    DisplayName = txtDisplayName.Text,
                    Address = txtAddress.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text,
                    MoreInfo = txtMoreInfo.Text,
                    // Nếu dpContractDate.SelectedDate là null, nó sẽ gán null cho ContractDate
                    ContractDate = dpContractDate.SelectedDate
                };

                _context.Customers.Add(newCustomer); // Thêm vào DbSet
                _context.SaveChanges(); // Lưu thay đổi vào database

                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadCustomers(); // Tải lại danh sách để hiển thị khách hàng mới
                ClearInputFields(); // Làm trống các ô nhập liệu sau khi thêm
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm khách hàng: {ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xử lý sự kiện Click cho nút "Sửa"
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Lấy Customer hiện tại đang được chọn trong DataGrid
            if (dgCustomers.SelectedItem is Customer selectedCustomer)
            {
                // Kiểm tra dữ liệu đầu vào cơ bản
                if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
                {
                    MessageBox.Show("Tên khách hàng không được để trống.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
                    // Cập nhật các thuộc tính của đối tượng được chọn bằng dữ liệu từ các ô nhập liệu
                    selectedCustomer.DisplayName = txtDisplayName.Text;
                    selectedCustomer.Address = txtAddress.Text;
                    selectedCustomer.Phone = txtPhone.Text;
                    selectedCustomer.Email = txtEmail.Text;
                    selectedCustomer.MoreInfo = txtMoreInfo.Text;
                    selectedCustomer.ContractDate = dpContractDate.SelectedDate;

                    // Đánh dấu đối tượng là đã được chỉnh sửa để Entity Framework biết cần cập nhật
                    _context.Entry(selectedCustomer).State = EntityState.Modified;
                    _context.SaveChanges(); // Lưu thay đổi vào database

                    MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadCustomers(); // Tải lại danh sách để đảm bảo DataGrid được cập nhật
                    ClearInputFields(); // Làm trống các ô nhập liệu
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật khách hàng: {ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Xử lý sự kiện Click cho nút "Xóa"
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Lấy Customer hiện tại đang được chọn trong DataGrid
            if (dgCustomers.SelectedItem is Customer selectedCustomer)
            {
                // Hỏi xác nhận trước khi xóa
                MessageBoxResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa khách hàng '{selectedCustomer.DisplayName}'?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Customers.Remove(selectedCustomer); // Xóa khỏi DbSet
                        _context.SaveChanges(); // Lưu thay đổi vào database

                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        LoadCustomers(); // Tải lại danh sách sau khi xóa
                        ClearInputFields(); // Làm trống các ô nhập liệu
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa khách hàng: {ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Xử lý sự kiện Click cho nút "Làm mới" (Clear)
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
        }
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomers(txtSearch.Text);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadCustomers(txtSearch.Text);
            }
        }
    }
}
