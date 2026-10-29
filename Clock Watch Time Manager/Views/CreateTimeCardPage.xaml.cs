using Clock_Watch_Time_Manager.ViewModels;
using System.Diagnostics;
namespace Clock_Watch_Time_Manager.Views;

public partial class CreateTimeCardPage : ContentPage
{
    public CreateTimeCardPage()
    {
        InitializeComponent();

#if DEBUG
        Debug.WriteLine("Called CreateTimeCardPage()");
#endif
    }
}

