using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeUpload
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // Driver
        public ChromeDriver Chrome_Sekme;
        private ChromeDriverService Chrome_Servis = ChromeDriverService.CreateDefaultService();
        private ChromeOptions Chrome_Ayar = new ChromeOptions();



        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            // Driver servis ayarları
            Chrome_Servis.HideCommandPromptWindow = true; // komut satırı gizleme


            // Parametreler
            //Chrome_Ayar.AddArguments("--incognito"); // gizli tarayıcı
            Chrome_Ayar.AddArgument("--disable-default-apps"); // varsayılan appleri kaldırma
            Chrome_Ayar.AddArgument("--disable-extensions"); // varsayılan appleri kaldırma
            Chrome_Ayar.AddArgument("--disable-gpu"); // gpu kullanımını kaldırma
            Chrome_Ayar.AddExcludedArgument("enable-automation"); // info bar gizleme
            Chrome_Ayar.AddAdditionalCapability("useAutomationExtension", false); // otomatik eklentiler 
            Chrome_Ayar.AddArgument("--no-sandbox"); // emin değilim

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://stackoverflow.com/users/login?ssrc=head&returnurl=https%3a%2f%2fstackoverflow.com%2f";
            string eposta = "";
            string sifre = "";

            string ytUploadUrl = "https://www.youtube.com/upload";

            

            Chrome_Sekme = new ChromeDriver(Chrome_Servis, Chrome_Ayar);
            
            // Tarayıcıyı aç
            Chrome_Sekme.Navigate().GoToUrl(url);

            // Site açılınca, gmail ile giriş yap
            Chrome_Sekme.FindElement(By.XPath("//*[@id='openid-buttons']/button[1]")).Click();

            // Eposta gir
            Chrome_Sekme.FindElementByXPath("//input[@type='email']").SendKeys(eposta);
            // İleri tıkla
            Chrome_Sekme.FindElementByXPath("//*[@id='identifierNext']").Click();

            Thread.Sleep(3000);
            
            // Şifre gir
            Chrome_Sekme.FindElementByXPath("//input[@type='password']").SendKeys(sifre);
            // İleri tıkla
            Chrome_Sekme.FindElementByXPath("//*[@id='passwordNext']").Click();

            Thread.Sleep(1000);

            // Youtube aç
            Chrome_Sekme.Navigate().GoToUrl(ytUploadUrl);
            /*Chrome_Sekme.FindElementByXPath("//input[@type='file']")
                .SendKeys(@"C:\Users\omnyvz\Desktop\TestVideo.mp4");*/



        }



        /// <summary>
        /// Tarayici Kaynak Kod
        /// </summary>
        /// <returns></returns>
        private string KaynakKod()
        {
            string DonenVeri = "";
            Thread t_Kaynak = new Thread(delegate ()
            {
                try
                {
                    DonenVeri = Chrome_Sekme.PageSource;
                }
                catch
                {
                    DonenVeri = "false";
                }
            });
            t_Kaynak.Start();
            t_Kaynak.Join();
            return DonenVeri;
        }

        /// <summary>
        /// Tarayici Sonlandır
        /// </summary>
        /// <returns></returns>
        public void Sonlandir()
        {
            try
            {
                Chrome_Sekme.Close();
                Chrome_Sekme.Quit();
                Chrome_Sekme.Dispose();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sonlandir();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*string HTML_Kod = KaynakKod();
            richTextBox1.Text = HTML_Kod;*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ytUploadUrl = "https://www.youtube.com/upload";

            Chrome_Sekme.FindElement(By.CssSelector("body")).SendKeys(OpenQA.Selenium.Keys.Control + "t");
            Chrome_Sekme.SwitchTo().Window(Chrome_Sekme.WindowHandles.Last());
            Chrome_Sekme.Navigate().GoToUrl(ytUploadUrl);

            /*Chrome_Sekme = new ChromeDriver(Chrome_Servis, Chrome_Ayar);

            // Tarayıcıyı aç
            Chrome_Sekme.Navigate().GoToUrl(ytUploadUrl);*/

        }
    }
}
