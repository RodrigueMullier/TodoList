using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TodoList.Models;

namespace TodoList.Views.UserControls
{
    /// <summary>
    /// Logique d'interaction pour FilterUserControl.xaml
    /// </summary>
    public partial class CategoryUserControl : UserControl
    {
        public static readonly DependencyProperty CategoriesProperty = 
            DependencyProperty.Register(nameof(Categories), typeof(ObservableCollection<CategoryItem>), typeof(CategoryUserControl), 
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public ObservableCollection<CategoryItem> Categories { get; set; }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CategoryUserControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public ICommand Command { get; set; }
        public CategoryUserControl()
        {
            InitializeComponent();
        }
    }
}
