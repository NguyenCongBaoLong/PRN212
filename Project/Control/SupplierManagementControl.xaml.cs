using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Control
{
    public partial class SupplierManagementControl : UserControl
    {
        private Prn212ProjectContext _context;
        private ObservableCollection<Supplier> _supplierDetails;

        public SupplierManagementControl()
        {
            InitializeComponent();
            _context = new Prn212ProjectContext();
            _supplierDetails = new ObservableCollection<Supplier>();
            LoadSuppliers();
        }

        // Modified LoadSuppliers to accept a search term
        private void LoadSuppliers(string searchTerm = null)
        {
            IQueryable<Supplier> query = _context.Suppliers;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Convert search term to lowercase for case-insensitive comparison
                string lowerSearchTerm = searchTerm.ToLower();

                // Filter by DisplayName, Address, Phone, or Email
                query = query.Where(s =>
                    s.DisplayName.ToLower().Contains(lowerSearchTerm) ||
                    (s.Address != null && s.Address.ToLower().Contains(lowerSearchTerm)) ||
                    (s.Phone != null && s.Phone.ToLower().Contains(lowerSearchTerm)) ||
                    (s.Email != null && s.Email.ToLower().Contains(lowerSearchTerm))
                );
            }

            _supplierDetails = new ObservableCollection<Supplier>(query.ToList());
            dgSupplierDetails.ItemsSource = _supplierDetails;
        }

        private void dgSupplierDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgSupplierDetails.SelectedItem is Supplier selectedSupplierDetail)
            {
                txtDisplayName.Text = selectedSupplierDetail.DisplayName ?? string.Empty;
                txtAddress.Text = selectedSupplierDetail.Address ?? string.Empty;
                txtPhone.Text = selectedSupplierDetail.Phone ?? string.Empty;
                txtEmail.Text = selectedSupplierDetail.Email ?? string.Empty;
                txtMoreInfo.Text = selectedSupplierDetail.MoreInfo ?? string.Empty;
                dpContractDate.SelectedDate = selectedSupplierDetail.ContractDate;
            }
            else
            {
                ClearFields();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            dgSupplierDetails.SelectedItem = null; // Unselect item in DataGrid
            LoadSuppliers(); // Reload all data after clearing search
        }

        private void ClearFields()
        {
            txtDisplayName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtMoreInfo.Clear();
            dpContractDate.SelectedDate = null;
            txtSearch.Clear(); // Clear search text box
            dgSupplierDetails.SelectedItem = null; // Unselect item in DataGrid
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                Supplier newSupplier = new Supplier
                {
                    DisplayName = txtDisplayName.Text,
                    Address = txtAddress.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text,
                    MoreInfo = txtMoreInfo.Text,
                    ContractDate = dpContractDate.SelectedDate ?? DateTime.Now
                };

                _context.Suppliers.Add(newSupplier);
                _context.SaveChanges();

                _supplierDetails.Add(newSupplier); // Add to ObservableCollection to update UI

                MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgSupplierDetails.SelectedItem is Supplier selectedSupplierDetail)
            {
                if (!ValidateInput())
                {
                    return;
                }

                try
                {
                    selectedSupplierDetail.DisplayName = txtDisplayName.Text;
                    selectedSupplierDetail.Address = txtAddress.Text;
                    selectedSupplierDetail.Phone = txtPhone.Text;
                    selectedSupplierDetail.Email = txtEmail.Text;
                    selectedSupplierDetail.MoreInfo = txtMoreInfo.Text;
                    selectedSupplierDetail.ContractDate = dpContractDate.SelectedDate ?? DateTime.Now;

                    _context.SaveChanges();
                    MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadSuppliers(); // Reload all data to update DataGrid
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgSupplierDetails.SelectedItem is Supplier selectedSupplierDetail)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Check if the supplier is used by any object
                        bool isInUse = _context.Objects.Any(o => o.IdSupplier == selectedSupplierDetail.Id);

                        if (isInUse)
                        {
                            MessageBox.Show("Không thể xóa nhà cung cấp này vì nó đang được sử dụng bởi ít nhất một vật tư.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        _context.Suppliers.Remove(selectedSupplierDetail);
                        _context.SaveChanges();

                        _supplierDetails.Remove(selectedSupplierDetail); // Remove from ObservableCollection
                        MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Check for duplicate DisplayName
            int currentItemId = (dgSupplierDetails.SelectedItem as Supplier)?.Id ?? 0;
            if (_context.Suppliers.Any(s => s.DisplayName == txtDisplayName.Text && s.Id != currentItemId))
            {
                MessageBox.Show("Tên nhà cung cấp đã tồn tại. Vui lòng nhập tên khác.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (!Regex.IsMatch(txtPhone.Text, @"^\d{10,11}$")) // Validate 10-11 digits
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập 10 hoặc 11 chữ số.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) // Basic email format validation
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập đúng định dạng email.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Check for duplicate Email
            if (_context.Suppliers.Any(s => s.Email == txtEmail.Text && s.Id != currentItemId))
            {
                MessageBox.Show("Email đã tồn tại. Vui lòng nhập email khác.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Check for duplicate Phone
            if (_context.Suppliers.Any(s => s.Phone == txtPhone.Text && s.Id != currentItemId))
            {
                MessageBox.Show("Số điện thoại đã tồn tại. Vui lòng nhập số điện thoại khác.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!dpContractDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng chọn Ngày hợp tác.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // --- New Search Methods ---

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadSuppliers(txtSearch.Text);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadSuppliers(txtSearch.Text);
            }
        }
    }
}