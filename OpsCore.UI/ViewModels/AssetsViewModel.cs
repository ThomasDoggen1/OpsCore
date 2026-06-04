using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OpsCore.UI.ViewModels
{
    public partial class AssetsViewModel : ObservableObject
    {
        private readonly IAssetRepository _assetRepository;

        [ObservableProperty]
        private ObservableCollection<Asset> assets = new();

        [ObservableProperty]
        private Asset? selectedAsset;

        [ObservableProperty]
        private bool isLoading;


        public AssetsViewModel(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        [RelayCommand]
        public async Task LoadAssetsAsync()
        {
            isLoading = true;
            var result = await _assetRepository.GetAllAsync();
            Assets = new ObservableCollection<Asset>(result);
            IsLoading = false;
        }

        [RelayCommand]
        public async Task DeleteAssetAsync(Asset asset)
        {
            await _assetRepository.DeleteAsync(asset.Id);
            assets.Remove(asset);
        }

    }


}
