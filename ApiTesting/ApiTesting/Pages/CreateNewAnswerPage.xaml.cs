using ApiTesting.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Request _request = new Request();
        private readonly RequestManager manager;
        private Answer answer = new Answer();
        readonly IList<Request> requests = new ObservableCollection<Request>();
        public CreateNewAnswerPage(RequestManager manager, Request request)
		{
            this._request = request;
            this.manager = manager;
			InitializeComponent ();
            BindingContext = request;
		}

        async void SubmitNewAnswerButton_Clicked(object sender, EventArgs e)
        {
            answer.TextTranslated = TranslatedTextEntry.Text;
            answer.RequestId = _request.RequestId;
            await manager.AddAnswerToRequest(answer, _request);
            Update();
           
        }
        async void Update()
        {
            IEnumerable<Request> requestCollection = await manager.GetAll();

            foreach (Request request in requestCollection)
            {
                if(_request.Answers.All(b => b.ID != answer.ID))
                {
                    _request.Answers.Add(answer);
                }
               
            }
        }
        
}
}