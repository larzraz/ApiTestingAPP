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
	public partial class CreateNewAnswerPage : ContentPage
	{
        private Request request = new Request();
        private readonly RequestManager manager;
        private Answer answer = new Answer();
        public CreateNewAnswerPage(RequestManager manager, Request request)
		{
            this.request = request;
            this.manager = manager;
			InitializeComponent ();
            BindingContext = request;
		}

        async void SubmitNewAnswerButton_Clicked(object sender, EventArgs e)
        {
            answer.TextTranslated = TranslatedTextEntry.Text;
            answer.RequestId = request.ID;
            await manager.AddAnswerToRequest(answer);
           //request.Answers.Add(answer);
        }
    }
}