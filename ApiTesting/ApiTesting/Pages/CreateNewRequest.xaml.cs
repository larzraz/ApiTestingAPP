using ApiTesting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApiTesting
{
    
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateNewRequest : ContentPage
	{
        private Request request = new Request();
        private readonly RequestManager manager;
        public CreateNewRequest (RequestManager manager)
		{
            this.manager = manager;
            InitializeComponent ();
            BindingContext = request; 

        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            request.LanguageOrigin = LanguageOriginEntry.Text;
            request.LanguageTarget = LanguageTargetEntry.Text;
            request.TextToTranslate = textToTranslateEntry.Text;
            await manager.CreateNewRequest(request);
            //await this.Navigation.PopAsync();
        }
    }
}