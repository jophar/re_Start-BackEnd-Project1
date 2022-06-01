﻿using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS6_OOP_P1_2022_05_JoaoMariz
{
    /* 
     * Classe Aula que contem a definição do objecto aula
     * e os métodos para manipulação dos mesmos
     */

    internal class Aula
    {
        internal string PersonalTrainerName { get; set; }
        internal string UserName { get; set; }
        internal DateTime DataDaAula { get; set; }
        internal bool AulaAceite { get; set; }
        internal int NumeroDoPedido { get; set; }
        internal string Mensagem { get; set; }

        internal Aula()
        {
            PersonalTrainerName = string.Empty;
            UserName = string.Empty;
            DataDaAula = new DateTime();
            AulaAceite = true;
            Mensagem = string.Empty;
            NumeroDoPedido = 0;
        }

        internal Aula(string pt, string un, DateTime d, bool aa, int num)
        {
            PersonalTrainerName = pt;
            UserName = un;
            DataDaAula = d;
            AulaAceite = aa;
            NumeroDoPedido = num;
            Mensagem = string.Empty;
        }

        internal Aula(string pt, string un, DateTime d, bool aa, int num, string msg)
        {
            PersonalTrainerName = pt;
            UserName = un;
            DateTime DataDaAula = d;
            AulaAceite = aa;
            NumeroDoPedido = num;
            Mensagem = msg;
        }

        internal static void Request()
        { }

        internal static void CancelarPedido(string a)
        {
            int pedido = 0;

            string patternRequest = @"^cancel -r (?<pedido>[0-9]+)";

            if (Regex.IsMatch(a, patternRequest, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternRequest, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(a);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);
                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Value.NumeroDoPedido == pedido)
                            {
                                RSGym.aulas.Remove(pedido);
                                Console.WriteLine($"O pedido numero {pedido} foi eliminado\n");
                                return;
                            }
                        }

                        Console.WriteLine("Pedido não encontrado\n");
                    }
                }

            }
            else
            {
                Utilitarios.AjudaInfo();
            }
        }

        internal static void ConcluirAula(string a)
        {
            int pedido = 0;

            string patternFinish = @"^finish -r (?<pedido>[0-9]+)";

            if (Regex.IsMatch(a, patternFinish, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternFinish, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(a);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);

                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Value.NumeroDoPedido == pedido)
                            {
                                aula.Value.Mensagem = $"Aula concluída {DateTime.Now}";
                                Console.WriteLine(aula.Value.Mensagem);
                                return;
                            }
                        }

                        Console.WriteLine("Pedido não encontrado");
                    }

                }
            }

            Utilitarios.AjudaInfo();
        }


        internal static void InserirMensagem(string s) 
        {
            int pedido = 0;

            string patternMessage = @"^message -r (?<pedido>[0-9]+) -s (?<mensagem>[A-zÀ-ú0-9]+)";

            if (Regex.IsMatch(s, patternMessage, RegexOptions.IgnoreCase))
            {
                Regex rx = new Regex(patternMessage, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(s);

                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    bool b = int.TryParse(groups["pedido"].Value, out pedido);

                    if (b)
                    {
                        foreach (KeyValuePair<int, Aula> aula in RSGym.aulas)
                        {
                            if (aula.Value.NumeroDoPedido == pedido)
                            {
                                aula.Value.Mensagem = $"{groups["mensagem"].Value} - {DateTime.Now}";
                                Console.WriteLine(aula.Value.Mensagem);
                                return;
                            }
                        }

                        Console.WriteLine("Pedido não encontrado");
                        return;
                    }

                }
            }
            else
            {
                Utilitarios.AjudaInfo();
            }
        } 
    
    }
}
