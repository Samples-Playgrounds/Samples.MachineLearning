using System;
using Terminal.Gui;

namespace Ph4ct3x.App.XamarinForms.Terminal.GUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            global::Terminal.Gui.Forms.Forms.Init();


            var app = new App();
            var window = new global::Terminal.Gui.Forms.FormsWindow("Xamarin.Forms gui.cs Backend");
            window.LoadApplication(app);
            Application.Run();

            return;
        }
    }
}
