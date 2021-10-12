using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;

namespace InoveTeste.Page_Object
{
    class Contato
    {
        // ## Antiga estrutura do PageFactory (Obsoleto) ##
        /*[FindsBy(How = How.Name, Using = "nome")]
        public IWebElement name { get; set; }

         [FindsBy(How = How.Name, Using = "email")]
         public IWebElement email { get; set; }

         [FindsBy(How = How.Name, Using = "assunto")]
         public IWebElement assunto { get; set; }

         [FindsBy(How = How.Name, Using = "mensagem")]
         public IWebElement mensagem { get; set; }

         [FindsBy(How = How.CssSelector, Using = "input.wpcf7-form-control.wpcf7-submit")]
         public IWebElement enviar { get; set; }

         public void ButtonEnviarClick()
         {
             enviar.Click();
         }
        */

        private RemoteWebDriver _driver;

        public Contato(RemoteWebDriver driver)
        {
            _driver = driver;
        }

        IWebElement name => _driver.FindElementByName("nome");
        IWebElement email => _driver.FindElementByName("email");
        IWebElement assunto => _driver.FindElementByName("assunto");
        IWebElement mensagem => _driver.FindElementByName("mensagem");
        IWebElement enviar => _driver.FindElementByCssSelector("input.wpcf7-form-control.wpcf7-submit");

        public void VerificarExistenciaCampos()
        {
            Assert.IsTrue(name.Enabled);
            Assert.IsTrue(email.Enabled);
            Assert.IsTrue(assunto.Enabled);
            Assert.IsTrue(mensagem.Enabled);
            Assert.IsTrue(enviar.Enabled);
        }

        public void ClicarBotaoEnviar()
        {
            enviar.Click();
        }

    }
}
