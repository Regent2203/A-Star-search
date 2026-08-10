using EasyField.SaveSystem;
using EasyField.SaveSystem.Dto.FieldSaveDtoProviders;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace EasyField.SceneControllers
{
    public class SaveLoadManager<TFieldSaveDto>
    {
        private Task _saveloadTask;

        private readonly ISaver _saver;
        private readonly ILoader _loader;
        private readonly IFieldSaveDtoProvider<TFieldSaveDto> _dtoProvider;


        public SaveLoadManager(ISaver saver, ILoader loader, IFieldSaveDtoProvider<TFieldSaveDto> dtoProvider)
        {
            _saver = saver;
            _loader = loader;
            _dtoProvider = dtoProvider;
        }


        public async void StartSaving()
        {
            if (_saveloadTask != null && !_saveloadTask.IsCompleted)
            {
                return;
            }

            try
            {
                var saveDto = _dtoProvider.GetDto();

                _saveloadTask = _saver.SaveAsync<TFieldSaveDto>(saveDto);
                await _saveloadTask;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                _saveloadTask = null;
            }
        }

        public async Task<TFieldSaveDto> StartLoading()
        {
            if (_saveloadTask != null && !_saveloadTask.IsCompleted)
            {
                return default;
            }
            
            try
            {
                var loadTask = _loader.LoadAsync<TFieldSaveDto>();
                _saveloadTask = loadTask;

                return await loadTask;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return default;
            }
            finally
            {
                _saveloadTask = null;
                
            }
        }
    }
}
