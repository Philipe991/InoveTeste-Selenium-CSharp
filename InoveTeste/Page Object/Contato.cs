using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace InoveTeste.Page_Object
{
    class Contato
    {
       [FindsBy(How = How.Name, Using = "nome")]
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

    }
}
