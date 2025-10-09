// 代码生成时间: 2025-10-09 17:35:27
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;

namespace VirtualScrollingDemo
{
    // Define a model for the list items
    public class ListItem
    {
        public string Text { get; set; }
    }

    // Create a view model for the list
    public class ListViewModel
    {
        public ObservableCollection<ListItem> Items { get; set; }
        public ListViewModel()
        {
            // Initialize the list with a large number of items
            Items = new ObservableCollection<ListItem>();
            for (int i = 0; i < 10000; i++)
            {
                Items.Add(new ListItem() { Text = $"Item {i}" });
            }
        }
    }

    public class VirtualScrollView : ScrollView
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<ListItem>), typeof(VirtualScrollView), null);

        public IEnumerable<ListItem> ItemsSource
        {
            get => (IEnumerable<ListItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public VirtualScrollView()
        {
            // Initialize the view with the necessary items source
            this.ContentSize = new Size(1, 10000); // Set the content size to the height of the list
        }
    }

    public class MainPage : ContentPage
    {
        private VirtualScrollView virtualScrollView;
        private ListViewModel viewModel;

        public MainPage()
        {
            // Initialize the view model and the virtual scroll view
            viewModel = new ListViewModel();
            virtualScrollView = new VirtualScrollView
            {
                ItemsSource = viewModel.Items,
                Orientation = ScrollOrientation.Vertical
            };
            virtualScrollView.ContentSize = new Size(1, 10000); // Set the content size to the height of the list

            // Bind the view model to the view
            BindingContext = viewModel;

            // Create the layout and add the virtual scroll view
            Content = new StackLayout
            {
                Children =
                {
                    virtualScrollView
                }
            };
        }
    }
}
