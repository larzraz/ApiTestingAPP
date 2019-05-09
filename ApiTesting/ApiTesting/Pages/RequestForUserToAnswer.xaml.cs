using ApiTesting.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApiTesting.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RequestForUserToAnswer : ContentPage
	{
        readonly IList<Request> requests = new ObservableCollection<Request>();
        readonly RequestManager requestManager = new RequestManager();
        public RequestForUserToAnswer ()
		{
            BindingContext = requests;
            Update();
            InitializeComponent();
            
        }

        private async void Update()
        {
            IEnumerable<Request> requestCollection = await requestManager.GetAll();

            //foreach (Request request in requestCollection)
            //{
            //    if (requests.All(b => b.TextToTranslate != request.TextToTranslate))
            //        requests.Add(request);
            //}
            foreach (Request request in requestCollection)
            {
                if (requests.All(b => b.TextToTranslate != request.TextToTranslate))
                    if (request.IsClosed != false)
                    { requests.Add(request); }

            }
        }


        async void ToolbarItem_Clicked(object ssender, EventArgs e)
        {

            IEnumerable<Request> requestCollection = await requestManager.GetAll();

            foreach (Request request in requestCollection)
            {
                if (requests.All(b => b.TextToTranslate != request.TextToTranslate))
                    requests.Add(request);
            }
            //requestCollection.OrderByDescending<Request, requestID>();
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
               new CreateNewAnswerPage(requestManager, request));

            }
        }
    }
}