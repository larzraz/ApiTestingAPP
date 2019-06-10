using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiTesting.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApiTesting
{

	public partial class RequestListPage : ContentPage
	{
        private RequestManager requestManager;
        private IList<Request> requests;
        private Request item;
        private IList<Answer> answers = new ObservableCollection<Answer>();
        private Answer answer = new Answer();


        public RequestListPage ()
		{
            try
            {
                InitializeComponent();
            }
            catch(Exception e)
            {
                DisplayAlert("alert", e.ToString(), "ok");
            }
		}

         public RequestListPage(RequestManager requestManager, IList<Request> requests, Request item)
        {
            this.requestManager = requestManager;
            this.requests = requests;
            this.item = item;         
            Update();                   
            BindingContext = answers;         
            InitializeComponent();
            requestLabel.Text = item.TextToTranslate;
            Update();
        }

        async void Update()
        {
            IEnumerable<Answer> answerCollection = await requestManager.GetAnswersForRequestAsync(item as Request);
            foreach (Answer answer in answerCollection)
            {
                if (answers.All(b => b.TextTranslated != answer.TextTranslated))
                    answers.Add(answer);
            }

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            IEnumerable<Answer> answerCollection = await requestManager.GetAnswersForRequestAsync(item as Request);

            foreach (Answer answer in answerCollection)
            {
                if (answers.All(b => b.TextTranslated != answer.TextTranslated))
                    answers.Add(answer);
            }

        }

        private void Prefered_Button_Clicked(object sender, EventArgs e)
        {
            var buttonClickHandler = (Button)sender;
            Grid parentGridLayout = (Grid)buttonClickHandler.Parent;
            Label Translated_label = (Label)parentGridLayout.Children[0];
            Label Prefered_Label = (Label)parentGridLayout.Children[1];
            Button Prefered_Button = (Button)parentGridLayout.Children[2];

            string Translated = Translated_label.Text;
            string Prefered = Prefered_Label.Text;
            int Id = int.Parse(Prefered_Button.Text); 
            //requestManager.UpdateIsPreferredValueForAnswer();

            
        }

        private async void CloseRequestLabel_Clicked(object sender, EventArgs e)
        {
              bool close = await DisplayAlert("Er du sikker på du vil lukke den?", "", "ja", "nej");
            if (close == true)
            {
                requestManager.CloseRequest(item);
                await Navigation.PopModalAsync();
                }

        }

        private async void CreateNewAnswerBtn_Clicked(object sender, EventArgs e)
        {
           
            
            if (item != null)
            {
                await Navigation.PushModalAsync(
               new CreateNewAnswerPage(requestManager, item));
            }

        }

        private async void GoBackBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();

        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bool answer = await DisplayAlert("", "Vil du gerne vælge denne som favorit?", "Ja", "nej");

            if (answer == true)
            {
                requestManager.UpdateIsPreferredValueForAnswer((Answer)e.Item);
            }
        }
    }
}