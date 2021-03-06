﻿using LewisFam.Desktop.Core.Config;
using LewisFam.Desktop.Core.Contracts.Services;
using LewisFam.Desktop.Core.Models;

using System;
using System.Collections;
using System.IO;
using System.Windows;

namespace LewisFam.Desktop.Core.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private readonly IFileService _fileService;
        private readonly AppConfig _appConfig;
        private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public PersistAndRestoreService(IFileService fileService, AppConfig appConfig)
        {
            _fileService = fileService;
            _appConfig = appConfig;
        }

        public void PersistData()
        {
            if (Application.Current.Properties != null)
            {
                var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
                var fileName = _appConfig.AppPropertiesFileName;
                _fileService.Save(folderPath, fileName, Application.Current.Properties);
            }
        }

        public void RestoreData()
        {
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.AppPropertiesFileName;
            var properties = _fileService.Read<IDictionary>(folderPath, fileName);
            if (properties != null)
            {
                foreach (DictionaryEntry property in properties)
                {
                    Application.Current.Properties.Add(property.Key, property.Value);
                }
            }
        }
    }
}
