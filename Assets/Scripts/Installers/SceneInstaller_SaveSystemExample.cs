using EasyField.SaveSystem;
using EasyField.SaveSystem.FileDtoGateways;
using EasyField.Serializers;
using Zenject;

namespace EasyField.Installers
{
    public class SceneInstaller_SaveSystemExample : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSaveSystem();
        }

        /// <summary>
        /// Here are several examples of binding for the save system. Any variant will work (uncomment one of six inner methods)
        /// </summary>
        private void BindSaveSystem()
        {
            Container.BindInterfacesAndSelfTo<Saver>().AsSingle();
            Container.BindInterfacesAndSelfTo<Loader>().AsSingle();

            //Choose and uncomment any variant here (only one)
            UseStringSaving_NewtonsoftJson();
            UseStringSaving_JsonUtility();
            UseBytesSaving_NewtonsoftJson();
            UseBytesSaving_JsonUtility();
            UseCompressedBytesSaving_NewtonsoftJson();
            UseCompressedBytesSaving_JsonUtility();


            #pragma warning disable CS8321
            void UseStringSaving_NewtonsoftJson()
            {
                Container.BindInterfacesAndSelfTo<StringFileDtoGateway>().AsSingle();                
                Container.BindInterfacesAndSelfTo<NewtonsoftJsonStringSerializer>().AsSingle();                
            }

            void UseStringSaving_JsonUtility()
            {
                Container.BindInterfacesAndSelfTo<StringFileDtoGateway>().AsSingle();
                Container.BindInterfacesAndSelfTo<JsonUtilityStringSerializer>().AsSingle();
            }

            void UseBytesSaving_NewtonsoftJson()
            {
                Container.BindInterfacesAndSelfTo<BytesFileDtoGateway>().AsSingle();
                Container.BindInterfacesAndSelfTo<NewtonsoftJsonBytesSerializer>().AsSingle();
            }

            void UseBytesSaving_JsonUtility()
            {
                Container.BindInterfacesAndSelfTo<BytesFileDtoGateway>().AsSingle();
                Container.BindInterfacesAndSelfTo<JsonUtilityBytesSerializer>().AsSingle();
            }

            void UseCompressedBytesSaving_NewtonsoftJson()
            {
                Container.BindInterfacesAndSelfTo<BytesFileDtoGateway>().AsSingle();

                Container.BindInterfacesAndSelfTo<GZipCompressedBytesSerializer>().FromSubContainerResolve()
                    .ByMethod(subContainer =>
                    {
                        subContainer.Bind<GZipCompressedBytesSerializer>().AsSingle();
                        subContainer.BindInterfacesAndSelfTo<NewtonsoftJsonBytesSerializer>().AsSingle();
                    }).AsSingle();
            }

            void UseCompressedBytesSaving_JsonUtility()
            {
                Container.BindInterfacesAndSelfTo<BytesFileDtoGateway>().AsSingle();

                Container.BindInterfacesAndSelfTo<GZipCompressedBytesSerializer>().FromSubContainerResolve()
                    .ByMethod(subContainer =>
                    {
                        subContainer.Bind<GZipCompressedBytesSerializer>().AsSingle();
                        subContainer.BindInterfacesAndSelfTo<JsonUtilityBytesSerializer>().AsSingle();
                    }).AsSingle();
            }
            #pragma warning restore CS8321
        }
    }
}
