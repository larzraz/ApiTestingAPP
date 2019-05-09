﻿using System;
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
			InitializeComponent ();
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
            requestManager.UpdateIsPreferredValueForAnswer(Id,item);

            
        }

        private void CloseRequestLabel_Clicked(object sender, EventArgs e)
        {
            requestManager.CloseRequest(item);
        }
        async void DisplayData(object Sender, EventArgs e)
        {
            var buttonClickHandler = (Button)Sender;
            Grid parentGridLayout = (Grid)buttonClickHandler.Parent;            
            Label Translated_label = (Label)parentGridLayout.Children[0];
            Label Recommended_Label = (Label)parentGridLayout.Children[1];
            Button Recommend_Button = (Button)parentGridLayout.Children[2];

            string Translated = Translated_label.Text;
            string Recommend = Recommended_Label.Text;
            await DisplayAlert("Answer Details", "oversættelse: " + Translated, "Anbefal: " + Recommend);

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
    }
}