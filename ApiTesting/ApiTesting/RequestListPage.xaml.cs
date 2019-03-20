using System;
using System.Collections.Generic;
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

        public RequestListPage ()
		{
			InitializeComponent ();
		}

        public RequestListPage(RequestManager requestManager, IList<Request> requests, Request item)
        {
            this.requestManager = requestManager;
            this.requests = requests;
            this.item = item;
            BindingContext = item.Answers;
            
            InitializeComponent();
            requestLabel.Text = item.TextToTranslate;
        }
    }
}