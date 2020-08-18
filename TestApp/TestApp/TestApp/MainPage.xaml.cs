using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnGet_Clicked(object sender, EventArgs e)
        {
            lblResult.Text = string.Empty;
            lblResult.TextColor = Color.Black;
            WebHelper webHelper = new WebHelper();
            try
            {
                ProtobufModelDto protobufModelDto = await webHelper.GetProtoBufData();
                if (!string.IsNullOrEmpty(protobufModelDto.Name))
                {
                    lblResult.Text = "Result = " + " Id : " + protobufModelDto.Id + ", Name : " + protobufModelDto.Name + ", StringValue : " + protobufModelDto.StringValue;
                }
                else
                {
                    lblResult.TextColor = Color.Red;
                    lblResult.Text = "Sorry, something went wrong";
                }
            }
            catch (Exception ex)
            {
                lblResult.TextColor = Color.Red;
                lblResult.Text = ex.Message;
            }

        }

        private async void BtnPost_Clicked(object sender, EventArgs e)
        {
            lblResult.Text = string.Empty;
            lblResult.TextColor = Color.Black;
            WebHelper webHelper = new WebHelper();
            try
            {
                ProtobufModelDto protobufModelDto = await webHelper.PostProtoBufData();
                if (!string.IsNullOrEmpty(protobufModelDto.Name))
                {
                    lblResult.Text = "Result = " + " Id : " + protobufModelDto.Id + ", Name : " + protobufModelDto.Name + ", StringValue : " + protobufModelDto.StringValue;
                }
                else
                {
                    lblResult.TextColor = Color.Red;
                    lblResult.Text = "Sorry, something went wrong";
                }
            }
            catch (Exception ex)
            {
                lblResult.TextColor = Color.Red;
                lblResult.Text = ex.Message;
            }

        }
    }
}
