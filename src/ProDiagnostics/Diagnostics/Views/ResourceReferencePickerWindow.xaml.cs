using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Diagnostics.Services;
using Avalonia.Diagnostics.ViewModels;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Avalonia.Diagnostics.Views
{
    partial class ResourceReferencePickerWindow : Window
    {
        public ResourceReferencePickerWindow()
        {
            InitializeComponent();
        }

        internal ResourceReferenceCandidate? SelectedCandidate { get; private set; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnUseStaticResource(object? sender, RoutedEventArgs e)
        {
            CloseSelectedCandidate(DevToolsResourceReferenceKind.Static);
        }

        private void OnUseDynamicResource(object? sender, RoutedEventArgs e)
        {
            CloseSelectedCandidate(DevToolsResourceReferenceKind.Dynamic);
        }

        private void OnCancel(object? sender, RoutedEventArgs e)
        {
            SelectedCandidate = null;
            Close(null);
        }

        private void OnResourceDoubleTapped(object? sender, TappedEventArgs e)
        {
            CloseSelectedCandidate(DevToolsResourceReferenceKind.Static);
        }

        private void CloseSelectedCandidate(DevToolsResourceReferenceKind kind)
        {
            if (DataContext is ResourceReferencePickerViewModel viewModel &&
                viewModel.GetSelectedCandidate(kind) is { } candidate)
            {
                SelectedCandidate = candidate;
                Close(candidate);
            }
        }
    }
}
