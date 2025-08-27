using System.Windows.Controls;
using simulation_app.Services;
using simulation_app.ViewModels;

namespace simulation_app.Views
{
    public partial class ManualInputsView : UserControl
    {
        public ManualInputsView()
        {
            InitializeComponent();
            // 간단 DI: Mock 주입
            this.DataContext = new ManualInputsViewModel(new MockInputLoader());
        }
    }
}
