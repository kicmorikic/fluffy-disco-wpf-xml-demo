using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpfApp.Repository;
using wpfApp.ViewModel;

namespace wpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PeopleListVM _peopleList;
        public ObservableCollection<PersonVM> PersonVM
        {
            get { return _peopleList.peopleVMColection; }
            
        }
        public MainWindow(IPeopleRepository repository)
        {

            InitializeComponent();
            _peopleList = new PeopleListVM(repository ,this );

            
        }
        

        

        
    }
}
