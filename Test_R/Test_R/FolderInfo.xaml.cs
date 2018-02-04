using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test_R
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FolderInfo : ContentPage
	{
        //initilaze objects
        JsonDeserialize jsonDeserialize1;
        public ObservableCollection<Folder> secondFolderInfo { get; set; }
        Root secondFolder;
        public FolderInfo (string _pathFolder)
		{
			InitializeComponent ();
            jsonDeserialize1 = new JsonDeserialize();
            secondFolderInfo = new ObservableCollection<Folder>();
            //setup settings listView
            ListView secondListView = new ListView();
            secondListView.RowHeight = 60;
            secondListView.ItemTemplate = new DataTemplate(typeof(Folder));
            //call ShowFiles method
            ShowFiles(_pathFolder);
        }


        private void ShowFiles(string _path)
        {
            string pathFolder;
            string[] split;
            //geting and answer from server and write in the row 
            string answer = jsonDeserialize1.Deserialize(_path).ToString();
            // parse answer
            JObject jobject = JObject.Parse(answer);
            // geting parametrs from content folder
            var arrFolder = jobject.SelectToken("content");
            // geting list folders
            var listFolder = arrFolder.ToList();
            secondFolder = JsonConvert.DeserializeObject<Root>(answer);
            //get folder options
            for (int i = 0; i < listFolder.Count; i++) {
                pathFolder = listFolder[i].Path;
                split = pathFolder.Split('[',']','\'');
                secondFolder.name = split[2];
                //setup parametrs folder for display
                try {
                    secondFolderInfo.Add(new Folder { name = secondFolder.name, birthtime = secondFolder.birthtime });
                }
                catch (Exception) {
                    labelFolder.Text = "Oops! Something went wrong";
                }
                
            }
            secondListView.ItemsSource = secondFolderInfo;
        }
        private void TextCell_Tapped(object sender, EventArgs e)
        {
            TextCell txtCell = (TextCell)sender;
            //transition to the following view
            //in the properties we pass the name and path
            Navigation.PushAsync(new ShowFile(txtCell.Text, secondFolder.path, secondFolder.size, secondFolder.birthtime));
            

        }
    }
}