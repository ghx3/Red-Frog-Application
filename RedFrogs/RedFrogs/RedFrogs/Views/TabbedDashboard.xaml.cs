using RedFrogs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedFrogs.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /**
    * Class used to create a tabbed page that contains TeamLeaderDashPage and DashboardPage 
    * **/
    public partial class TabbedDashboard : TabbedPage
    {
        public TabbedDashboard(Events e)
        {
            InitializeComponent();

            Children.Add(new TeamLeaderDashPage(e));
            Children.Add(new DashboardPage(e));
        }
    }
}