using System;
using System.Data;
using System.IO;
using System.Reflection;
using AVGEngine.Page;
using Xamarin.Forms;

namespace AVGEngine
{
    public class InterApplication : Application
    {
        public static InterApplication InterApp;

        public GamePage inGamePage { get; private set; }

        public InterApplication(Assembly resAssembly, string nameSpace)
        {
            InterApp = this;

            //加载资源
            Resource.LoadFromAssembly(resAssembly, nameSpace);
            //获取记录中当前所在的位置
            string saveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Save.aes";
            GamePage startMap = null;
            if (!File.Exists(saveFilePath))
                File.Create(saveFilePath).Close();

            string pageName = File.OpenText(saveFilePath).ReadLine();
            foreach (var type in resAssembly.GetTypes())
            {
                if (type.Name == "StartPage")
                    startMap = (GamePage) type.GetConstructor(new Type[0])?.Invoke(new object[0]);

                if (type.Name == pageName)
                {
                    inGamePage = (GamePage) type.GetConstructor(new Type[0])?.Invoke(new object[0]);
                    break;
                }
            }

            if (inGamePage == null)
                inGamePage = startMap;

            //切换到主菜单
            MainPage = new MainMenuPage();
        }

        public void SwitchTo(GamePage gamePage)
        {
            inGamePage = gamePage;
        }

        public static class Resource
        {
            public static ImageSource Title = null;

            public static void LoadFromAssembly(Assembly resAssembly, string nameSpace)
            {
                foreach (var field in typeof(Resource).GetFields())
                {
                    var resLocation = nameSpace + ".Resource." + field.Name;
                    Action<Stream> loadResource;

                    //根据类型判断后缀
                    switch (field.FieldType.Name)
                    {
                        case "ImageSource":
                            resLocation += ".png";
                            loadResource = s => { field.SetValue(null, ImageSource.FromStream(() => s)); };
                            break;
                        default:
                            throw new InvalidDataException("Don't support res with type: " + field.FieldType.Name);
                    }

                    var res = resAssembly.GetManifestResourceStream(resLocation);

                    if (res == null)
                        throw new NoNullAllowedException("Failed to find resource: " + resLocation +
                                                         ".Read ReadMe.txt in AVGGame/Resource to find more.");

                    loadResource(res);
                }
            }
        }
    }
}