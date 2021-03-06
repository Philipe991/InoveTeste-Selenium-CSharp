using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using InoveTeste;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using InoveTeste.Page_Object;

namespace SeleniumTests
{
    [TestFixture]
    public class CT02ValidarCamposObrigatorios
    {
        private RemoteWebDriver driver;
        private WebDriverWait wait;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            //driver = new FirefoxDriver();
            driver = Comandos.GetBrowserLocal(driver, ConfigurationManager.AppSettings["browser"]);
            // Declaração de espera implicita
            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            // Declaração de espera explicita

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

            // Clica no botão salvar sem preencher os campos obrigatórios - Usando o PageFactory (Obsoleto)
            // driver.FindElement(By.CssSelector("input.wpcf7-form-control.wpcf7-submit")).Click();

            // Usando o PageObject mais recente para clicar no botão enviar
            Contato contato = new Contato(driver);
            contato.ClicarBotaoEnviar();

            // Validar as mensagens de crítica dos campos obrigatórios

            // Comando Sleep comentado por causa da espera implicita
            //Thread.Sleep(10000);

            // Declaração de espera explicita
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("span.wpcf7-not-valid-tip")));

            Assert.AreEqual("Por favor, preencha o campo obrigatório.", driver.FindElement(By.CssSelector("span.wpcf7-not-valid-tip")).Text);
            Assert.AreEqual("Por favor, preencha o campo obrigatório.", driver.FindElement(By.CssSelector("span.wpcf7-form-control-wrap.email > span.wpcf7-not-valid-tip")).Text);
            Assert.AreEqual("Por favor, preencha o campo obrigatório.", driver.FindElement(By.CssSelector("span.wpcf7-form-control-wrap.assunto > span.wpcf7-not-valid-tip")).Text);
            Assert.AreEqual("Por favor, preencha o campo obrigatório.", driver.FindElement(By.CssSelector("span.wpcf7-form-control-wrap.mensagem > span.wpcf7-not-valid-tip")).Text);
            Assert.AreEqual("Erros de validação ocorreram. Por favor, confirme os campos e envie-os novamente.", driver.FindElement(By.CssSelector("div.wpcf7-response-output")).Text);
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
