using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TP03___Leonardo_Ewbank
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal();
        }


        static List<Pessoas> pessoasCadastradas = new List<Pessoas>(); 
        public static void MenuPrincipal()
        {
            Escrever("Lista de Aniversariantes");
            Escrever("Menu principal");
            Escrever("Selecione uma opção:");
            Escrever("1 - Cadastrar Pessoa");
            Escrever("2 - Consultar Pessoa");
            Escrever("3 - Sair");

            char operacao = Console.ReadLine().ToCharArray()[0];

            if (operacao == '1')
            {
                CadastroPessoa();
            }
            else if (operacao =='2')
            {
                ConsultarPessoa();
            }
            else
            {
                Escrever("Operação Inválida");
                MenuPrincipal();
            }

        }
        static void Escrever(string texto)
        {
            Console.WriteLine(texto);
        }
        static void Limpar()
        {
            Console.Clear();
        }
        public static void CadastroPessoa()
        {
            Limpar();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Escrever("Ficha de Cadastro, Preencha os dados solicitados(nome e sobrenome e data de aniversário)");
            Console.ForegroundColor = ConsoleColor.Red;
            Escrever("Digite o nome e sobrenome da Pessoa");
            string nome = Console.ReadLine();

           

            Escrever("Entre a Data de Nascimento da Pessoa");
            DateTime aniversario = DateTime.Parse(Console.ReadLine());


            var pessoa = new Pessoas();

            pessoa.Nome = nome;
         
            pessoa.DataAniversario = aniversario;

            pessoasCadastradas.Add(pessoa);

            Escrever("Cadastrado Com Sucesso");

            MenuPrincipal();
    
        }
        public static void ConsultarPessoa()
        {
            Limpar();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Escrever("Consultar Pessoa");
            Console.ForegroundColor = ConsoleColor.Red;
            Escrever("Escolha uma opção abaixo para a consulta:");
            Escrever("1 - Listar todas as pessoas cadastradas");
            Escrever("2 - Buscar por Nome");

            char operacao = Console.ReadLine().ToCharArray()[0];

            if (operacao == '1')
            {
                ListarTodos();
            }
            else if (operacao == '2')
            {
                BuscarPorNome();
            }
            else
            {
                Escrever("Operação Inválida, retornando ao menu principal");
                Limpar();
                MenuPrincipal();
             
            }
        }

        static void ListarTodos()
        {
            Limpar();
            foreach (var pessoa in pessoasCadastradas )
            {
                Escrever($"{pessoasCadastradas.IndexOf(pessoa)}) {pessoa.Nome}");
            }
            Escrever("Escolha um numero para ver mais detalhes ou 0 para retornar ao menu principal");

            int operacao = int.Parse(Console.ReadLine());

            if (operacao == '0')
            {
                MenuPrincipal();
            }
            else
            {
                foreach(var pessoa in pessoasCadastradas)
                {
                    if(pessoasCadastradas.IndexOf(pessoa) == operacao)
                    {
                        Escrever($"Nome: {pessoa.Nome}, Data de Nascimento {pessoa.DataAniversario}");
                        TempoParaAniversario(pessoa.DataAniversario,pessoa.Nome);
                        
                    }
                }
            }

        }
        static void BuscarPorNome()
        {
            Escrever("Busca por Nome");
            Escrever("Digite o nome ou sobrenome da Pessoa");
            string nome = Console.ReadLine();

            var pessoasBuscadas = pessoasCadastradas.Where(pessoa => pessoa.Nome.Contains(nome));
            var pessoasEncontradas = new List<Pessoas>();

            foreach(var pessoa in pessoasBuscadas)
            {
                pessoasEncontradas.Add(pessoa);
            }

            foreach(var pessoa in pessoasEncontradas)
            {
                Console.WriteLine($"{pessoasEncontradas.IndexOf(pessoa)}){pessoa.Nome}");
            }
            Escrever("Escolha um numero para ver mais detalhes ou 0 para retornar ao menu principal");

            int operacao = int.Parse(Console.ReadLine());

            if (operacao == '0')
            {
                MenuPrincipal();
            }
            else
            {
                foreach (var pessoa in pessoasEncontradas)
                {
                    if (pessoasEncontradas.IndexOf(pessoa) == operacao)
                    {
                        Escrever($"Nome: {pessoa.Nome}, Data de Nascimento {pessoa.DataAniversario}");
                        TempoParaAniversario(pessoa.DataAniversario, pessoa.Nome);

                    }
                }
            }
            MenuPrincipal();
        }
        

        static void TempoParaAniversario(DateTime pessoa, string nome)
        {
            DateTime hoje = DateTime.Today;
            DateTime prox = new DateTime(hoje.Year,pessoa.Month,pessoa.Day );
            if (prox < hoje)
            {
                prox = prox.AddYears(1);
            }
            int faltam = (prox - hoje).Days;
            Escrever($"Faltam {faltam} dias para o aniversário da {nome}");
        }


        public class Pessoas
        {
            public string Nome { get; set; }
           
            public DateTime DataAniversario { get; set; }
        }

    }
}
