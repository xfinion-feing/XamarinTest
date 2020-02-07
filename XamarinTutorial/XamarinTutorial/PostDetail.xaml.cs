using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTutorial.Model;

namespace XamarinTutorial
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostDetail : ContentPage
	{
        Post selectedPost;
		public PostDetail (Post selectedPost)
		{
			InitializeComponent ();
            this.selectedPost = selectedPost;
            experienceEntry.Text = selectedPost.Experience;
		}

        private void UpdateButton_Clicked(object sender, EventArgs e)
        {
            selectedPost.Experience = experienceEntry.Text;

            using (var sqlConn = new SQLiteConnection(App.DatabaseLocation))
            {
                sqlConn.CreateTable<Post>();
                var rows = sqlConn.Update(selectedPost);

                if (rows > 0)
                    DisplayAlert("Success", "Experience successfully updated", "OK");
                else
                    DisplayAlert("Error", "Experience failed to be updated", "OK");
            }
               
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            using (var sqlConn = new SQLiteConnection(App.DatabaseLocation))
            {
                sqlConn.CreateTable<Post>();
                var rows = sqlConn.Delete(selectedPost);

                if (rows > 0)
                    DisplayAlert("Success", "Experience successfully deleted", "OK");
                else
                    DisplayAlert("Error", "Experience failed to be deleted", "OK");
            }
        }
    }
}