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
    public partial class InputManagementControl : UserControl
    {
        private Prn212ProjectContext _context;
        private ObservableCollection<InputInfo> _inputDetails;

        public InputManagementControl()
        {
            InitializeComponent();
            _context = new Prn212ProjectContext();
            _inputDetails = new ObservableCollection<InputInfo>();
            LoadInputDetails();
            LoadObjects();
        }

        private void LoadInputDetails()
        {
            _inputDetails = new ObservableCollection<InputInfo>(
                _context.InputInfos
                    .Include(ii => ii.IdObjectNavigation)
                    .Include(ii => ii.IdInputNavigation)
                    .ToList()
            );
            dgInputDetails.ItemsSource = _inputDetails;
        }


        private void LoadObjects()
        {
            cmbObject.ItemsSource = _context.Objects.ToList();
        }

        private void dgInputDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgInputDetails.SelectedItem is InputInfo selectedInputDetail)
            {
                cmbObject.SelectedValue = selectedInputDetail.IdObject;
                dpDateInput.SelectedDate = selectedInputDetail.IdInputNavigation?.DateInput;
                txtCount.Text = selectedInputDetail.Count.ToString();
                txtInputPrice.Text = selectedInputDetail.InputPrice.ToString();
                txtStatus.Text = selectedInputDetail.Status;
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
                Input newInput = new Input
                {
                    DateInput = dpDateInput.SelectedDate ?? DateTime.Now
                };

                _context.Inputs.Add(newInput);
                _context.SaveChanges();

                InputInfo newDetail = new InputInfo
                {
                    IdObject = (int)cmbObject.SelectedValue,
                    IdInput = newInput.Id,
                    Count = int.Parse(txtCount.Text),
                    InputPrice = decimal.Parse(txtInputPrice.Text),
                    Status = txtStatus.Text
                };

                _context.InputInfos.Add(newDetail);
                _context.SaveChanges();

                var addedDetail = _context.InputInfos
                                        .Include(ii => ii.IdObjectNavigation)
                                        .Include(ii => ii.IdInputNavigation)
                                        .FirstOrDefault(ii => ii.Id == newDetail.Id);
                if (addedDetail != null)
                {
                    _inputDetails.Add(addedDetail);
                }

                MessageBox.Show("Thêm phiếu nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm phiếu nhập: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgInputDetails.SelectedItem is InputInfo selectedInputDetail)
            {
                if (!ValidateInput())
                {
                    return;
                }

                try
                {
                    selectedInputDetail.IdObject = (int)cmbObject.SelectedValue;
                    selectedInputDetail.Count = int.Parse(txtCount.Text);
                    selectedInputDetail.InputPrice = decimal.Parse(txtInputPrice.Text);
                    selectedInputDetail.Status = txtStatus.Text;

                    if (selectedInputDetail.IdInputNavigation != null)
                    {
                        selectedInputDetail.IdInputNavigation.DateInput = dpDateInput.SelectedDate ?? DateTime.Now;
                    }
                    else
                    {
                        Input newOrExistingInput = _context.Inputs.Find(selectedInputDetail.IdInput);
                        if (newOrExistingInput == null)
                        {
                            newOrExistingInput = new Input { DateInput = dpDateInput.SelectedDate ?? DateTime.Now };
                            _context.Inputs.Add(newOrExistingInput);
                            _context.SaveChanges();
                            selectedInputDetail.IdInput = newOrExistingInput.Id;
                            selectedInputDetail.IdInputNavigation = newOrExistingInput;
                        }
                        else
                        {
                            newOrExistingInput.DateInput = dpDateInput.SelectedDate ?? DateTime.Now;
                        }
                    }

                    _context.SaveChanges();
                    MessageBox.Show("Cập nhật phiếu nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadInputDetails();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật phiếu nhập: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phiếu nhập để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgInputDetails.SelectedItem is InputInfo selectedInputDetail)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.InputInfos.Remove(selectedInputDetail);
                        _context.SaveChanges();

                        var relatedInputCount = _context.InputInfos.Count(ii => ii.IdInput == selectedInputDetail.IdInput);
                        if (relatedInputCount == 0 && selectedInputDetail.IdInputNavigation != null)
                        {
                            _context.Inputs.Remove(selectedInputDetail.IdInputNavigation);
                            _context.SaveChanges();
                        }

                        _inputDetails.Remove(selectedInputDetail);
                        MessageBox.Show("Xóa phiếu nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa phiếu nhập: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phiếu nhập để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            dgInputDetails.SelectedItem = null;
        }

        private void ClearFields()
        {
            cmbObject.SelectedIndex = -1;
            dpDateInput.SelectedDate = null;
            txtCount.Clear();
            txtInputPrice.Clear();
            txtStatus.Clear();
        }

        private bool ValidateInput()
        {
            if (cmbObject.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Vật tư.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!dpDateInput.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng chọn Ngày nhập.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(txtCount.Text, out int count) || count <= 0)
            {
                MessageBox.Show("Số lượng phải là một số nguyên dương.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(txtInputPrice.Text, out decimal inputPrice) || inputPrice <= 0)
            {
                MessageBox.Show("Giá nhập phải là một số dương.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtStatus.Text))
            {
                MessageBox.Show("Trạng thái nhập không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
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