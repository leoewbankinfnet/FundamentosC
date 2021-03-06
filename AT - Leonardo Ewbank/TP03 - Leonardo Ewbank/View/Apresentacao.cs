﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TP03___Leonardo_Ewbank.Controller;

namespace TP03___Leonardo_Ewbank
{
    class Apresentacao
    {

        public static void MenuPrincipal()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Escrever("Lista de Aniversariantes");
            Escrever("----------------------------------------------------------------------------------------");
            Hoje();
            Escrever("----------------------------------------------------------------------------------------");
            Escrever("Menu principal");
            Escrever("Selecione uma opção:");
            Escrever("1 - Cadastrar Pessoa");
            Escrever("2 - Consultar Pessoa");
            Escrever("3 - Alterar Pessoa");
            Escrever("4 - Deletar Pessoa");
            Escrever("5 - Sair ");
          

            int operacao = int.Parse(Console.ReadLine());

            switch (operacao)
            {
                case 1: CadastroPessoa(); break;
                case 2: ConsultarPessoa(); break;
                case 3: AlterarPessoa(); break;
                case 4: DeletarPessoa(); break;
                case 5: Environment.Exit(0); break;
                default: Escrever("Opção Inválida"); MenuPrincipal(); break;
            }
        }
        public static void Escrever(string texto)
        {
            Console.WriteLine(texto);
        }
        public static void Limpar()
        {
            Console.Clear();
        }
        public static void CadastroPessoa()
        {
            Limpar();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Escrever("Ficha de Cadastro, Preencha os dados solicitados(nome e sobrenome e data de aniversário)");
            Console.ForegroundColor = ConsoleColor.Red;
            Escrever("Digite o nome da Pessoa");
            string nome = Console.ReadLine();

            Escrever("Digite o sobrenome da Pessoa");
            string sobrenome = Console.ReadLine();

            nome = nome + " " + sobrenome;

            Escrever("Entre a Data de Nascimento da Pessoa");
            DateTime aniversario = DateTime.Parse(Console.ReadLine());


            var pessoa = new Pessoas();

            pessoa.Nome = nome;

            pessoa.DataAniversario = aniversario;

            BancoDeDadosEmArquivo.Salvar(pessoa);
            Escrever("Cadastrado Com Sucesso");
            System.Threading.Thread.Sleep(1000);
            Limpar();

            MenuPrincipal();

        }
        public static void ConsultarPessoa()
        {
            Limpar();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Escrever("Consultar Pessoa");
            Escrever("Escolha uma opção abaixo para a consulta:");
            Escrever("1 - Listar todas as pessoas cadastradas");
            Escrever("2 - Buscar por Nome");
            Escrever("3 - Buscar por Data");


            char operacao = Console.ReadLine().ToCharArray()[0];

            if (operacao == '1')
            {
                ListarTodos();
            }
            else if (operacao == '2')
            {
                BuscarPorNome();
            }
            else if (operacao == '3')
            {
                BuscarPorData();
            }

            else
            {
                Escrever("Operação Inválida, retornando ao menu principal");
                Limpar();
                MenuPrincipal();

            }
        }
        public static void AlterarPessoa()
        {
            Limpar();

            //1) Buscar 
            Escrever("Digite o nome completo da pessoa que deseja Buscar");
            string nomeCompleto = Console.ReadLine();

            //Buscar a pessoa pelo nome

            var buscado = BancoDeDadosEmArquivo.BuscarPeloNome(nomeCompleto);

            var mod = buscado;




            if (buscado == null)
            {
                Escrever("Pessoa não cadastrada");
                Escrever("Pressione Enter para Buscar outra pessoa ou ESC para retornar ao menu principal");

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    Limpar();
                    MenuPrincipal();
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Limpar();
                    AlterarPessoa();
                }
            }

            BancoDeDadosEmArquivo.Remover(buscado);
            Limpar();
            Escrever($"Nome: {buscado.Nome}");
            Escrever($"Data de Nascimento: {buscado.DataAniversario.Date.ToString("dd/MM/yyyy")}");


            //2)Alterar Dados
            bool alterar = true;

            while (alterar == true)
            {
                Escrever("O que você quer alterar?");

                Escrever("1 - Nome");

                Escrever("2 - Data de Nascimento");

                int operacao = int.Parse(Console.ReadLine());

                if (operacao == 1)
                {
                    Escrever("Digite o nome corrigido");
                    string Nome = Console.ReadLine();
                    if (!String.IsNullOrEmpty(Nome)) mod.Nome = Nome; 
                    Escrever("Deseja alterar outra informação? Digite S para sim e N para não");
                    string op = Console.ReadLine();
                    if (op.Equals("S", StringComparison.InvariantCultureIgnoreCase)) alterar = true;
                    else alterar = false;
                }
                else if (operacao == 2)
                {
                    Escrever("Digite a data corrigida");
                    mod.DataAniversario = DateTime.Parse(Console.ReadLine());
                    
                    Escrever("Deseja alterar outra informação? Digite S para sim e N para não");
                    string op = Console.ReadLine();
                    if (op.Equals("S", StringComparison.InvariantCultureIgnoreCase)) alterar = true;
                    else alterar = false;
                }
                else alterar = false;

            }

       

