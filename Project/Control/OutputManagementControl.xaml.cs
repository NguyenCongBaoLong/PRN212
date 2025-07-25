using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Project.Models; // Đảm bảo namespace này là đúng

namespace Project.Control
{
    public partial class OutputManagementControl : UserControl
    {
        private Prn212ProjectContext _context;
        private ObservableCollection<OutputInfo> _outputDetails;

        public OutputManagementControl()
        {
            InitializeComponent();
            _context = new Prn212ProjectContext();
            _outputDetails = new ObservableCollection<OutputInfo>();
            LoadOutputDetails();
            LoadObjects();
            LoadCustomers();
        }

        private void LoadOutputDetails()
        {
            _outputDetails = new ObservableCollection<OutputInfo>(
                _context.OutputInfos
                    .Include(oi => oi.IdObjectNavigation)      // Bao gồm thông tin Vật tư
                    .Include(oi => oi.IdOutputNavigation)      // Bao gồm thông tin Phiếu xuất
                    .Include(oi => oi.IdCustomerNavigation)    // Bao gồm thông tin Khách hàng
                    .ToList()
            );
            dgOutputDetails.ItemsSource = _outputDetails;
        }

        private void LoadObjects()
        {
            cmbObject.ItemsSource = _context.Objects.ToList();
        }

        private void LoadCustomers()
        {
            cmbCustomer.ItemsSource = _context.Customers.ToList(); // Giả định DbSet Customers trong DbContext
        }

        private void dgOutputDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOutputDetails.SelectedItem is OutputInfo selectedOutputDetail)
            {
                cmbObject.SelectedValue = selectedOutputDetail.IdObject;
                dpDateOutput.SelectedDate = selectedOutputDetail.IdOutputNavigation?.DateOutput;
                txtCount.Text = selectedOutputDetail.Count?.ToString() ?? string.Empty;
                txtOutputPrice.Text = selectedOutputDetail.OutputPrice?.ToString() ?? string.Empty; // Nếu OutputPrice là của OutputInfo
                // Hoặc: txtOutputPrice.Text = selectedOutputDetail.IdObjectNavigation?.PriceOut?.ToString() ?? string.Empty; // Nếu Giá xuất là PriceOut của Object
                cmbCustomer.SelectedValue = selectedOutputDetail.IdCustomer;
                txtStatus.Text = selectedOutputDetail.Status ?? string.Empty;
            }
            else
            {
                ClearFields();
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
                // Tạo một Output mới
                Output newOutput = new Output
                {
                    DateOutput = dpDateOutput.SelectedDate ?? DateTime.Now
                };

                _context.Outputs.Add(newOutput); // Giả định DbSet Outputs trong DbContext
                _context.SaveChanges();

                // Tạo OutputInfo mới
                OutputInfo newDetail = new OutputInfo
                {
                    IdObject = (int)cmbObject.SelectedValue,
                    IdOutput = newOutput.Id,
                    IdCustomer = (int)cmbCustomer.SelectedValue,
                    Count = int.Parse(txtCount.Text),
                    OutputPrice = decimal.TryParse(txtOutputPrice.Text, out decimal op) ? op : (decimal?)null, // Gán OutputPrice từ textbox
                    Status = txtStatus.Text
                };

                _context.OutputInfos.Add(newDetail);
                _context.SaveChanges();

                // Cập nhật lại ObservableCollection để DataGrid hiển thị ngay lập tức
                var addedDetail = _context.OutputInfos
                                        .Include(oi => oi.IdObjectNavigation)
                                        .Include(oi => oi.IdOutputNavigation)
                                        .Include(oi => oi.IdCustomerNavigation)
                                        .FirstOrDefault(oi => oi.Id == newDetail.Id);
                if (addedDetail != null)
                {
                    _outputDetails.Add(addedDetail);
                }

                MessageBox.Show("Thêm phiếu xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm phiếu xuất: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgOutputDetails.SelectedItem is OutputInfo selectedOutputDetail)
            {
                if (!ValidateInput())
                {
                    return;
                }

                try
                {
                    selectedOutputDetail.IdObject = (int)cmbObject.SelectedValue;
                    selectedOutputDetail.Count = int.Parse(txtCount.Text);
                    selectedOutputDetail.OutputPrice = decimal.TryParse(txtOutputPrice.Text, out decimal op) ? op : (decimal?)null; // Cập nhật OutputPrice
                    selectedOutputDetail.IdCustomer = (int)cmbCustomer.SelectedValue;
                    selectedOutputDetail.Status = txtStatus.Text;

                    if (selectedOutputDetail.IdOutputNavigation != null)
                    {
                        selectedOutputDetail.IdOutputNavigation.DateOutput = dpDateOutput.SelectedDate ?? DateTime.Now;
                    }
                    else
                    {
                        Output newOrExistingOutput = _context.Outputs.Find(selectedOutputDetail.IdOutput); // Giả định DbSet Outputs
                        if (newOrExistingOutput == null)
                        {
                            newOrExistingOutput = new Output { DateOutput = dpDateOutput.SelectedDate ?? DateTime.Now };
                            _context.Outputs.Add(newOrExistingOutput);
                            _context.SaveChanges();
                            selectedOutputDetail.IdOutput = newOrExistingOutput.Id;
                            selectedOutputDetail.IdOutputNavigation = newOrExistingOutput;
                        }
                        else
                        {
                            newOrExistingOutput.DateOutput = dpDateOutput.SelectedDate ?? DateTime.Now;
                        }
                    }

                    _context.SaveChanges();
                    MessageBox.Show("Cập nhật phiếu xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadOutputDetails();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật phiếu xuất: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phiếu xuất để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgOutputDetails.SelectedItem is OutputInfo selectedOutputDetail)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu xuất này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.OutputInfos.Remove(selectedOutputDetail);
                        _context.SaveChanges();

                        var relatedOutputCount = _context.OutputInfos.Count(oi => oi.IdOutput == selectedOutputDetail.IdOutput);
                        if (relatedOutputCount == 0 && selectedOutputDetail.IdOutputNavigation != null)
                        {
                            _context.Outputs.Remove(selectedOutputDetail.IdOutputNavigation); // Giả định DbSet Outputs
                            _context.SaveChanges();
                        }

                        _outputDetails.Remove(selectedOutputDetail);
                        MessageBox.Show("Xóa phiếu xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa phiếu xuất: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phiếu xuất để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            dgOutputDetails.SelectedItem = null;
        }

        private void ClearFields()
        {
            cmbObject.SelectedIndex = -1;
            dpDateOutput.SelectedDate = null;
            txtCount.Clear();
            txtOutputPrice.Clear();
            cmbCustomer.SelectedIndex = -1;
            txtStatus.Clear();
        }

        private bool ValidateInput()
        {
            if (cmbObject.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Vật tư.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!dpDateOutput.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng chọn Ngày xuất.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(txtCount.Text, out int count) || count <= 0)
            {
                MessageBox.Show("Số lượng phải là một số nguyên dương.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtOutputPrice.Text) && (!decimal.TryParse(txtOutputPrice.Text, out decimal outputPrice) || outputPrice < 0))
            {
                MessageBox.Show("Giá xuất phải là một số dương hoặc 0 nếu có nhập.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStatus.Text))
            {
                MessageBox.Show("Trạng thái xuất không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);

            TextBox textBox = sender as TextBox;
            if (e.Text == "." && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}