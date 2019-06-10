using ApiTesting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using System.IO;


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

        private async void AddPhotoButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                // 1. Add camera logic.
                await CrossMedia.Current.Initialize();

                MediaFile photo;
                //if (CrossMedia.Current.IsCameraAvailable)
                //{
                //    photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                //    {
                //        Directory = "OrCeIT",
                //        Name = "Orce.jpg"
                //    });
                //}
                //else
                //{
                photo = await CrossMedia.Current.PickPhotoAsync();
                //}
                // 2. Add  OCR logic.

                OcrResult text;


                using (var client = new ComputerVisionClient(Credentials) { Endpoint = "https://westcentralus.api.cognitive.microsoft.com" })
                {
                    using (photoStream = photo.GetStream())
                    {
                        text = await client.RecognizePrintedTextInStreamAsync(!DetectOrientation, photoStream, OcrLanguages.En);

                    }
                }


                //await UploadAndRecognizeImageAsync(photoStream.ToString() , OcrLanguages.En);
                // 3. Add to data-bound collection.

                textToTranslateEntry.Text = OcrStringBuilder(text);                             // Add to data-bound collection.

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
                await DisplayAlert("Error", ex.Message, "OK");
            }

        }
        public bool DetectOrientation { get; private set; }
        public Stream photoStream;

        protected ApiKeyServiceClientCredentials Credentials
        {
            get
            {
                return new ApiKeyServiceClientCredentials("76a03512e78b4349bab47da45e57c0df");
            }
        }
        

        private string OcrStringBuilder(OcrResult results)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (results != null && results.Regions != null)
            {
                stringBuilder.Append("");
                stringBuilder.AppendLine();
                foreach (var item in results.Regions)
                {
                    foreach (var line in item.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            stringBuilder.Append(word.Text);
                            stringBuilder.Append(" ");
                        }

                        stringBuilder.AppendLine();
                    }

                    stringBuilder.AppendLine();
                }
            }
            return stringBuilder.ToString();
        }

        private async Task<OcrResult> UploadAndRecognizeImageAsync(string imageFilePath, OcrLanguages language)
        {
           
            //
            // Create Cognitive Services Vision API Service client.
            //
            using (var client = new ComputerVisionClient(Credentials) { Endpoint = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0" })
            {


                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    //
                    // Upload an image and perform OCR.
                    //

                    OcrResult ocrResult = await client.RecognizePrintedTextInStreamAsync(!DetectOrientation, imageFileStream, language);
                    return ocrResult;
                }
            }

           }
    }
}