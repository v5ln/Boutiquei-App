using System;
using System.Collections.Generic;
// using Boutiquei.ViewModels;
using Boutiquei.Views;
using Xamarin.Forms;

namespace Boutiquei
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SingleBoutiquePage),
                typeof(SingleBoutiquePage));

        }

    }
}
    