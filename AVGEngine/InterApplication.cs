using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
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
            Resource.InitFromAssembly(resAssembly, nameSpace);
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
            private static Assembly resAssembly;
            private static string nameSpace;

            public static ImageSource Title = null;

            //注意这里的name**不需要**加上命名空间和**文件后缀**
            public static ImageSource LoadImageSource(string name)
            {
                var resLocation = nameSpace + ".Resource." + name + ".png";
                var res = resAssembly.GetManifestResourceStream(resLocation);

                if (res == null)
                    throw new NoNullAllowedException("Failed to find resource: " + resLocation +
                                                     ".Read ReadMe.txt in AVGGame/Resource to find more.");

                return ImageSource.FromStream(() => res);
            }

            public static void InitFromAssembly(Assembly ass, string nam)
            {
                resAssembly = ass;
                nameSpace = nam;

                foreach (var field in typeof(Resource).GetFields())
                {
                    var resName = field.Name;

                    //根据类型寻找加载器
                    var loadMethod = typeof(Resource).GetMethod("Load" + field.FieldType.Name);

                    if (loadMethod == null)
                        throw new InvalidDataException("Don't support res with type: " + field.FieldType.Name);

                    field.SetValue(null, loadMethod.Invoke(null, new object[] {resName}));
                }
            }
        }
    }
}