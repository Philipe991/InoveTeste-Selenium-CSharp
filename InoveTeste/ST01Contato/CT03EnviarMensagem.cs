using System;
using System.Configuration;
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
using InoveTeste;

namespace ST01Contato
{
    [TestFixture]
    public class CT03EnviarMensagem
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
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
        public void TheCT03EnviarMensagemTest()
        {
            // Acessa o site
            driver.Navigate().GoToUrl(baseURL + "contato");

            // Acessa o menu Contato
            //driver.FindElement(By.XPath("//a/span/i")).Click();
            //driver.FindElement(By.CssSelector("#mobile-menu-item-5643 > a > span")).Click();
            
            // Preenche todos os campos do formulário
            driver.FindElement(By.Name("nome")).Clear();
            driver.FindElement(By.Name("nome")).SendKeys(ConfigurationManager.AppSettings["nome"]);
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(ConfigurationManager.AppSettings["email"]);
            driver.FindElement(By.Name("assunto")).Clear();
            driver.FindElement(By.Name("assunto")).SendKeys("Teste Automação com C#");
            driver.FindElement(By.Name("mensagem")).Clear();
            driver.FindElement(By.Name("mensagem")).SendKeys("Teste Automação com C#, curso da Udemy.");

            // Clica no botão Enviar após preencher todos os campos obrigatórios
            Comandos.ExecuteJavaScript(driver, "document.querySelector('input.wpcf7-form-control.wpcf7-submit').click()");

            // Valida a mensagem de sucesso no envio do formulário
            Thread.Sleep(10000);
            Assert.AreEqual("Falha no envio de sua mensagem. Por favor, tente mais tarde ou entre em contato com o administrador por outro método.", driver.FindElement(By.CssSelector("div.wpcf7-response-output")).Text);

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
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
