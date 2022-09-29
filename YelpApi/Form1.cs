using YelpApi.Models;
using YelpApi.Services;

namespace YelpApi
{
    public partial class Form1 : Form
    {
        #region Private Methods

        private void DisplayResults(ResponseData responseData)
        {
            lvwResults.BeginUpdate();
            lvwResults.Items.Clear();

            foreach(var business in responseData.businesses)
            {
                ListViewItem item = new ListViewItem(business.name);
                item.SubItems.Add(business.location.address1.Trim());
                item.SubItems.Add(business.location.address2);
                item.SubItems.Add(business.location.city.Trim());
                item.SubItems.Add(business.location.state.Trim());
                item.SubItems.Add(business.image_url);

                if(lvwResults.Items.Count % 2==0)
                {
                    item.BackColor = Color.LightYellow;
                }
                lvwResults.Items.Add(item);
            }
            foreach (ColumnHeader column in lvwResults.Columns)
            {
                column.Width = -2;
            }
            lvwResults.Columns[lvwResults.Columns.Count - 1].Width = 0;

            lvwResults.EndUpdate();
        }

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "...";
            Cursor = Cursors.WaitCursor;
            YelpInformation yelpInformation = new YelpInformation();
            RequestData requestData=new RequestData { term=txtSearchFor.Text,location=txtLocation.Text};
            ResponseData responseData = await yelpInformation.GetYelpInformation(requestData);

            DisplayResults(responseData);

            toolStripStatusLabel1.Text = "API Processing Complete";
            Cursor=Cursors.Default;
        }

        private void lvwResults_Click(object sender, EventArgs e)
        {
            if(lvwResults.SelectedItems[0].SubItems[5].Text.Length > 0)
            {
                pbBusinessImage.Load(lvwResults.SelectedItems[0].SubItems[5].Text);
            }
            else
            {
                pbBusinessImage.Image = null;
            }

        }
    }
}