            BancoDeDadosEmArquivo.Salvar(mod);

            Limpar();
            MenuPrincipal();

        }
        public static void DeletarPessoa()
        {
            Limpar();
            Escrever("Digite o nome da pessoa que deseja deletar");
            string nomeCompleto = Console.ReadLine();

            var paraRemover = BancoDeDadosEmArquivo.BuscarPeloNome(nomeCompleto);

            if (paraRemover == null)
            {
                Escrever("Pessoa Nao Cadastrada");
                Pressionar();
            }

            BancoDeDadosEmArquivo.Remover(paraRemover);

            Escrever("Pessoa Removida Com Sucesso");
            Pressionar();


        }
        static void ListarTodos()
        {
            Limpar();

            var lista = BancoDeDadosEmArquivo.PessoasNoSistema();

            ifRetornoMenu(lista);
           
            
            foreach (var pessoa in lista)
            {
                Escrever($"{lista.IndexOf(pessoa)}){pessoa.Nome}"); 
            }
            Detalhes(BancoDeDadosEmArquivo.PessoasNoSistema().ToList());
            Pressionar();
        }
        static void BuscarPorNome()
        {
            Limpar();
            Escrever("Busca por Nome");
            Escrever("Digite o nome ou sobrenome da Pessoa");
            string nome = Console.ReadLine();


            var pessoasEncontradas = BancoDeDadosEmArquivo.PessoasNoSistema(nome);

            ifRetornoMenu(pessoasEncontradas.ToList());

            foreach (var pessoa in pessoasEncontradas)
            {
                Escrever($"{pessoasEncontradas.ToList().IndexOf(pessoa)}) {pessoa.Nome}");
            }
            Detalhes(pessoasEncontradas.ToList());

            Pressionar();

        }
        static void BuscarPorData()
        {
            Limpar();
            Escrever("Digite o mês desejado: ");
            int mes = int.Parse(Console.ReadLine());
            Escrever("Digite o dia desejado: ");
            int dia = int.Parse(Console.ReadLine());

            DateTime consulta = new DateTime(DateTime.Today.Year, mes, dia);


            var pessoasBuscadas = BancoDeDadosEmArquivo.PessoasNoSistema(consulta);

            var pessoasEncontradas = pessoasBuscadas.ToList();

            ifRetornoMenu(pessoasEncontradas);

            foreach (var pessoa in pessoasEncontradas)
            {
                Escrever($"{pessoasEncontradas.IndexOf(pessoa)}){pessoa.Nome}");
            }
            Detalhes(pessoasEncontradas);
            Pressionar();
        }
        static void ifRetornoMenu(List<Pessoas> Consulta)
        {
            if (Consulta.Count() == 0)
            {
                Escrever("Não foi possível retornar nenhuma pessoa cadastrada");
                Pressionar();
            }
        }
        static void Pressionar()
        {
            Escrever("Pressione uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Limpar();
            MenuPrincipal();
        }
        static void TempoParaAniversario(DateTime pessoa)
        {
            DateTime hoje = DateTime.Today.Date;
            DateTime prox = new DateTime(hoje.Year, pessoa.Month, pessoa.Day);
            if (prox < hoje)
            {
                prox = prox.AddYears(1);
            }
            int faltam = (prox - hoje).Days;
            if (faltam == 0)
            {
                Escrever("Eh Hoje!");
            }
            else Escrever($"Faltam {faltam} dias para o aniversário");
        }

        static void Detalhes(List<Pessoas> pessoa)
        {
            Escrever("\nDeseja ver mais detalhes dos aniversariantes?Pressione ENTER para sim e ESC para retornar ao menu principal\n");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                Escrever("Retornando ao menu principal");
                Limpar();
                MenuPrincipal();
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Escrever("Digite o indice do aniversariante desejado");
                int op = int.Parse(Console.ReadLine());

                var dets = new List<Pessoas>();

                foreach (var p in pessoa)
                {
                    if (pessoa.ToList().IndexOf(p) == op)
                    {
                        dets.Add(p);
                        break;
                    }
                }

                if (dets.Count() == 0)
                {
                    Limpar();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Escrever("Erro Na selecao");
                    Pressionar();
                }
                else
                {
                    Limpar();
                    foreach (var p in dets)
                    {
                        Escrever($"{p.Nome} Data de Aniversario {p.DataAniversario.ToString("dd/MM")}");
                        TempoParaAniversario(p.DataAniversario);
                    }
                }
            }
        }

        static void Hoje()
        {
            DateTime DtHoje = DateTime.Today;


            var AnivHoje = BancoDeDadosEmArquivo.AniversariantesHoje().ToList();


            Escrever($"Hoje eh dia {DtHoje.Date.ToString("dd/MM")} ");

            if (AnivHoje.Count() == 0)
            {
                Escrever("Ninguem faz aniversario hoje");
            }
            else
            {
                Escrever("Aniversariantes do dia:");

                foreach (var aniv in AnivHoje)
                {
                    Escrever($"{ aniv.Nome} \n");
                }
            }
            

        }
    }
}
