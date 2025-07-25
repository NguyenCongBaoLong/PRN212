using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions; // Giữ lại nếu cần các validation phức tạp
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; // Giữ lại nếu cần xử lý input events
using Microsoft.EntityFrameworkCore;
using Project.Models; // Đảm bảo namespace này là đúng

namespace Project.Control
{
    public partial class UserManagementControl : UserControl
    {
        private Prn212ProjectContext _context;
        private ObservableCollection<User> _userDetails;

        public UserManagementControl()
        {
            InitializeComponent();
            _context = new Prn212ProjectContext();
            _userDetails = new ObservableCollection<User>();
            LoadUsers();
            LoadUserRoles();
        }

        private void LoadUsers(string searchTerm = null)
        {
            IQueryable<User> query = _context.Users.Include(u => u.IdRoleNavigation);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Tìm kiếm theo DisplayName hoặc UserName
                query = query.Where(u => u.DisplayName.Contains(searchTerm) || u.UserName.Contains(searchTerm));
            }

            _userDetails = new ObservableCollection<User>(query.ToList());
            dgUserDetails.ItemsSource = _userDetails;
        }

        private void LoadUserRoles()
        {
            cmbUserRole.ItemsSource = _context.UserRoles.ToList();
        }

        private void dgUserDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUserDetails.SelectedItem is User selectedUserDetail)
            {
                txtDisplayName.Text = selectedUserDetail.DisplayName ?? string.Empty;
                txtUserName.Text = selectedUserDetail.UserName ?? string.Empty;
                txtPassword.Text = selectedUserDetail.Password ?? string.Empty; // Cẩn thận với mật khẩu
                cmbUserRole.SelectedValue = selectedUserDetail.IdRole;
            }
            else
            {
                ClearFields();
            }
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            dgUserDetails.SelectedItem = null; 
        }
        private void ClearFields()
        {
            txtDisplayName.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
            cmbUserRole.SelectedIndex = -1;
            dgUserDetails.SelectedItem = null; // Bỏ chọn trên DataGrid
        }
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers(txtSearch.Text);
        }

        // <<<< PHƯƠNG THỨC TÌM KIẾM KHI NHẤN ENTER TRONG Ô TÌM KIẾM >>>>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadUsers(txtSearch.Text);
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                User newUser = new User
                {
                    DisplayName = txtDisplayName.Text,
                    UserName = txtUserName.Text,
                    Password = txtPassword.Text, // LƯU Ý: Không an toàn cho ứng dụng thực tế
                    IdRole = (int)cmbUserRole.SelectedValue
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                // Tải lại các navigation property để hiển thị đúng trong DataGrid
                var addedUser = _context.Users
                                        .Include(u => u.IdRoleNavigation)
                                        .FirstOrDefault(u => u.Id == newUser.Id);
                if (addedUser != null)
                {
                    _userDetails.Add(addedUser);
                }

                MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm người dùng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgUserDetails.SelectedItem is User selectedUserDetail)
            {
                if (!ValidateInput())
                {
                    return;
                }

                try
                {
                    selectedUserDetail.DisplayName = txtDisplayName.Text;
                    selectedUserDetail.UserName = txtUserName.Text;
                    selectedUserDetail.Password = txtPassword.Text; 
                    selectedUserDetail.IdRole = (int)cmbUserRole.SelectedValue;



                    _context.SaveChanges();
                    MessageBox.Show("Cập nhật người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUsers(); 
                    ClearFields();
                }
                catch (DbUpdateException ex) 
                {
                    string errorMessage = "Lỗi khi cập nhật người dùng: ";
                    // Lấy thông báo lỗi chi tiết từ InnerException
                    if (ex.InnerException != null)
                    {
                        errorMessage += ex.InnerException.Message;
                        if (ex.InnerException.InnerException != null) // Có thể có nhiều cấp InnerException
                        {
                            errorMessage += "\nChi tiết: " + ex.InnerException.InnerException.Message;
                        }
                    }
                    else
                    {
                        errorMessage += ex.Message;
                    }
                    MessageBox.Show(errorMessage, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Để debug, bạn cũng có thể in ra console hoặc log file:
                    // Console.WriteLine(ex.ToString());
                }
                catch (Exception ex) // Bắt các loại ngoại lệ khác
                {
                    MessageBox.Show($"Một lỗi không xác định đã xảy ra: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUserDetails.SelectedItem is User selectedUserDetail)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Users.Remove(selectedUserDetail);
                        _context.SaveChanges();

                        _userDetails.Remove(selectedUserDetail); // Xóa khỏi ObservableCollection
                        MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa người dùng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageBox.Show("Tên hiển thị không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Kiểm tra trùng lặp UserName
            int currentItemId = (dgUserDetails.SelectedItem as User)?.Id ?? 0;
            if (_context.Users.Any(u => u.UserName == txtUserName.Text && u.Id != currentItemId))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng nhập tên khác.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Mật khẩu không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // Thêm kiểm tra độ phức tạp của mật khẩu nếu cần

            if (cmbUserRole.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Quyền người dùng.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Không cần NumberValidationTextBox hay DecimalValidationTextBox cho người dùng nếu chỉ nhập chuỗi
    }
}