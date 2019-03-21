﻿using ApiTesting.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ApiTesting
{
    public partial class MainPage : ContentPage
    {
        readonly IList<Request> requests = new ObservableCollection<Request>();
        readonly RequestManager requestManager = new RequestManager();
        public MainPage()
        {
            BindingContext = requests;
            InitializeComponent();
        }

        async void ToolbarItem_Clicked(object ssender, EventArgs e)
        {
            
            IEnumerable<Request> requestCollection = await requestManager.GetAll();
                
            foreach (Request request in requestCollection)
            {
                if (requests.All(b => b.TextToTranslate != request.TextToTranslate))
                    requests.Add(request);
            }
        }

        async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(
                new RequestListPage(requestManager, requests, (Request)e.Item));
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new CreateNewRequest(requestManager));
        }

        async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            Request request = item.CommandParameter as Request;
            if (request != null)
            {
                await Navigation.PushModalAsync(
               new CreateNewAnswerPage(requestManager,request));

            }
        }
    }
}
