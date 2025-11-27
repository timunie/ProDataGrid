using System;
using System.Collections.ObjectModel;
using DataGridSample.Models;
using DataGridSample.Mvvm;

namespace DataGridSample.ViewModels
{
    public class PixelColumnsViewModel : ObservableObject
    {
        public ObservableCollection<PixelItem> Items { get; } = new();

        public PixelColumnsViewModel()
        {
            Populate();
        }

        private void Populate()
        {
            Items.Clear();
            var random = new Random(7);
            for (int i = 1; i <= 150; i++)
            {
                Items.Add(PixelItem.Create(i, random));
            }
        }
    }
}
