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
    public partial class ObjectManagementControl : UserControl
    {
        private Prn212ProjectContext _context;
        private ObservableCollection<Objects> _objectDetails; // ĐÃ ĐỔI TỪ Object THÀNH Objects

        public ObjectManagementControl()
        {
            InitializeComponent();
            _context = new Prn212ProjectContext();
            _objectDetails = new ObservableCollection<Objects>(); // ĐÃ ĐỔI TỪ Object THÀNH Objects
            LoadObjects();
            LoadUnits();
            LoadSuppliers();
        }

        private void LoadObjects()
        {
            _objectDetails = new ObservableCollection<Objects>( // ĐÃ ĐỔI TỪ Object THÀNH Objects
                _context.Objects // DbSet đã đổi tên
                    .Include(o => o.IdUnitNavigation)
                    .Include(o => o.IdSupplierNavigation)
                    .ToList()
            );
            dgObjectDetails.ItemsSource = _objectDetails;
        }
        private void LoadUnits()
        {
            cmbUnit.ItemsSource = _context.Units.ToList();
        }

        private void LoadSuppliers()
        {
            cmbSupplier.ItemsSource = _context.Suppliers.ToList();
        }

        private void dgObjectDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgObjectDetails.SelectedItem is Objects selectedObjectDetail) // ĐÃ ĐỔI TỪ Object THÀNH Objects
            {
                txtDisplayName.Text = selectedObjectDetail.DisplayName ?? string.Empty;
                cmbUnit.SelectedValue = selectedObjectDetail.IdUnit;
                cmbSupplier.SelectedValue = selectedObjectDetail.IdSupplier;
                txtQrCode.Text = selectedObjectDetail.QrCode ?? string.Empty;
                txtBarCode.Text = selectedObjectDetail.BarCode ?? string.Empty;
                txtQuantity.Text = selectedObjectDetail.Quantity?.ToString() ?? string.Empty;
            }
            else
            {
                ClearFields();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            dgObjectDetails.SelectedItem = null; // Bỏ chọn trên DataGrid
        }

        private void ClearFields()
        {
            txtDisplayName.Clear();
            cmbUnit.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;
            txtQrCode.Clear();
            txtBarCode.Clear();
            txtQuantity.Clear();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                Objects newObject = new Objects // ĐÃ ĐỔI TỪ Object THÀNH Objects
                {
                    DisplayName = txtDisplayName.Text,
                    IdUnit = (int)cmbUnit.SelectedValue,
                    IdSupplier = (int)cmbSupplier.SelectedValue,
                    QrCode = txtQrCode.Text,
                    BarCode = txtBarCode.Text,
                    Quantity = int.Parse(txtQuantity.Text),
                };

                _context.Objects.Add(newObject); // DbSet đã đổi tên
                _context.SaveChanges();

                var addedObject = _context.Objects // DbSet đã đổi tên
                                        .Include(o => o.IdUnitNavigation)
                                        .Include(o => o.IdSupplierNavigation)
                                        .FirstOrDefault(o => o.Id == newObject.Id);
                if (addedObject != null)
                {
                    _objectDetails.Add(addedObject);
                }

                MessageBox.Show("Thêm vật tư thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm vật tư: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgObjectDetails.SelectedItem is Objects selectedObjectDetail) // ĐÃ ĐỔI TỪ Object THÀNH Objects
            {
                if (!ValidateInput())
                {
                    return;
                }

                try
                {
                    selectedObjectDetail.DisplayName = txtDisplayName.Text;
                    selectedObjectDetail.IdUnit = (int)cmbUnit.SelectedValue;
                    selectedObjectDetail.IdSupplier = (int)cmbSupplier.SelectedValue;
                    selectedObjectDetail.QrCode = txtQrCode.Text;
                    selectedObjectDetail.BarCode = txtBarCode.Text;
                    selectedObjectDetail.Quantity = int.Parse(txtQuantity.Text);

                    _context.SaveChanges();
                    MessageBox.Show("Cập nhật vật tư thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadObjects();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật vật tư: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một vật tư để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgObjectDetails.SelectedItem is Objects selectedObjectDetail) // ĐÃ ĐỔI TỪ Object THÀNH Objects
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa vật tư này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool isInUse = _context.InputInfos.Any(ii => ii.IdObject == selectedObjectDetail.Id) ||
                                        _context.OutputInfos.Any(oi => oi.IdObject == selectedObjectDetail.Id);

                        if (isInUse)
                        {
                            MessageBox.Show("Không thể xóa vật tư này vì nó đang được sử dụng trong phiếu nhập hoặc phiếu xuất.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        _context.Objects.Remove(selectedObjectDetail); // DbSet đã đổi tên
                        _context.SaveChanges();

                        _objectDetails.Remove(selectedObjectDetail);
                        MessageBox.Show("Xóa vật tư thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa vật tư: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một vật tư để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageBox.Show("Tên vật tư không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cmbUnit.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Đơn vị đo.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (cmbSupplier.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQrCode.Text))
            {
                MessageBox.Show("Mã vật tư (QR Code) không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Số lượng tồn phải là một số nguyên không âm.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            Objects selectedObject = dgObjectDetails.SelectedItem as Objects;

            if (selectedObject == null) // Nếu đang ở chế độ THÊM MỚI
            {
                if (_context.Objects.Any(o => o.QrCode == txtQrCode.Text))
                {
                    MessageBox.Show("Mã vật tư (QR Code) đã tồn tại. Vui lòng nhập mã khác.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
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