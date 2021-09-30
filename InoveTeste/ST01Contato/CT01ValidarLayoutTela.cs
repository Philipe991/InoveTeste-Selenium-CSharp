using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using InoveTeste.Page_Object;
using OpenQA.Selenium.Support.PageObjects;
using System.Configuration;
using InoveTeste;

namespace SeleniumTests
{
    [TestFixture]
    public class CT0ValidarLayoutTela
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            //driver = new FirefoxDriver();
            driver = Comandos.GetBrowserLocal(driver, ConfigurationManager.AppSettings["browser"]);
            baseURL = "https://inoveteste.com.br/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheCT02ValidarCamposObrigatoriosTest()
        {
            // Acessa o site
            driver.Navigate().GoToUrl(baseURL + "/contato");

            // Acessa o menu Contato
            //driver.FindElement(By.XPath("//a/span/i")).Click();
            //driver.FindElement(By.CssSelector("#mobile-menu-item-5643 > a > span")).Click();

            //Valida campos do formulário


            // PageObject
            Contato contato = new Contato();
            PageFactory.InitElements(driver, contato);

            Assert.IsTrue(contato.name.Displayed);
            Assert.IsTrue(contato.email.Displayed);
            Assert.IsTrue(contato.assunto.Displayed);
            Assert.IsTrue(contato.mensagem.Displayed);
            Assert.IsTrue(contato.enviar.Displayed);

        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
