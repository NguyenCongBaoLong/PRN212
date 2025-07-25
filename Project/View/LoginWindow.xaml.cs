using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Models; // Đảm bảo namespace này chứa các Models như User
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls; // Đảm bảo có để sử dụng PasswordBox

namespace Project.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IConfiguration _configuration;

        public LoginWindow()
        {
            InitializeComponent();

            // Khởi tạo Configuration để đọc appsettings.json
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Prn212ProjectContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

            using (var context = new Prn212ProjectContext(optionsBuilder.Options))
            {
                string userName = txtUser.Text.Trim(); 
                string password = txtPass.Password;

                var adminUserName = _configuration["AdminAccount:Email"];
                var adminPassword = _configuration["AdminAccount:Password"];

                var managerUserName = _configuration["ManagerAccount:Email"];
                var managerPassword = _configuration["ManagerAccount:Password"];

                var userFromDb = context.Users
                                        .Include(u => u.IdRoleNavigation) // Đảm bảo UserRole được tải
                                        .FirstOrDefault(u => u.UserName == userName && u.Password == password);

                if (userName == adminUserName && password == adminPassword)
                {
                    // Đăng nhập với quyền admin
                    var adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Close();
                }
                else if (userName == managerUserName && password == managerPassword)
                {
                    // Đăng nhập với quyền manager
                    var managerWindow = new ManagerWindow();
                    managerWindow.Show();
                    this.Close();
                }
                else if (userFromDb != null)
                {
                    // Xác định quyền dựa trên IdRole từ database
                    if (userFromDb.IdRole == 1) // IdRole 1 là Admin
                    {
                        var adminWindow = new AdminWindow();
                        adminWindow.Show();
                        this.Close();
                    }
                    else if (userFromDb.IdRole == 3) // IdRole 3 là Manager
                    {
                        var managerWindow = new ManagerWindow();
                        managerWindow.Show();
                        this.Close();
                    }

                }
                else
                {

                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Đăng nhập thất bại",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}