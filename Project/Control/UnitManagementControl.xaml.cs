using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions; // Mặc dù không dùng regex cho Unit, giữ lại để đồng bộ
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; // Mặc dù không dùng Input cho Unit, giữ lại để đồng bộ
using Microsoft.EntityFrameworkCore;
using Project.Models; // Đảm bảo namespace này là đúng

namespace Project.Control
{
    public partial class UnitManagementControl : UserControl
    {
        private Prn212ProjectContext _context;
        private ObservableCollection<Unit> _unitDetails;

        public UnitManagementControl()
        {
            InitializeComponent();
            InitializeDbContextAndLoadData();
        }

        private void InitializeDbContextAndLoadData()
        {
            _context?.Dispose();
            _context = new Prn212ProjectContext();
            _unitDetails = new ObservableCollection<Unit>();
            LoadUnits(); 
            ClearFields(); 
            dgUnitDetails.SelectedItem = null; 
        }

        // <<<< SỬA PHƯƠNG THỨC LoadUnits ĐỂ CÓ THAM SỐ TÌM KIẾM >>>>
        private void LoadUnits(string searchTerm = null)
        {
            IQueryable<Unit> query = _context.Units;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                // Tìm kiếm theo DisplayName hoặc Description
                query = query.Where(u => u.DisplayName.Contains(searchTerm) || u.Description.Contains(searchTerm));
            }

            _unitDetails = new ObservableCollection<Unit>(query.ToList());
            dgUnitDetails.ItemsSource = _unitDetails;
        }

        private void dgUnitDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUnitDetails.SelectedItem is Unit selectedUnitDetail)
            {
                txtDisplayName.Text = selectedUnitDetail.DisplayName ?? string.Empty;
                txtDescription.Text = selectedUnitDetail.Description ?? string.Empty;
            }
            else
            {
                ClearFields();
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadUnits(txtSearch.Text);
        }

        // <<<< PHƯƠNG THỨC TÌM KIẾM KHI NHẤN ENTER TRONG Ô TÌM KIẾM >>>>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadUnits(txtSearch.Text);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
            dgUnitDetails.SelectedItem = null; // Bỏ chọn trên DataGrid
        }
        private void ClearFields()
        {
            txtDisplayName.Clear();
            txtDescription.Clear();
            dgUnitDetails.SelectedItem = null; // Bỏ chọn trên DataGrid
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                Unit newUnit = new Unit
                {
                    DisplayName = txtDisplayName.Text,
                    Description = txtDescription.Text
                };

                _context.Units.Add(newUnit);
                _context.SaveChanges();

                _unitDetails.Add(newUnit); // Thêm vào ObservableCollection để UI cập nhật
                
                MessageBox.Show("Thêm đơn vị đo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm đơn vị đo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgUnitDetails.SelectedItem is Unit selectedUnitDetail)
            {
                if (!ValidateInput())
                {
                    return;
                }

                try
                {
                    selectedUnitDetail.DisplayName = txtDisplayName.Text;
                    selectedUnitDetail.Description = txtDescription.Text;

                    _context.SaveChanges();
                    MessageBox.Show("Cập nhật đơn vị đo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUnits(); // Tải lại toàn bộ dữ liệu để cập nhật DataGrid
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật đơn vị đo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn vị đo để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUnitDetails.SelectedItem is Unit selectedUnitDetail)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa đơn vị đo này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Kiểm tra xem đơn vị đo có đang được sử dụng bởi bất kỳ vật tư nào không
                        bool isInUse = _context.Objects.Any(o => o.IdUnit == selectedUnitDetail.Id);

                        if (isInUse)
                        {
                            MessageBox.Show("Không thể xóa đơn vị đo này vì nó đang được sử dụng bởi ít nhất một vật tư.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        _context.Units.Remove(selectedUnitDetail);
                        _context.SaveChanges();

                        _unitDetails.Remove(selectedUnitDetail); // Xóa khỏi ObservableCollection
                        MessageBox.Show("Xóa đơn vị đo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa đơn vị đo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn vị đo để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtDisplayName.Text))
            {
                MessageBox.Show("Tên đơn vị đo không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Kiểm tra trùng lặp DisplayName (nếu cần)
            // Lấy ID của item đang chọn nếu đang ở chế độ sửa
            int currentItemId = (dgUnitDetails.SelectedItem as Unit)?.Id ?? 0; 
            if (_context.Units.Any(u => u.DisplayName == txtDisplayName.Text && u.Id != currentItemId))
            {
                MessageBox.Show("Tên đơn vị đo đã tồn tại. Vui lòng nhập tên khác.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Không cần NumberValidationTextBox hay DecimalValidationTextBox vì DisplayName và Description là string
    }
}