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
        private IList<Answer> answers = new ObservableCollection<Answer>();
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
            await Navigation.PopModalAsync();
           
        }
        async void Update()
        {

            IEnumerable<Answer> answerCollection = await manager.GetAnswersForRequestAsync(_request);
            foreach (Answer answer in answerCollection)
            {
                if (answers.All(b => b.AnswerId != answer.AnswerId))
                    answers.Add(answer);
            }
           
            
        }
        
}
}