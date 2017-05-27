using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class UnitTest
    {
        static IWebDriver browser;
        static DefaultWait<ChromeDriver> wait;

        class Editor
        {
            public IWebElement btnProximo { get; private set; }
            public IWebElement progressBar { get; private set; }
            public IWebElement btnSalvar { get; private set; }

            public Editor()
            {
                btnProximo = wait.Until((c) => c.FindElement(By.CssSelector("button[click\\.trigger='nextPage()'")));
                progressBar = browser.FindElement(By.CssSelector("div.progress-bar"));
                btnSalvar = browser.FindElement(By.CssSelector("button[click\\.trigger='save()'"));
            }
        }

        class Page1
        {
            [FindsBy(How = How.Name, Using = "EMail")]
            public IWebElement txtEMail { get; private set; }

            public Page1()
            {
                PageFactory.InitElements(browser, this);
            }
        }

        class Page2
        {
            [FindsBy(How = How.Name, Using = "Nome")]
            public IWebElement txtNome { get; private set; }

            [FindsBy(How = How.Name, Using = "Skype")]
            public IWebElement txtSkype { get; private set; }

            [FindsBy(How = How.Name, Using = "LinkedIn")]
            public IWebElement txtLinkedIn { get; private set; }

            [FindsBy(How = How.Name, Using = "Telefone")]
            public IWebElement txtTelefone { get; private set; }

            [FindsBy(How = How.Name, Using = "Cidade")]
            public IWebElement txtCidade { get; private set; }

            [FindsBy(How = How.Name, Using = "Estado")]
            public IWebElement txtEstado { get; private set; }

            [FindsBy(How = How.Name, Using = "Portifolio")]
            public IWebElement txtPortifolio { get; private set; }

            [FindsBy(How = How.CssSelector, Using = "input[name='Disponibilidades']")]
            public IList<IWebElement> chkDisponibilidade { get; private set; }

            [FindsBy(How = How.CssSelector, Using = "input[name='MelhoresHorarios']")]
            public IList<IWebElement> chkMelhoresHorarios { get; private set; }

            [FindsBy(How = How.Name, Using = "PretencaoSalarialHora")]
            public IWebElement txtPretencaoSalarialHora { get; private set; }

            [FindsBy(How = How.Name, Using = "InformacaoBancaria")]
            public IWebElement txtInformacaoBancaria { get; private set; }


            public Page2()
            {
                PageFactory.InitElements(browser, this);
            }
        }

        class Page3
        {
            [FindsBy(How = How.Name, Using = "Titular")]
            public IWebElement txtTitular { get; private set; }

            [FindsBy(How = How.Name, Using = "Cpf")]
            public IWebElement txtCpf { get; private set; }

            [FindsBy(How = How.Name, Using = "Banco")]
            public IWebElement txtBanco { get; private set; }

            [FindsBy(How = How.Name, Using = "Agencia")]
            public IWebElement txtAgencia { get; private set; }

            [FindsBy(How = How.CssSelector, Using = "input[name='TipoConta']")]
            public IList<IWebElement> rdgTipoConta { get; private set; }

            [FindsBy(How = How.Name, Using = "Conta")]
            public IWebElement txtConta { get; private set; }

            public Page3()
            {
                PageFactory.InitElements(browser, this);
            }
        }

        class Page4
        {
            public IList<IList<IWebElement>> Niveis { get; private set; }

            [FindsBy(How = How.Name, Using = "OutrosConhecimentos")]
            public IWebElement OutrosConhecimentos { get; private set; }

            [FindsBy(How = How.Name, Using = "LinkCrud")]
            public IWebElement LinkCrud { get; private set; }

            public Page4()
            {
                PageFactory.InitElements(browser, this);

                Niveis = new List<IList<IWebElement>>();
                foreach (var element in browser.FindElements(By.TagName("radio-level")))
                {
                    Niveis.Add(element.FindElements(By.TagName("input")));
                }

            }
        }

        [TestInitialize]
        public void Setup()
        {
            browser = new ChromeDriver();
            browser.Manage().Window.Maximize();
            wait = new DefaultWait<ChromeDriver>((ChromeDriver)browser);
            wait.Timeout = TimeSpan.FromSeconds(30);
            wait.Message = "Objeto não encontrado.";
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        [TestCleanup]
        public void Cleanup()
        {
            browser.Quit();
        }

        public void AbrirCadastro()
        {
            browser.Navigate().GoToUrl("http://localhost:5000/");

            var cadastroLink = wait.Until((c) => c.FindElement(By.CssSelector("body > nav > div > ul > li:nth-child(2) > a")));
            cadastroLink.Click();

            wait.Until(b => b.FindElement(By.CssSelector("table > thead > tr")));
        }

        [TestMethod]
        public void TesteInclusao()
        {
            Incluir();
        }

        public void Incluir()
        {
            AbrirCadastro();

            var rows = browser.FindElements(By.CssSelector("table > tbody > tr"));
            var btnNovo = wait.Until((c) => c.FindElement(By.CssSelector("button[title='Novo']")));
            btnNovo.Click();

            var editor = new Editor();

            var page1 = new Page1();
            page1.txtEMail.SendKeys("fulano@email.com.br");
            editor.btnProximo.Click();
            wait.Until((c => editor.progressBar.Text == "2/4"));

            var page2 = new Page2();
            page2.txtNome.SendKeys("Fulano de Tal");
            page2.txtSkype.SendKeys("fulano.de.tal");
            page2.txtTelefone.SendKeys("(98) 3089-5299");
            page2.txtLinkedIn.SendKeys("fulano_de_tal");
            page2.txtCidade.SendKeys("Avaí");
            page2.txtEstado.SendKeys("SP");
            page2.txtPretencaoSalarialHora.SendKeys("33");
            page2.chkDisponibilidade[new Random().Next(page2.chkDisponibilidade.Count)].Click();
            page2.chkMelhoresHorarios[new Random().Next(page2.chkMelhoresHorarios.Count)].Click();
            editor.btnProximo.Click();
            wait.Until((c => editor.progressBar.Text == "3/4"));

            var page3 = new Page3();
            page3.txtTitular.SendKeys("Nome do titular");
            page3.txtCpf.SendKeys("528.294.333-61");
            page3.txtBanco.SendKeys("Banco do Brasil");
            page3.txtAgencia.SendKeys("0029-3");
            page3.rdgTipoConta[0].Click();
            page3.txtConta.SendKeys("147258-0");
            editor.btnProximo.Click();
            wait.Until((c => editor.progressBar.Text == "4/4"));

            var page4 = new Page4();
            foreach (var nivel in page4.Niveis)
            {
                nivel[new Random().Next(nivel.Count)].Click();
            }

            editor.btnSalvar.Click();

            wait.Message = "Candidato não incluído";
            wait.Until((c) =>
            {
                var r = browser.FindElements(By.CssSelector("table > tbody > tr"));
                return rows.Count == r.Count - 1;
            });
        }

        [TestMethod]
        public void TesteAlteracao()
        {
            Alterar();
        }

        public void Alterar()
        {
            AbrirCadastro();

            var rows = wait.Until(b => b.FindElements(By.CssSelector("table > tbody > tr")));

            if (rows.Count == 0)
            {
                Incluir();
                rows = wait.Until(b => b.FindElements(By.CssSelector("table > tbody > tr")));
            }
            var row = rows[rows.Count - 1];
            var btnEditar = row.FindElement(By.CssSelector("button[title='Editar']"));
            btnEditar.Click();

            var editor = new Editor();

            editor.btnProximo.Click();

            var page2 = new Page2();
            string txt = page2.txtNome.GetAttribute("value").ToUpper();
            page2.txtNome.Clear();
            page2.txtNome.SendKeys(txt);
            page2.txtPortifolio.SendKeys("Sem portifolio");
            editor.btnProximo.Click();
            editor.btnProximo.Click();

            var page4 = new Page4();
            page4.OutrosConhecimentos.SendKeys("xpto 5");
            page4.LinkCrud.SendKeys(@"github.com\usuario");

            editor.btnSalvar.Click();

            wait.Until(b => row.FindElement(By.CssSelector("td:nth-child(2)")).Text == txt);
        }

        [TestMethod]
        public void TesteExclusao()
        {
            Excluir();
        }

        public void Excluir()
        {
            AbrirCadastro();

            var rows = wait.Until(b => b.FindElements(By.CssSelector("table > tbody > tr")));

            if (rows.Count == 0)
            {
                Incluir();
                rows = wait.Until(b => b.FindElements(By.CssSelector("table > tbody > tr")));
            }
            var row = rows[rows.Count - 1];

            var btnExcluir = row.FindElement(By.CssSelector("button[title='Excluir']"));
            btnExcluir.Click();

            wait.Until(b => b.FindElement(By.CssSelector("ux-dialog-container .panel-title")).Text == "Confirmação");
            var btnSim = browser.FindElement(By.CssSelector("ux-dialog-container ux-dialog-footer > button:nth-child(1)"));
            btnSim.Click();

            wait.Until(b => b.FindElement(By.CssSelector("ux-dialog-container .panel-title")).Text == "Informação");
            var btnOk = browser.FindElement(By.CssSelector("ux-dialog-container ux-dialog-footer > button:nth-child(1)"));
            btnOk.Click();

            wait.Message = "Candidato não incluído";
            wait.Until((b) =>
            {
                var r = b.FindElements(By.CssSelector("table > tbody > tr"));
                return rows.Count == r.Count + 1;
            });
        }

        [TestMethod]
        public void TestCRUD()
        {
            Incluir();
            Alterar();
            Excluir();
        }
    }
}
