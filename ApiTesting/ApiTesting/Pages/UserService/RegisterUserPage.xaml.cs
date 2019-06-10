using ApiTesting.Data.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApiTesting.Pages.UserService
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterUserPage : ContentPage
	{
        private User user = new User();
        private readonly UserManager manager;
            public RegisterUserPage (UserManager manager)
		{
                this.manager = manager;
                InitializeComponent();
                BindingContext = user;
            }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await manager.Register(user);
        }
    }
}