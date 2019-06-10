using ApiTesting.Data.UserService;
using ApiTesting.Pages.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApiTesting.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private User user = new User();
        private readonly UserManager manager;
        public LoginPage(UserManager manager)
        {
            this.manager = manager;
            InitializeComponent();
            BindingContext = user;
        }

        private async void LoginUser(object sender, EventArgs e)
        {
            await manager.Login(user);
        }
        private async void RegisterUser(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new RegisterUserPage(manager));
        }

    }
}