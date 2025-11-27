using System;
using System.Collections.ObjectModel;
using DataGridSample.Models;
using DataGridSample.Mvvm;

namespace DataGridSample.ViewModels
{
    public class FrozenColumnsViewModel : ObservableObject
    {
        private int _frozenColumnCount = 2;

        public FrozenColumnsViewModel()
        {
            Populate();
        }

        public ObservableCollection<PixelItem> Items { get; } = new();

        public int FrozenColumnCount
        {
            get => _frozenColumnCount;
            set => SetProperty(ref _frozenColumnCount, value);
        }

        private void Populate()
        {
            Items.Clear();
            var random = new Random(17);
            for (int i = 1; i <= 200; i++)
            {
                Items.Add(PixelItem.Create(i, random));
            }
        }
    }
}
