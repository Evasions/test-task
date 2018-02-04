using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowFile : ContentPage
    {
        public ShowFile(string _name, string _path, int _size, DateTime _dateCreate)
        {
            InitializeComponent();
            ShowFileContent(_name, _path, _size, _dateCreate);
        }

        private void ShowFileContent(string _name, string _path, int _size, DateTime _dateCreate)
        {
            try {
                labelSize.Text = "Size: " + _size.ToString();
                labelDateCreator.Text = _dateCreate.Date.ToString();
                string[] splitTxt = _name.Split('.', '\'');
                //determine the type of file
                if (String.Equals(splitTxt[splitTxt.Length - 1], "jpg")) {
                    imageContent.Source = ImageSource.FromUri(new Uri("https://fileserver-tsh.herokuapp.com/" + _path + "/" + _name));
                }
                else { }
            }
            catch (Exception) {

                labelSecond.Text = "Oops!Something went wrong";
            }
           
          
        }
    }
}